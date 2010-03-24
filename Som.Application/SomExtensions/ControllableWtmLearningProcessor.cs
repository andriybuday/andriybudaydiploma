using System.Collections.Generic;
using Som.Data;
using Som.Data.Suffle;
using Som.KohonenLearningProcessor;
using Som.Learning;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.Application.SomExtensions
{
    public class ControllableWtmLearningProcessor : ConcurrencyLearningProcessor
    {
        public int Iteration { get; private set; }
        public int BmnIndex { get; private set; }
        public IList<double> BestMatchingNeuronWeights { get; private set; }
        public double[] DataVector { get; private set; }

        public ControllableWtmLearningProcessor(ILearningDataProvider learningDataProvider, INetwork network, ITopology topology, IMetricFunction metricFunction, ILearningFactorFunction learningFactorFunction, INeighbourhoodFunction neighbourhoodFunction, int maxIterationsCount, ISuffleProvider suffleProvider)
            : base(learningDataProvider, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, maxIterationsCount, suffleProvider)
        {
            Reset();
        }

        public void Reset()
        {
            Iteration = 0;
        }

        public void Next(double[] dataVector)
        {
            DataVector = dataVector;
            BmnIndex = FindBestMatchingNeuron(dataVector);
            BestMatchingNeuronWeights = Network.Neurons[BmnIndex].Weights;
            AccommodateNetworkWeights(BmnIndex, dataVector, Iteration);
        }

        public void IncrementIteration()
        {
            Iteration++;            
        }

    }
}
