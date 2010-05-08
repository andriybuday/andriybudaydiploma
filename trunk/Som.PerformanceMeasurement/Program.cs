using System;
using System.Collections.Generic;
using System.Diagnostics;
using Som.ActivationFunction;
using Som.Data;
using Som.Data.Suffle;
using Som.Learning;
using Som.LearningProcessor;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.PerformanceMeasurement
{
    class Program
    {
        static void Main(string[] args)
        {

            int dimentions = 5;
            int iterations = 20000;
            double startLearningRate = 0.07;
            int wh = 100;
            if (args.Length == 1)
            {
                Console.WriteLine("PARAMS_ARE: iterations width dimentinos");
                Console.ReadKey();
                return;
            }
            if (args.Length == 3)
            {
                iterations = Convert.ToInt32(args[0]);
                wh = Convert.ToInt32(args[1]);
                dimentions = Convert.ToInt32(args[2]);
            }


            MeasurePerformance(iterations, startLearningRate, wh, dimentions);
            //SecondaryMeasurements();
        }

        private static void MeasurePerformance(int iterations, double startLearningRate, int wh, int dimentions)
        {
            var ts1 = MeasureStandardLearn(iterations, startLearningRate, wh, dimentions);
            Console.WriteLine();
            var ts2 = MeasureConcurrentLearn(iterations, startLearningRate, wh, dimentions);
            Console.WriteLine();

            Console.WriteLine(string.Format("Standard learning took {0}H:{1}M:{2}S:{3}ms", ts1.Hours, ts1.Minutes, ts1.Seconds, ts1.Milliseconds));
            Console.WriteLine(string.Format("Concurrent learning took {0}H:{1}M:{2}S:{3}ms", ts2.Hours, ts2.Minutes, ts2.Seconds, ts2.Milliseconds));

            var standardMS = ts1.TotalMilliseconds;
            var concurrentMS = ts2.TotalMilliseconds;
            var diff = (standardMS - concurrentMS);

            Console.WriteLine(string.Format("{0}%", Math.Round(diff * 100 / standardMS, 2)));
        }

        private static TimeSpan MeasureStandardLearn(int iterations, double startLearningRate, int wh, int dimentions)
        {
            SomLearningProcessor somLearningProcessor = GetStandardLearningProcessor(iterations, startLearningRate, wh, dimentions);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            somLearningProcessor.Learn();

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        private static TimeSpan MeasureConcurrentLearn(int iterations, double startLearningRate, int wh, int dimentions)
        {
            SomLearningProcessor somLearningProcessor = GetParallelLearningProcessor(iterations, startLearningRate, wh, dimentions);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            somLearningProcessor.Learn();

            stopWatch.Stop();
            return stopWatch.Elapsed;

        }


        private static void SecondaryMeasurements()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            Console.WriteLine(string.Format("took {0}H:{1}M:{2}S:{3}ms", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds));
        }

        private static SomLearningProcessor GetStandardLearningProcessor(int iterations, double startLearningRate, int wh, int dimentions)
        {

            //learning data
            ILearningDataProvider LearningDataProvider = new CompletelyRandomDataProvider(dimentions, 1);

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
            IShuffleProvider shuffleProvider = new NotShufflingProvider();
            SomLearningProcessor somLearningProcessor = new SomLearningProcessor(LearningDataProvider, network, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, shuffleProvider);

            return somLearningProcessor;
        }

        private static SomLearningProcessor GetParallelLearningProcessor(int iterations, double startLearningRate, int wh, int dimentions)
        {

            //learning data
            ILearningDataProvider LearningDataProvider = new CompletelyRandomDataProvider(dimentions, 1);

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
            IShuffleProvider shuffleProvider = new NotShufflingProvider();
            //somLearningProcessor somLearningProcessor = new somLearningProcessor(LearningDataProvider, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, shuffleProvider);
            SomLearningProcessor somLearningProcessor = new ConcurrencySomLearningProcessor(LearningDataProvider, network, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, shuffleProvider);

            return somLearningProcessor;
        }
    }
}
