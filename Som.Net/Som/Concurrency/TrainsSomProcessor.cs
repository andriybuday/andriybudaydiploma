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
    // all 4 run concurrently
    // they join after best matching neuron found
    // then they start accomodation, which is done on bit arrays
    // then join after accomodation has finished
    // start from finding again!!
    public class TrainsSomProcessor : ILearningProcessor
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

        #region bla
        // this is needed just to save interface 
        public int FindBestMatchingNeuron(double[] dataVector)
        {
            throw new NotImplementedException();
        }
        public void AccommodateNetworkWeights(int bmnIndex, double[] dataVector, int iteration)
        {
            throw new NotImplementedException();
        }
        #endregion bla

        private int GridGroups;

        private volatile int _executedThreadsCounter = 0;
        private int[] bmnNumbers;
        private double[] bmnDistances;


        private AutoResetEvent[] BmnFound { get; set; }
        private AutoResetEvent[] StartFindBmn { get; set; }
        private AutoResetEvent[] StartAccomodation { get; set; }
        private AutoResetEvent[] FinishedAccomodation { get; set; }

        public TrainsSomProcessor(ILearningDataProvider learningDataProvider,
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
            BmnFound = new AutoResetEvent[GridGroups];
            StartFindBmn = new AutoResetEvent[GridGroups];
            StartAccomodation = new AutoResetEvent[GridGroups];
            FinishedAccomodation = new AutoResetEvent[GridGroups];
            for (int i = 0; i < GridGroups; i++)
            {
                BmnFound[i] = new AutoResetEvent(false);
                StartFindBmn[i] = new AutoResetEvent(false);
                StartAccomodation[i] = new AutoResetEvent(false);
                FinishedAccomodation[i] = new AutoResetEvent(false);
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
                //FindBestNeuronRequests[i].DoneEvent = BmnFound[i];
                start = end;
                end += sectionLength;
                if (i == GridGroups - 1) end = neuronsCount;
            }
        }

        public virtual void Learn()
        {
            // LET THEY WORK!!!
            for (int i = 0; i < GridGroups; i++)
            {
                ThreadPool.UnsafeQueueUserWorkItem(ThreadProc, i);
                ThreadPool.UnsafeQueueUserWorkItem(ThreadProcAccomodate, i);
            }


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
                    CurrentDataVector = LearningDataProvider.GetLearingDataVector(suffleList[dataInd]);

                    for (int i = 1; i < GridGroups; i++) StartFindBmn[i].Set();
                    AccommodateNetworkWeightsInThreadRange(0);
                    for (int i = 1; i < GridGroups; i++) BmnFound[i].WaitOne();
                    
                    CurrentBMN = FindBestMatchingNeuron();
                    SetAccododationRange();

                    for (int i = 1; i < GridGroups; i++) StartAccomodation[i].Set();
                    ThreadProcAccomodate(0);
                    for (int i = 1; i < GridGroups; i++) FinishedAccomodation[i].WaitOne();
                }
            }
            //Monitor.
            // looks that we need this to finish running...
            //for (int i = 0; i < GridGroups; i++) StartFindBmn[i].Set();
        }


        private void ThreadProc(object threadNmbr)
        {
            int threadNumber = (int)threadNmbr;

            while (CurrentIteration < MaxIterationsCount)// while(true) is better? :)
            {
                StartFindBmn[threadNumber].WaitOne();

                FindBestMatchingNeuronInThreadRange(threadNumber);

                BmnFound[threadNumber].Set();
            }
        }

        private void ThreadProcAccomodate(object threadNmbr)
        {
            int threadNumber = (int)threadNmbr;

            while (CurrentIteration < MaxIterationsCount)// while(true) is better? :)
            {
                StartAccomodation[threadNumber].WaitOne();

                AccommodateNetworkWeightsInThreadRange(threadNumber);

                FinishedAccomodation[threadNumber].Set();
            }
        }


        protected virtual void AccommodateNeuronWeights(int neuronNumber, double distance)
        {
            var neuronWghts = Network.Neurons[neuronNumber].Weights;

            var falloffRate = NeighbourhoodFunction.GetDistanceFalloff(distance, CurrentRadius);

            for (int i = 0; i < neuronWghts.Length; i++)
            {
                double weight = neuronWghts[i];
                neuronWghts[i] += CurrentLearningRate * falloffRate * (CurrentDataVector[i] - weight);
            }
        }

        public int FindBestMatchingNeuron()
        {
            int result = -1;

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

        public void AccommodateNetworkWeights()
        {
            var radius = RadiusProvider.GetRadius(CurrentIteration);
            var effectedNeurons = Topology.GetNeuronsInRadius(CurrentBMN, radius);

            foreach (var effectedNeuron in effectedNeurons)
            {
                var distance = Topology.DistancesToWinner[effectedNeuron];

                AccommodateNeuronWeights(effectedNeuron, distance);
            }

        }
        public void SetAccododationRange()
        {
            CurrentRadius = RadiusProvider.GetRadius(CurrentIteration);
            CurrentEffectedNeurons = Topology.GetNeuronsInRadius(CurrentBMN, CurrentRadius);

            var effectedNeuronsCount = CurrentEffectedNeurons.Count;

            int sectionLength = effectedNeuronsCount / GridGroups;
            int start = 0;
            int end = sectionLength;
            for (int i = 0; i < GridGroups; i++)
            {
                AccomodateNetworkRequests[i].Start = start;
                AccomodateNetworkRequests[i].End = end;

                start = end;
                end += sectionLength;
                if (i == GridGroups - 1) end = effectedNeuronsCount;
            }
        }

        private void FindBestMatchingNeuronInThreadRange(int threadNumber)
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

        protected virtual void AccommodateNetworkWeightsInThreadRange(int threadNumber)
        {
            var findRequest = AccomodateNetworkRequests[threadNumber];

            for (int i = findRequest.Start; i < findRequest.End; i++)
            {
                AccommodateNeuronWeights(CurrentEffectedNeurons[i], Topology.DistancesToWinner[CurrentEffectedNeurons[i]]);
            }
        }

        private FindBestNeuronRequest[] FindBestNeuronRequests;
        private AccomodateNetworkRequest[] AccomodateNetworkRequests;
        private double[] CurrentDataVector;
        private double CurrentLearningRate;
        private int CurrentBMN;
        private int CurrentIteration;
        private double CurrentRadius;
        private List<int> CurrentEffectedNeurons;
    }
}
