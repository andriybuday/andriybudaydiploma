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
            var effectedNeurons = Topology.GetNeuronsInRadius(bestNeuronNum);
            var bestNeuronWgs = Network.Neurons[bestNeuronNum].Weights;
            foreach (var effectedNeuron in effectedNeurons.Keys)
            {
                var distance = MetricFunction.GetDistance(bestNeuronWgs, Network.Neurons[effectedNeuron].Weights);
                //var distance = effectedNeurons[effectedNeuron];
                //var distance = 1;

                AccommodateNeuronWeights(effectedNeuron, dataVector, iteration, distance);    
            }
        }

        protected override void AccommodateNeuronWeights(int bestNeuronNum, IList<double> dataVector, int iteration, double distance)
        {
            var bstNeuronWghts = Network.Neurons[bestNeuronNum].Weights;
            var factorValue = LearningFactorFunction.GetResult(iteration);
            var neighboCoef = NeighbourhoodFunction.GetResult(distance);
            for (int i = 0; i < bstNeuronWghts.Count; i++)
            {
                double weight = bstNeuronWghts[i];
                bstNeuronWghts[i] += factorValue * neighboCoef * (dataVector[i] - weight);
            }
        }
    }
}