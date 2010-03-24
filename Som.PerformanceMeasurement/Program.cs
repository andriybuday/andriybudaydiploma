using System;
using System.Collections.Generic;
using System.Diagnostics;
using Som.ActivationFunction;
using Som.Data;
using Som.Data.Suffle;
using Som.KohonenLearningProcessor;
using Som.Learning;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.PerformanceMeasurement
{
    class Program
    {
        static void Main(string[] args)
        {
            MeasurePerformance();
            //SecondaryMeasurements();
        }

        private static void MeasurePerformance()
        {
            LearningProcessor learningProcessor = GetStandardLearningProcessor();

            //Console.WriteLine("To start press any key...."); Console.ReadKey();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            learningProcessor.Learn();

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            Console.WriteLine(string.Format("Learning took {0}H:{1}M:{2}S:{3}ms",ts.Hours,ts.Minutes,ts.Seconds,ts.Milliseconds));
        }

        
        private static void SecondaryMeasurements()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            Console.WriteLine(string.Format("took {0}H:{1}M:{2}S:{3}ms", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds));
        }

        private static LearningProcessor GetStandardLearningProcessor()
        {
            var iterations = 200000;
            var startLearningRate = 0.07;

            var wh = 10;

            //learning data
            ILearningDataProvider LearningDataProvider = new CompletelyRandomDataProvider(5, 1);

            var dataVectorDimention = LearningDataProvider.DataVectorDimention;
            var maxWeights = new List<double>();
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
            ISuffleProvider suffleProvider = new NotSufflingProvider();
            //LearningProcessor learningProcessor = new LearningProcessor(LearningDataProvider, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, suffleProvider);
            LearningProcessor learningProcessor = new ConcurrencyLearningProcessor(LearningDataProvider, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, suffleProvider);

            return learningProcessor;
        }
    }
}
