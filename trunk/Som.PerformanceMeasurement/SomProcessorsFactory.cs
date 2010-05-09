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
        public int Iterations { get; private set; }
        public int Dimentions { get; private set; }
        public int GridOneSideSize { get; private set; }
        public double StartLearningRate { get; private set; }

        public SomProcessorsFactory(int iterations, int dimentions, int gridOneSideSize, double startLearningRate)
        {
            Iterations = iterations;
            Dimentions = dimentions;
            GridOneSideSize = gridOneSideSize;
            StartLearningRate = startLearningRate;
        }

        public SomLearningProcessor GetStandardLearningProcessor(int iterations, double startLearningRate, int wh, int dimentions)
        {

            //learning data
            ILearningDataProvider LearningDataProvider = new CompletelyRandomDataProvider(dimentions, 1);

            int dataVectorDimention = LearningDataProvider.DataVectorDimention;
            List<double> maxWeights = new List<double>();
            for (int i = 0; i < dataVectorDimention; i++)
            {
                maxWeights.Add(1);
            }

            ITopology topology = new SimpleMatrixTopology(wh, wh);

            IActivationFunction activationFunction = new TransparentActivationFunction(new double[] { });

            INetwork network = new NetworkBase(true, maxWeights, activationFunction, topology);

            IMetricFunction metricFunction = new EuclideanMetricFunction();
            ILearningFactorFunction learningFactorFunction = new ExponentionalFactorFunction(startLearningRate, iterations);
            INeighbourhoodFunction neighbourhoodFunction = new GaussNeighbourhoodFunction();
            IShuffleProvider shuffleProvider = new NotShufflingProvider();
            SomLearningProcessor somLearningProcessor = new SomLearningProcessor(LearningDataProvider, network, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, shuffleProvider);

            return somLearningProcessor;
        }

        public SomLearningProcessor GetParallelLearningProcessor(int iterations, double startLearningRate, int wh, int dimentions)
        {

            //learning data
            ILearningDataProvider LearningDataProvider = new CompletelyRandomDataProvider(dimentions, 1);

            int dataVectorDimention = LearningDataProvider.DataVectorDimention;
            List<double> maxWeights = new List<double>();
            for (int i = 0; i < dataVectorDimention; i++)
            {
                maxWeights.Add(1);
            }

            ITopology topology = new SimpleMatrixTopology(wh, wh);

            IActivationFunction activationFunction = new TransparentActivationFunction(new double[] { });

            INetwork network = new NetworkBase(true, maxWeights, activationFunction, topology);

            IMetricFunction metricFunction = new EuclideanMetricFunction();
            ILearningFactorFunction learningFactorFunction = new ExponentionalFactorFunction(startLearningRate, iterations);
            INeighbourhoodFunction neighbourhoodFunction = new GaussNeighbourhoodFunction();
            IShuffleProvider shuffleProvider = new NotShufflingProvider();
            //somLearningProcessor somLearningProcessor = new somLearningProcessor(LearningDataProvider, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, shuffleProvider);
            SomLearningProcessor somLearningProcessor = new ConcurrencySomLearningProcessor(LearningDataProvider, network, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, shuffleProvider);

            return somLearningProcessor;
        }
    }
}
