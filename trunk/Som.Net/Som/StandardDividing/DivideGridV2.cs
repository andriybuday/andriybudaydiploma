using System;
using System.Collections.Generic;
using System.Threading;
using Som.Concurrency;
using Som.Data;
using Som.Data.Shuffle;
using Som.Learning;
using Som.LearningProcessor;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.Concurrency
{
    public class DivideGridV2 : ILearningProcessor
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

        public DivideGridV2(ILearningDataProvider learningDataProvider,
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

            //TODO
            // this is super crap stuff
            ColCount = Topology.ColCount;
            RowCount = Topology.RowCount;
            _n = RowCount * ColCount;
            DistancesToWinner = new double[NeuronsCount];
            WholeTopologyRadius = Math.Max(ColCount, RowCount) / 2.0;
            //


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
                CurrentIteration = iteration;
                //if ((iteration % 1000) == 0) Console.Write(string.Format("{0} ", iteration));
                CurrentLearningRate = LearningFactorFunction.GetLearningRate(iteration);
                suffleList = ShuffleProvider.Suffle(suffleList);

                for (int dataInd = 0; dataInd < vectorsCount; dataInd++)
                {
                    double[] dataVector = LearningDataProvider.GetLearingDataVector(suffleList[dataInd]);

                    CurrentBMN = FindBestMatchingNeuron(dataVector);

                    AccommodateNetworkWeights(CurrentBMN, dataVector, iteration);
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

        public void AccommodateNetworkWeights(int bestMatchingNeuron, double[] dataVector, int iteration)
        {
            CurrentRadius = RadiusProvider.GetRadius(iteration);

            for (int i = 1; i < GridGroups; i++)
            {
                ThreadPool.UnsafeQueueUserWorkItem(AsynchronousAccommodateNetworkWeights, i);
            }
            AsynchronousAccommodateNetworkWeights(0);

            //joining threads
            for (int i = 1; i < GridGroups; i++) DoneEvents[i].WaitOne();
        }

        protected virtual void AsynchronousAccommodateNetworkWeights(object objThreadNumber)
        {
            int threadNumber = (int)objThreadNumber;
            int end = FindBestNeuronRequests[threadNumber].End;

            for (int i = FindBestNeuronRequests[threadNumber].Start; i < end; i++)
            {
                var distance = GetDistance(CurrentBMN, i);
                if (distance < CurrentRadius)
                {
                    AccommodateNeuronWeights(i, CurrentDataVector, CurrentIteration, distance, CurrentRadius);
                }
            }

            DoneEvents[threadNumber].Set();
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
            int threadNumber = (int)threadNumberRequest;
            ScheduleFindBestMatchingNeuron(threadNumber);
            DoneEvents[threadNumber].Set();
        }

        private FindBestNeuronRequest[] FindBestNeuronRequests;
        private AccomodateNetworkRequest[] AccomodateNetworkRequests;
        private double[] CurrentDataVector;
        private double CurrentLearningRate;



        #region TopologyStuff
        private int _n;
        private int CurrentBMN;
        private double CurrentRadius;
        private int CurrentIteration;
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public double WholeTopologyRadius { get; private set; }

        public double[] DistancesToWinner { get; private set; }
        public int NeuronsCount { get { return _n; } }

        private double GetDistance(int firstNeuronPos, int secondNeuronPos)
        {
            var fColPos = firstNeuronPos % ColCount;
            var fRowPos = firstNeuronPos / ColCount;
            var sColPos = secondNeuronPos % ColCount;
            var sRowPos = secondNeuronPos / ColCount;
            var xD = (fColPos - sColPos);
            var yD = (fRowPos - sRowPos);
            return Math.Sqrt(xD * xD + yD * yD);
        }

        #endregion
    }
}
