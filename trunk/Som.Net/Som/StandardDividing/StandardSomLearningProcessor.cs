using System;
using System.Collections.Generic;
using Som.Data.Shuffle;
using Som.Learning;
using Som.Data;
using Som.LearningProcessor;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.StandardDividing
{

    public class StandardSomLearningProcessor : ILearningProcessor
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

        public StandardSomLearningProcessor(
            ILearningDataProvider learningDataProvider,
            INetwork network,
            IMetricFunction metricFunction,
            ILearningFactorFunction learningFactorFunction,
            INeighbourhoodFunction neighbourhoodFunction,
            int maxIterationsCount,
            IShuffleProvider shuffleProvider)
        {
            LearningDataProvider = learningDataProvider;
            Network = network;
            Topology = network.Topology;
            //TODO
            // this is super crap stuff
            ColCount = Topology.ColCount;
            RowCount = Topology.RowCount;
            _n = RowCount*ColCount;
            DistancesToWinner = new double[NeuronsCount];
            WholeTopologyRadius = Math.Max(ColCount, RowCount) / 2.0;
            //
            MetricFunction = metricFunction;
            LearningFactorFunction = learningFactorFunction;
            NeighbourhoodFunction = neighbourhoodFunction;
            MaxIterationsCount = maxIterationsCount;
            ShuffleProvider = shuffleProvider;

            RadiusProvider = new DefaultRadiusProvider(maxIterationsCount, Topology.WholeTopologyRadius);
        }

        public virtual void Learn()
        {
            int vectorsCount = LearningDataProvider.LearningVectorsCount;
            IList<int> suffleList = new ShuffleList(vectorsCount);

            for (int iteration = 0; iteration < MaxIterationsCount; iteration++)
            {
                //if ((iteration % 1000) == 0) Console.Write(string.Format("{0} ", iteration));
                suffleList = ShuffleProvider.Suffle(suffleList);

                for (int dataInd = 0; dataInd < vectorsCount; dataInd++)
                {
                    double[] dataVector = LearningDataProvider.GetLearingDataVector(suffleList[dataInd]);

                    int bestNeuronNum = FindBestMatchingNeuron(dataVector);

                    AccommodateNetworkWeights(bestNeuronNum, dataVector, iteration);
                }
            }
        }

        public virtual int FindBestMatchingNeuron(double[] dataVector)
        {
            int result = -1;
            Double minDistance = Double.MaxValue;
            for (int i = 0; i < Network.Neurons.Length; i++)
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

        public virtual void AccommodateNetworkWeights(int bestNeuronNum, double[] dataVector, int iteration)
        {
            var radius = RadiusProvider.GetRadius(iteration);

            int n = Network.Neurons.Length;
            for (int i = 0; i < n; i++)
            {
                var distance = GetDistance(bestNeuronNum, i);
                if(distance < radius)
                {
                    AccommodateNeuronWeights(i, dataVector, iteration, distance, radius);    
                }
            }
        }

        protected virtual void AccommodateNeuronWeights(int neuronNumber, double[] dataVector, int iteration, double distance, double radius)
        {
            var neuronWghts = Network.Neurons[neuronNumber].Weights;

            var learningRate = LearningFactorFunction.GetLearningRate(iteration);
            var falloffRate = NeighbourhoodFunction.GetDistanceFalloff(distance, radius);

            for (int i = 0; i < neuronWghts.Length; i++)
            {
                double weight = neuronWghts[i];
                neuronWghts[i] += learningRate * falloffRate * (dataVector[i] - weight);
            }
        }

        #region TopologyStuff
        private int _n;
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public double WholeTopologyRadius { get; private set; }
        public double[] DistancesToWinner { get; private set; }

        public int NeuronsCount { get { return _n; } }

        private double GetDistance(int firstNeuronPos, int secondNeuronPos)
        {
            var fColPos = firstNeuronPos % ColCount;
            var fRowPos = firstNeuronPos / ColCount;
            var sColPos = secondNeuronPos % ColCount;
            var sRowPos = secondNeuronPos / ColCount;
            var xD = (fColPos - sColPos);
            var yD = (fRowPos - sRowPos);
            return Math.Sqrt(xD * xD + yD * yD);
        }
        #endregion 
    }

}