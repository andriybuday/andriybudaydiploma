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
    public class LearningProcessorBase
    {

        public delegate void LearningProcessingHandler<Arg>(Object sender, Arg e);
        public event LearningProcessingHandler<NewEpochStartedEvenArgs> NewEpochStarted;


        protected ISuffleProvider SuffleProvider { get; set; }
        protected ILearningDataProvider LearningDataProvider { get; set; }
        public INetwork Network { get; set; }
        public ITopology Topology { get; set; }
        protected IRadiusProvider RadiusProvider { get; private set; }
        protected INeighbourhoodFunction NeighbourhoodFunction { get; set; }
        protected IMetricFunction MetricFunction { get; set; }
        protected ILearningFactorFunction LearningFactorFunction { get; set; }
        

        protected int MaxIterationsCount { get; set; }

        public LearningProcessorBase(ILearningDataProvider learningDataProvider, INetwork network, ITopology topology, IMetricFunction metricFunction, ILearningFactorFunction learningFactorFunction, INeighbourhoodFunction neighbourhoodFunction, int maxIterationsCount, ISuffleProvider suffleProvider)
        {
            LearningDataProvider = learningDataProvider;
            Network = network;
            Topology = topology;
            MetricFunction = metricFunction;
            LearningFactorFunction = learningFactorFunction;
            NeighbourhoodFunction = neighbourhoodFunction;
            MaxIterationsCount = maxIterationsCount;
            SuffleProvider = suffleProvider;

            RadiusProvider = new DefaultRadiusProvider(maxIterationsCount, topology.WholeTopologyRadius);
        }

        public void Learn()
        {
            int vectorsCount = LearningDataProvider.LearningVectorsCount;
            IList<int> suffleList = new SuffleList(vectorsCount);

            for (int iteration = 0; iteration < MaxIterationsCount; iteration++)
            {
                suffleList = SuffleProvider.Suffle(suffleList);
                OnNewEpochStarted(iteration);

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
            AccommodateNeuronWeights(bestNeuronNum, dataVector, iteration, 0, 1);
        }

        protected virtual void AccommodateNeuronWeights(int neuronNumber, IList<double> dataVector, int iteration, double distance, double radius)
        {
            var bstNeuronWghts = Network.Neurons[neuronNumber].Weights;
            var factorValue = LearningFactorFunction.GetLearningRate(iteration);
            for (int i = 0; i < bstNeuronWghts.Count; i++)
            {
                double weight = bstNeuronWghts[i];
                bstNeuronWghts[i] += factorValue * (dataVector[i] - weight);
            }
        }

        private void OnNewEpochStarted(int iteration)
        {
            if(NewEpochStarted != null)
            {
                NewEpochStarted(this, new NewEpochStartedEvenArgs(iteration));
            }
        }
    }

    public class NewEpochStartedEvenArgs : EventArgs
    {
        public int Iteration { get; private set; }

        public NewEpochStartedEvenArgs(int iteration)
        {
            Iteration = iteration;
        }
    }
}