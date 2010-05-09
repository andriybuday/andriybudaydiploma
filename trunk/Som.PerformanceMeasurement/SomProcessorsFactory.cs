using System.Collections.Generic;
using Som.ActivationFunction;
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
        public int Iterations { get; private set; }
        public int Dimentions { get; private set; }
        public int GridOneSideSize { get; private set; }
        public double StartLearningRate { get; private set; }

        ILearningDataProvider LearningDataProvider { get; set; }
        INetwork Network { get; set; }

        public SomProcessorsFactory(int iterations, int dimentions, int gridOneSideSize, double startLearningRate)
        {
            Iterations = iterations;
            Dimentions = dimentions;
            GridOneSideSize = gridOneSideSize;
            StartLearningRate = startLearningRate;


            ILearningDataPersister learningDataPersister = new TextFileLearningDataPersister("iris.data");
            LearningDataProvider = new LearningDataProvider(learningDataPersister);


            int dataVectorDimention = LearningDataProvider.DataVectorDimention;
            List<double> maxWeights = new List<double>();
            for (int i = 0; i < dataVectorDimention; i++)
            {
                maxWeights.Add(1);
            }

            ITopology topology = new SimpleMatrixTopology(gridOneSideSize, gridOneSideSize);

            IActivationFunction activationFunction = new TransparentActivationFunction(new double[] { });

            Network = new NetworkBase(true, maxWeights, activationFunction, topology);

            metricFunction = new EuclideanMetricFunction();
            learningFactorFunction = new ExponentionalFactorFunction(startLearningRate, iterations);
            neighbourhoodFunction = new GaussNeighbourhoodFunction();
            shuffleProvider = new NotShufflingProvider();
        }

        public SomLearningProcessor GetStandardLearningProcessor()
        {
            SomLearningProcessor somLearningProcessor = new SomLearningProcessor(LearningDataProvider, Network, metricFunction, learningFactorFunction, neighbourhoodFunction, Iterations, shuffleProvider);

            return somLearningProcessor;
        }

        public SomLearningProcessor GetParallelLearningProcessor()
        {
            SomLearningProcessor somLearningProcessor = new ConcurrencySomLearningProcessor(LearningDataProvider, Network, metricFunction, learningFactorFunction, neighbourhoodFunction, Iterations, shuffleProvider);
            return somLearningProcessor;
        }
    }
}
