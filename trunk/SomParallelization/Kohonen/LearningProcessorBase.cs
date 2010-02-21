using System;
using System.Collections.Generic;
using SomParallelization.Learning;
using SomParallelization.LearningData;
using SomParallelization.Metrics;
using SomParallelization.Network;
using SomParallelization.Topology;

namespace SomParallelization.Kohonen
{
    public class LearningProcessorBase
    {
        protected ILearningData LearningData { get; set; }
        public INetwork Network { get; set; }
        public ITopology Topology { get; set; }
        protected IMetricFunction MetricFunction { get; set; }
        protected ILearningFactorFunction LearningFactorFunction { get; set; }
        protected INeighbourhoodFunction NeighbourhoodFunction { get; set; }

        protected int MaxIterationsCount { get; set; }

        public LearningProcessorBase(
            ILearningData learningData,
            INetwork network,
            ITopology topology,
            IMetricFunction metricFunction,
            ILearningFactorFunction learningFactorFunction,
            INeighbourhoodFunction neighbourhoodFunction,
            int maxIterationsCount)
        {
            LearningData = learningData;
            Network = network;
            Topology = topology;
            MetricFunction = metricFunction;
            LearningFactorFunction = learningFactorFunction;
            NeighbourhoodFunction = neighbourhoodFunction;
            MaxIterationsCount = maxIterationsCount;
        }

        public void Learn()
        {
            int vectorsCount = LearningData.LearningVectorsCount;
            var suffleList = new SuffleList(vectorsCount);

            for (int iteration = 0; iteration < MaxIterationsCount; iteration++)
            {
                suffleList.Suffle();

                for (int dataInd = 0; dataInd < vectorsCount; dataInd++)
                {
                    IList<double> dataVector = LearningData.GetLearingDataVector(suffleList[dataInd]);

                    int bestNeuronNum = FindBestMatchingNeuron(dataVector);

                    AccommodateNetworkWeights(bestNeuronNum, dataVector, iteration);
                }
            }
        }


        protected virtual int FindBestMatchingNeuron(IList<double> dataVector)
        {
            int result = -1;
            Double minDistance = Double.MaxValue;
            for (int i = 0; i < Network.Neurons.Count; i++)
            {
                double distance = MetricFunction.GetDistance(Network.Neurons[i].Weights, dataVector);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = i;
                }
            }
            return result;
        }

        protected virtual void AccommodateNetworkWeights(int bestNeuronNum, IList<double> dataVector, int iteration)
        {
            AccommodateNeuronWeights(bestNeuronNum, dataVector, iteration, 0);
        }

        protected virtual void AccommodateNeuronWeights(int neuronNum, IList<double> dataVector, int iteration, double distance)
        {
            var neuronWeights = Network.Neurons[neuronNum].Weights;
            var factorValue = LearningFactorFunction.GetResult(iteration);
            for (int i = 0; i < neuronWeights.Count; i++)
            {
                double weight = neuronWeights[i];
                neuronWeights[i] += factorValue * (dataVector[i] - weight);
            }
        }
    }
}