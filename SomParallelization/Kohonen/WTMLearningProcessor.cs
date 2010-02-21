using System.Collections.Generic;
using SomParallelization.Learning;
using SomParallelization.LearningData;
using SomParallelization.Metrics;
using SomParallelization.Network;
using SomParallelization.Topology;

namespace SomParallelization.Kohonen
{
    public class WTMLearningProcessor : LearningProcessorBase
    {
        public WTMLearningProcessor(ILearningData learningData, INetwork network, ITopology topology, IMetricFunction metricFunction, ILearningFactorFunction learningFactorFunction, INeighbourhoodFunction neighbourhoodFunction, int maxIterationsCount)
            : base(learningData, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, maxIterationsCount)
        {
        }

        protected override void AccommodateNetworkWeights(int bestNeuronNum, IList<double> dataVector, int iteration)
        {
            var effectedNeurons = Topology.GetNeuronsInRadius(bestNeuronNum);
            var bestNeuronWgs = Network.Neurons[bestNeuronNum].Weights;
            foreach (var effectedNeuron in effectedNeurons)
            {
                var distance = MetricFunction.GetDistance(bestNeuronWgs, Network.Neurons[effectedNeuron].Weights);
                AccommodateNeuronWeights(effectedNeuron, dataVector, iteration, distance);    
            }
        }

        protected override void AccommodateNeuronWeights(int neuronNum, IList<double> dataVector, int iteration, double distance)
        {
            var neuronWeights = Network.Neurons[neuronNum].Weights;
            var factorValue = LearningFactorFunction.GetResult(iteration);
            var neighboCoef = NeighbourhoodFunction.GetResult(distance);
            for (int i = 0; i < neuronWeights.Count; i++)
            {
                double weight = neuronWeights[i];
                neuronWeights[i] += factorValue * neighboCoef * (dataVector[i] - weight);
            }
        }
    }
}