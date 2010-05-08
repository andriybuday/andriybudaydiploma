using System;
using System.Collections.Generic;
using System.Threading;
using Som.Data;
using Som.Data.Suffle;
using Som.Learning;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.LearningProcessor
{
    public class MasterSlaveLearningProcessor: SomLearningProcessor
    {
        public int GridDivideNumber { get; private set; }
        private Dictionary<int, double> BestMatchingNeurons { get; set; }
        private ManualResetEvent[] DoneEvents { get; set; }

        public MasterSlaveLearningProcessor(ILearningDataProvider learningDataProvider, INetwork network, ITopology topology, IMetricFunction metricFunction, ILearningFactorFunction learningFactorFunction, INeighbourhoodFunction neighbourhoodFunction, int maxIterationsCount, IShuffleProvider shuffleProvider)
            : base(learningDataProvider, network, metricFunction, learningFactorFunction, neighbourhoodFunction, maxIterationsCount, shuffleProvider)
        {
            GridDivideNumber = 4;
            DoneEvents = new ManualResetEvent[GridDivideNumber];
        }

        //what do I want to see here?

        // first of all I want to have two threads..
        // I want to know which of two threads executed first
        // see if there exists overlapping area..
        // if NO - run AccommodateNetworkWeights in two separate threads.
        // if YES - run AccommodateNetworkWeights of the first winner...
        //          make Second to be master and run Accommodate for it..
        public override void Learn()
        {
            int vectorsCount = LearningDataProvider.LearningVectorsCount;
            IList<int> suffleList = new ShuffleList(vectorsCount);

            for (int iteration = 0; iteration < MaxIterationsCount; iteration++)
            {
                if ((iteration % 1000) == 0) Console.Write(string.Format("{0} ", iteration));
                suffleList = ShuffleProvider.Suffle(suffleList);



                double[] mDataVector = LearningDataProvider.GetLearingDataVector(suffleList[0]);
                int mBestNeuronNum = FindBestMatchingNeuron(mDataVector);

                for (int dataInd = 0; dataInd < vectorsCount; dataInd++)
                {
                    int sDataInd = dataInd + 1;
                    if (sDataInd < vectorsCount)
                    {
                        double[] sDataVector = LearningDataProvider.GetLearingDataVector(suffleList[sDataInd]);
                        int sBestNeuronNum = FindBestMatchingNeuron(sDataVector);
                        double r = RadiusProvider.GetRadius(iteration);

                        bool areOverlapped = Topology.Overlaps(mBestNeuronNum, sBestNeuronNum, r);
                        if(areOverlapped)
                        {
                            AccommodateNetworkWeights(mBestNeuronNum, mDataVector, iteration);
                            mBestNeuronNum = sBestNeuronNum;
                            mDataVector = sDataVector;
                        }
                        else
                        {
                            AccommodateNetworkWeights(mBestNeuronNum, mDataVector, iteration);
                            AccommodateNetworkWeights(sBestNeuronNum, sDataVector, iteration);
                            dataInd++;
                            mDataVector = LearningDataProvider.GetLearingDataVector(suffleList[dataInd]);
                            mBestNeuronNum = FindBestMatchingNeuron(mDataVector);
                        }
                        continue;
                    }

                    AccommodateNetworkWeights(mBestNeuronNum, mDataVector, iteration);
                }
            }
        }

        public override int FindBestMatchingNeuron(double[] dataVector)
        {
            int result = -1;
            Double minDistance = Double.MaxValue;
            var neuronsCount = Network.Neurons.Count;

            BestMatchingNeurons = new Dictionary<int, double>();
            int sectionLength = neuronsCount/GridDivideNumber;
            int start = 0;
            int end = sectionLength;
            for (int i = 0; i < GridDivideNumber; i++)
            {
                //ScheduleFindBestMatchingNeuron(start, end, dataVector);
                DoneEvents[i] = new ManualResetEvent(false);
                var request = new FindBestNeuronRequest(start, end, dataVector, DoneEvents[i]);
                ThreadPool.QueueUserWorkItem(AsynchronousFindBestMatchingNeuron, request);
                
                start = end;
                end += sectionLength;
                if (i == GridDivideNumber - 1) end = neuronsCount;
            }
            
            //joining threads
            WaitHandle.WaitAll(DoneEvents);


            foreach (var matchingNeuron in BestMatchingNeurons)
            {
                double distance = matchingNeuron.Value;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = matchingNeuron.Key;
                }
            }

            return result;
        }

        public override void AccommodateNetworkWeights(int bestNeuronNum, double[] dataVector, int iteration)
        {
            var radius = RadiusProvider.GetRadius(iteration);
            var effectedNeurons = Topology.GetNeuronsInRadius(bestNeuronNum, radius);

            var effectedNeuronsCount = effectedNeurons.Count;
            if (effectedNeuronsCount >= 100)
            {
                int sectionLength = effectedNeuronsCount / GridDivideNumber;
                int start = 0;
                int end = sectionLength;
                for (int i = 0; i < GridDivideNumber; i++)
                {
                    DoneEvents[i] = new ManualResetEvent(false);
                    var request = new AccomodateNetworkRequest(iteration, radius, start, end, effectedNeurons, dataVector, DoneEvents[i]);

                    ThreadPool.QueueUserWorkItem(AsynchronousAccommodateNetworkWeights, request);

                    start = end;
                    end += sectionLength;
                    if (i == GridDivideNumber - 1) end = effectedNeuronsCount;
                }
                //joining threads
                WaitHandle.WaitAll(DoneEvents);
            }
            else
            {
                foreach (var effectedNeuron in effectedNeurons.Keys)
                {
                    var distance = effectedNeurons[effectedNeuron];

                    AccommodateNeuronWeights(effectedNeuron, dataVector, iteration, distance, radius);
                }

            }
        }

        protected virtual void AsynchronousAccommodateNetworkWeights(object request)
        {
            var findRequest = (AccomodateNetworkRequest)request;
            var iteration = findRequest.Iteration;
            var radius = findRequest.Radius;

            var enumerator = findRequest.EffectedNeurons.GetEnumerator();
            int counter = 0;
            while (enumerator.MoveNext())
            {
                if(counter < findRequest.Start)
                {
                    ++counter;
                    continue;
                }
                if(counter >= findRequest.End)break;

                AccommodateNeuronWeights(enumerator.Current.Key, findRequest.DataVector, iteration, enumerator.Current.Value, radius);

                ++counter;
            }

            findRequest.DoneEvent.Set();
        }

        private void ScheduleFindBestMatchingNeuron(int start, int end, double[] dataVector)
        {
            int result = -1;
            Double minDistance = Double.MaxValue;

            for (int i = start; i < end; i++)
            {
                double distance = MetricFunction.GetDistance(Network.Neurons[i].Weights, dataVector);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = i;
                }
            }
            Monitor.Enter(this);
            BestMatchingNeurons[result] = minDistance;
            Monitor.Exit(this);
        }

        private void AsynchronousFindBestMatchingNeuron(object request)
        {
            var findRequest = (FindBestNeuronRequest)request;
            this.ScheduleFindBestMatchingNeuron(findRequest.Start, findRequest.End, findRequest.DataVector);
            findRequest.DoneEvent.Set();
        }


        private class AccomodateNetworkRequest
        {
            public int Iteration { get; private set; }
            public double Radius { get; private set; }
            public int Start { get; private set; }
            public int End { get; private set; }
            public Dictionary<int, double> EffectedNeurons { get; private set; }
            public double[] DataVector { get; private set; }
            public ManualResetEvent DoneEvent { get; private set; }

            public AccomodateNetworkRequest(int iteration, double radius, int start, int end, Dictionary<int, double> effectedNeurons, double[] dataVector, ManualResetEvent doneEvent)
            {
                Iteration = iteration;
                Radius = radius;
                Start = start;
                End = end;
                EffectedNeurons = effectedNeurons;
                DataVector = dataVector;
                DoneEvent = doneEvent;
            }
        }

        private class FindBestNeuronRequest
        {
            public int Start { get; private set; }
            public int End { get; private set; }
            public double[] DataVector { get; private set; }
            public ManualResetEvent DoneEvent { get; private set; }
            public FindBestNeuronRequest(int start, int end, double[] dataVector, ManualResetEvent doneEvent)
            {
                Start = start;
                End = end;
                DataVector = dataVector;
                DoneEvent = doneEvent;
            }
        }
        
    }
}