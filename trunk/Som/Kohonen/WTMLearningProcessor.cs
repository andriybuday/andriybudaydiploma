using System.Collections.Generic;
using Som.Data.Suffle;
using Som.Learning;
using Som.Data;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.Kohonen
{
    public class WTMLearningProcessor : LearningProcessorBase
    {
        public WTMLearningProcessor(ILearningDataProvider learningDataProvider, INetwork network, ITopology topology, IMetricFunction metricFunction, ILearningFactorFunction learningFactorFunction, INeighbourhoodFunction neighbourhoodFunction, int maxIterationsCount, ISuffleProvider suffleProvider)
            : base(learningDataProvider, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, maxIterationsCount, suffleProvider)
        {
        }

        protected override void AccommodateNetworkWeights(int bestNeuronNum, IList<double> dataVector, int iteration)
        {
            var radius = RadiusProvider.GetRadius(iteration);
            var effectedNeurons = Topology.GetNeuronsInRadius(bestNeuronNum, radius);

            foreach (var effectedNeuron in effectedNeurons.Keys)
            {
                var distance = effectedNeurons[effectedNeuron];

                AccommodateNeuronWeights(effectedNeuron, dataVector, iteration, distance, radius);    
            }
        }

        protected override void AccommodateNeuronWeights(int neuronNumber, IList<double> dataVector, int iteration, double distance, double radius)
        {
            var neuronWghts = Network.Neurons[neuronNumber].Weights;

            var learningRate = LearningFactorFunction.GetLearningRate(iteration);
            var falloffRate = NeighbourhoodFunction.GetDistanceFalloff(distance, radius);
            
            for (int i = 0; i < neuronWghts.Count; i++)
            {
                double weight = neuronWghts[i];
                neuronWghts[i] += learningRate * falloffRate * (dataVector[i] - weight);
            }
        }
    }
}