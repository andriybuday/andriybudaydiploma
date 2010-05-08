using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class TestResult
    {
        public TestResult(int iterations, string gridSize, int inputSpaceDimentions)
        {
            Iterations = iterations;
            GridSize = gridSize;
            InputSpaceDimentions = inputSpaceDimentions;
            TimeStat = new List<AlgorithmTime>();
        }

        public int Iterations { get; private set; }
        public string GridSize { get; private set; }
        public int InputSpaceDimentions { get; private set; }
        public List<AlgorithmTime> TimeStat { get; set; }
    }

    public class AlgorithmTime
    {
        public AlgorithmTime(string algoName, double aveTime)
        {
            AlgoName = algoName;
            AverageTime = aveTime;
        }

        public string AlgoName { get; private set; }
        public double AverageTime { get; private set; }
    }

    public class Program
    {
        public static void Main(String[] args)
        {
            var program = new Program();
            program.Run();

        }

        private void Run()
        {
            ReadConfiguration();

            var testResults = new List<TestResult>();

            Console.WriteLine("Start...");
            foreach (var gridSideSize in Grids)
            {
                Console.WriteLine("Grid {0}X{0}...", gridSideSize);    
                foreach (var dimention in Dimentions)
                {
                    Console.WriteLine("Dimentin {0}...", dimention);
                    var testResult = GetOneTestResult(gridSideSize, dimention);
                    testResults.Add(testResult);
                }
            }
            Console.WriteLine("End.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private TestResult GetOneTestResult(int gridSideSize, int dimention)
        {
            var testResult = new TestResult(Iterations, string.Format("{0}X{1}", gridSideSize, gridSideSize), dimention);

            SomLearningProcessor somLearningProcessor = GetStandardLearningProcessor(Iterations, 0.07, gridSideSize, dimention);
            var timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            testResult.TimeStat.Add(new AlgorithmTime("Standard", timeForAlgo));
            Console.WriteLine("Standard:{0}", timeForAlgo);

            somLearningProcessor = GetParallelLearningProcessor(Iterations, 0.07, gridSideSize, dimention);
            timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            testResult.TimeStat.Add(new AlgorithmTime("Paralleled", timeForAlgo));
            Console.WriteLine("Paralleled:{0}", timeForAlgo);

            return testResult;
        }

        private double GetAverageTimeForAlgo(ILearningProcessor somLearningProcessor, TestResult testResult)
        {
            double mult = 1.0/Tests;
            var averageTime = 0.0;
            for (int i = 0; i < Tests; i++)
            {
                var timeForStandardAlgo = GetTimeForExecutingLearn(somLearningProcessor);
                averageTime += timeForStandardAlgo * mult;
            }
            return averageTime;
        }

        private double GetTimeForExecutingLearn(ILearningProcessor learningProcessor)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            learningProcessor.Learn();

            stopWatch.Stop();
            return stopWatch.Elapsed.TotalMilliseconds;
        }

        public int Tests { get; private set; }
        public int Iterations { get; private set; }
        public List<int> Dimentions { get; private set; }
        public List<int> Grids { get; private set; }

        private void ReadConfiguration()
        {
            Tests = Int32.Parse(ConfigurationSettings.AppSettings["Tests"]);
            Iterations = Int32.Parse(ConfigurationSettings.AppSettings["Iterations"]);
            var dimentionsString = ConfigurationSettings.AppSettings["Dimentions"];
            var dimentions = dimentionsString.Split(',');
            Dimentions = new List<int>();
            foreach (var dimention in dimentions)
            {
                Dimentions.Add(Int32.Parse(dimention));
            }
            var gridsString = ConfigurationSettings.AppSettings["Grids"];
            var grids = gridsString.Split(',');
            Grids = new List<int>();
            foreach (var grid in grids)
            {
                Grids.Add(Int32.Parse(grid));
            }
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
