using System;
using System.Collections.Generic;
using System.Threading;
using Som.Data;
using Som.Data.Suffle;
using Som.Learning;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.KohonenLearningProcessor
{

    //what do I want to see here?

    // first of all I want to have two threads..
    // I want to know which of two threads executed first
    // see if there exists overlapping area..
    // if NO - run AccommodateNetworkWeights in two separate threads.
    // if YES - run AccommodateNetworkWeights of the first winner...
    //          make Second to be master and run Accommodate for it..

    public class ConcurrencyLearningProcessor: LearningProcessor
    {
        public int GridDivideNumber { get; private set; }
        private Dictionary<int, double> BestMatchingNeurons { get; set; }
        private ManualResetEvent[] DoneEvents { get; set; }

        public ConcurrencyLearningProcessor(ILearningDataProvider learningDataProvider, INetwork network, ITopology topology, IMetricFunction metricFunction, ILearningFactorFunction learningFactorFunction, INeighbourhoodFunction neighbourhoodFunction, int maxIterationsCount, ISuffleProvider suffleProvider) : base(learningDataProvider, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, maxIterationsCount, suffleProvider)
        {
            GridDivideNumber = 4;
            DoneEvents = new ManualResetEvent[GridDivideNumber];
        }

        protected override int FindBestMatchingNeuron(double[] dataVector)
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
