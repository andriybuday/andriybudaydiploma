using System;
using System.Collections.Generic;
using System.Threading;
using Som.Data.Suffle;
using Som.Learning;
using Som.Data;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.Kohonen
{
    public class ParallelizationLearningProcessor
    {
        protected ILearningDataProvider LearningDataProvider { get; set; }
        public INetwork Network { get; set; }
        public ITopology Topology { get; set; }
        protected IMetricFunction MetricFunction { get; set; }
        protected ILearningFactorFunction LearningFactorFunction { get; set; }
        protected INeighbourhoodFunction NeighbourhoodFunction { get; set; }

        protected int MaxIterationsCount { get; set; }

        public ParallelizationLearningProcessor(
            ILearningDataProvider learningDataProvider,
            INetwork network,
            ITopology topology,
            IMetricFunction metricFunction,
            ILearningFactorFunction learningFactorFunction,
            INeighbourhoodFunction neighbourhoodFunction,
            int maxIterationsCount)
        {
            LearningDataProvider = learningDataProvider;
            Network = network;
            Topology = topology;
            MetricFunction = metricFunction;
            LearningFactorFunction = learningFactorFunction;
            NeighbourhoodFunction = neighbourhoodFunction;
            MaxIterationsCount = maxIterationsCount;
        }

        public void Learn()
        {
            //what do I want to see here?

            // first of all I want to have two threads..
            // I want to know which of two threads executed first
            // see if there exists overlapping area..
            // if NO - run AccommodateNetworkWeights in two separate threads.
            // if YES - run AccommodateNetworkWeights of the first winner...
            //          make Second to be master and run Accommodate for it..
            int vectorsCount = LearningDataProvider.LearningVectorsCount;
            var suffleList = new SuffleList(vectorsCount);

            for (int iteration = 0; iteration < MaxIterationsCount; iteration++)
            {
                suffleList.Suffle();

                for (int dataInd = 0; dataInd < vectorsCount; dataInd++)
                {
                    IList<double> dataVector = LearningDataProvider.GetLearingDataVector(suffleList[dataInd]);

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