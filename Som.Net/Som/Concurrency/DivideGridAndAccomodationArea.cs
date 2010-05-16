using System;
using System.Collections.Generic;
using System.Threading;
using Som.Concurrency;
using Som.Data;
using Som.Data.Shuffle;
using Som.Learning;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.LearningProcessor
{
    public class DivideGridAndAccomodationArea: ILearningProcessor
    {
        public INetwork Network { get; private set; }
        public ITopology Topology { get; private set; }
        protected IShuffleProvider ShuffleProvider { get; set; }
        protected ILearningDataProvider LearningDataProvider { get; set; }
        protected IRadiusProvider RadiusProvider { get; private set; }
        protected INeighbourhoodFunction NeighbourhoodFunction { get; set; }
        protected IMetricFunction MetricFunction { get; set; }
        protected ILearningFactorFunction LearningFactorFunction { get; set; }
        public int MaxIterationsCount { get; protected set; }

        private int GridGroups;

        private volatile int _executedThreadsCounter = 0;
        private int[] bmnNumbers;
        private double[] bmnDistances;


        private AutoResetEvent[] DoneEvents { get; set; }

        public DivideGridAndAccomodationArea(ILearningDataProvider learningDataProvider,
            INetwork network,
            IMetricFunction metricFunction,
            ILearningFactorFunction learningFactorFunction,
            INeighbourhoodFunction neighbourhoodFunction,
            int maxIterationsCount,
            IShuffleProvider shuffleProvider,
            int gridGroups)
        {
            LearningDataProvider = learningDataProvider;
            Network = network;
            Topology = network.Topology;
            MetricFunction = metricFunction;
            LearningFactorFunction = learningFactorFunction;
            NeighbourhoodFunction = neighbourhoodFunction;
            MaxIterationsCount = maxIterationsCount;
            ShuffleProvider = shuffleProvider;

            RadiusProvider = new DefaultRadiusProvider(maxIterationsCount, Topology.WholeTopologyRadius);


            GridGroups = gridGroups;
            DoneEvents = new AutoResetEvent[GridGroups];
            for (int i = 0; i < GridGroups; i++)
            {
                DoneEvents[i] = new AutoResetEvent(false);    
            }
            

            bmnNumbers = new int[GridGroups];
            bmnDistances = new double[GridGroups];

            FindBestNeuronRequests = new FindBestNeuronRequest[GridGroups];
            AccomodateNetworkRequests = new AccomodateNetworkRequest[GridGroups];

            

            SetStartEndParams();
        }

        private void SetStartEndParams()
        {
            var neuronsCount = Network.Neurons.Length;
            int sectionLength = neuronsCount / GridGroups;
            int start = 0;
            int end = sectionLength;
            for (int i = 0; i < GridGroups; i++)
            {
                FindBestNeuronRequests[i].Start = start;
                FindBestNeuronRequests[i].End = end;
                FindBestNeuronRequests[i].DoneEvent = DoneEvents[i];
                start = end;
                end += sectionLength;
                if (i == GridGroups - 1) end = neuronsCount;
            }
        }

        public virtual void Learn()
        {
            int vectorsCount = LearningDataProvider.LearningVectorsCount;
            IList<int> suffleList = new ShuffleList(vectorsCount);

            for (int iteration = 0; iteration < MaxIterationsCount; iteration++)
            {
                //if ((iteration % 1000) == 0) Console.Write(string.Format("{0} ", iteration));
                CurrentLearningRate = LearningFactorFunction.GetLearningRate(iteration);
                suffleList = ShuffleProvider.Suffle(suffleList);

                for (int dataInd = 0; dataInd < vectorsCount; dataInd++)
                {
                    double[] dataVector = LearningDataProvider.GetLearingDataVector(suffleList[dataInd]);

                    int bestNeuronNum = FindBestMatchingNeuron(dataVector);

                    AccommodateNetworkWeights(bestNeuronNum, dataVector, iteration);
                }
            }
        }

        protected virtual void AccommodateNeuronWeights(int neuronNumber, double[] dataVector, int iteration, double distance, double radius)
        {
            var neuronWghts = Network.Neurons[neuronNumber].Weights;

            
            var falloffRate = NeighbourhoodFunction.GetDistanceFalloff(distance, radius);

            for (int i = 0; i < neuronWghts.Length; i++)
            {
                double weight = neuronWghts[i];
                neuronWghts[i] += CurrentLearningRate * falloffRate * (dataVector[i] - weight);
            }
        }

        public int FindBestMatchingNeuron(double[] dataVector)
        {
            int result = -1;
            CurrentDataVector = dataVector;
            _executedThreadsCounter = 0;
            for (int i = 1; i < GridGroups; i++)
            {
                ThreadPool.UnsafeQueueUserWorkItem(AsynchronousFindBestMatchingNeuron, i);
            }
            AsynchronousFindBestMatchingNeuron(0);

            //joining threads
            for (int i = 1; i < GridGroups; i++) DoneEvents[i].WaitOne();

            Double minDistance = Double.MaxValue;
            for (int i = 0; i < GridGroups; i++)
            {
                double distance = bmnDistances[i];
                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = bmnNumbers[i];
                }
            }

            return result;
        }

        public void AccommodateNetworkWeights(int bestNeuronNum, double[] dataVector, int iteration)
        {
            var radius = RadiusProvider.GetRadius(iteration);
            var effectedNeurons = Topology.GetNeuronsInRadius(bestNeuronNum, radius);

            var effectedNeuronsCount = effectedNeurons.Count;
            if (effectedNeuronsCount >= 100)
            {
                int sectionLength = effectedNeuronsCount / GridGroups;
                int start = 0;
                int end = sectionLength;
                for (int i = 0; i < GridGroups; i++)
                {
                    AccomodateNetworkRequests[i].Iteration = iteration;
                    AccomodateNetworkRequests[i].Radius = radius;
                    AccomodateNetworkRequests[i].Start = start;
                    AccomodateNetworkRequests[i].End = end;
                    AccomodateNetworkRequests[i].EffectedNeurons = effectedNeurons;
                    AccomodateNetworkRequests[i].DoneEvent = DoneEvents[i];

                    start = end;
                    end += sectionLength;
                    if (i == GridGroups - 1) end = effectedNeuronsCount;
                }

                for (int i = 1; i < GridGroups; i++)
                {
                    ThreadPool.UnsafeQueueUserWorkItem(AsynchronousAccommodateNetworkWeights, i);
                }
                AsynchronousAccommodateNetworkWeights(0);

                //AsynchronousAccommodateNetworkWeights(0);
                //joining threads
                for (int i = 1; i < GridGroups; i++)
                {
                    AccomodateNetworkRequests[i].DoneEvent.WaitOne();
                }
            }
            else
            {
                foreach (var effectedNeuron in effectedNeurons)
                {
                    var distance = Topology.DistancesToWinner[effectedNeuron];

                    AccommodateNeuronWeights(effectedNeuron, dataVector, iteration, distance, radius);
                }

            }
        }

        protected virtual void AsynchronousAccommodateNetworkWeights(object request)
        {
            var findRequest = AccomodateNetworkRequests[(int)request];
            var iteration = findRequest.Iteration;
            var radius = findRequest.Radius;

            for (int i = findRequest.Start; i < findRequest.End; i++)
            {
                AccommodateNeuronWeights(findRequest.EffectedNeurons[i], CurrentDataVector, iteration, Topology.DistancesToWinner[findRequest.EffectedNeurons[i]], radius);
            }

            findRequest.DoneEvent.Set();
        }

        private void ScheduleFindBestMatchingNeuron(int threadNumber)
        {
            int result = -1;
            Double minDistance = Double.MaxValue;
            int end = FindBestNeuronRequests[threadNumber].End;
            for (int i = FindBestNeuronRequests[threadNumber].Start; i < end; i++)
            {
                double distance = MetricFunction.GetDistance(Network.Neurons[i].Weights, CurrentDataVector);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = i;
                }
            }

            bmnDistances[threadNumber] = minDistance;
            bmnNumbers[threadNumber] = result;
        }

        private void AsynchronousFindBestMatchingNeuron(object threadNumberRequest)
        {
            int threadNumber = (int) threadNumberRequest;
            var findRequest = FindBestNeuronRequests[threadNumber];
            this.ScheduleFindBestMatchingNeuron(threadNumber);
            findRequest.DoneEvent.Set();
        }

        private FindBestNeuronRequest[] FindBestNeuronRequests;
        private AccomodateNetworkRequest[] AccomodateNetworkRequests;
        private double[] CurrentDataVector;
        private double CurrentLearningRate;        
    }
}
