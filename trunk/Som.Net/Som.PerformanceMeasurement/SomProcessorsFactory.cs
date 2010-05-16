using System;
using System.Collections.Generic;
using Som.ActivationFunction;
using Som.Concurrency;
using Som.Data;
using Som.Data.Shuffle;
using Som.Learning;
using Som.LearningProcessor;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace PerformanceMeasurement
{
    public class SomProcessorsFactory
    {
        private EuclideanMetricFunction metricFunction;
        private ILearningFactorFunction learningFactorFunction;
        private INeighbourhoodFunction neighbourhoodFunction;
        private IShuffleProvider shuffleProvider;
        protected IActivationFunction ActivationFunction { get; set; }
        public int Iterations { get; private set; }
        public int Dimentions { get; private set; }
        public int GridOneSideSize { get; private set; }
        public double StartLearningRate { get; private set; }
        protected List<double> MaxWeights { get; set; }

        ILearningDataProvider LearningDataProvider { get; set; }
        INetwork Network { get; set; }

        public SomProcessorsFactory(int iterations, int dimentions, int gridOneSideSize, double startLearningRate, bool fromFile, string fileName)
        {
            Iterations = iterations;
            Dimentions = dimentions;
            GridOneSideSize = gridOneSideSize;
            StartLearningRate = startLearningRate;

            if (fromFile)
            {
                ILearningDataPersister learningDataPersister = new TextFileLearningDataPersister(fileName);
                LearningDataProvider = new LearningDataProvider(learningDataPersister);
            }
            else
            {
                LearningDataProvider = new CompletelyRandomDataProvider(dimentions, 150);
            }

            int dataVectorDimention = LearningDataProvider.DataVectorDimention;
            MaxWeights = new List<double>();
            for (int i = 0; i < dataVectorDimention; i++)
            {
                MaxWeights.Add(1);
            }

            ITopology topology = new SimpleMatrixTopology(gridOneSideSize, gridOneSideSize);

            ActivationFunction = new TransparentActivationFunction(new double[] { });

            Network = new NetworkBase(true, MaxWeights, ActivationFunction, topology);

            metricFunction = new EuclideanMetricFunction();
            learningFactorFunction = new ExponentionalFactorFunction(startLearningRate, iterations);
            neighbourhoodFunction = new GaussNeighbourhoodFunction();
            shuffleProvider = new NotShufflingProvider();
        }




        public ILearningProcessor GetStandardLearningProcessor()
        {
            ILearningProcessor somLearningProcessor = new SomLearningProcessor(LearningDataProvider, Network, metricFunction, learningFactorFunction, neighbourhoodFunction, Iterations, shuffleProvider);

            return somLearningProcessor;
        }

        public ILearningProcessor GetParallelLearningProcessor(int gridGroups)
        {
            //ConcurrentSimpleMatrixTopology localTopology = new ConcurrentSimpleMatrixTopology(GridOneSideSize, GridOneSideSize);
            //INetwork specialNetwork = new NetworkBase(true, MaxWeights, ActivationFunction, localTopology);

            //ILearningProcessor somLearningProcessor = new DivideGridAndAccomodationArea(LearningDataProvider, specialNetwork, metricFunction, learningFactorFunction, neighbourhoodFunction, Iterations, shuffleProvider, gridGroups);
            //return somLearningProcessor;

            ILearningProcessor somLearningProcessor = new DivideGridAndAccomodationArea(LearningDataProvider, Network, metricFunction, learningFactorFunction, neighbourhoodFunction, Iterations, shuffleProvider, gridGroups);
            return somLearningProcessor;
        }

        public ILearningProcessor GetTrainsProcessor(int gridGroups)
        {

            ILearningProcessor somLearningProcessor = new TrainsSomProcessor(LearningDataProvider, Network, metricFunction, learningFactorFunction, neighbourhoodFunction, Iterations, shuffleProvider, gridGroups);
            return somLearningProcessor;
        }
    }
}
