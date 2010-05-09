using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
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
            SaveTestResults(testResults);
            Console.WriteLine("End.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private void SaveTestResults(List<TestResult> testResults)
        {
            string fileName = string.Format("ResultsFor_{0}_{1}_{2}_{3}_{4}.xml", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);
            var streamWriter = File.CreateText(fileName);
            var xmlSerializer = new XmlSerializer(typeof (List<TestResult>));
            xmlSerializer.Serialize(streamWriter,testResults);
            streamWriter.Close();
        }

        private TestResult GetOneTestResult(int gridSideSize, int dimention)
        {
            var testResult = new TestResult(Iterations, string.Format("{0}X{1}", gridSideSize, gridSideSize), dimention);

            var somProcessorsFactory = new SomProcessorsFactory(Iterations,dimention, gridSideSize, 0.07);

            SomLearningProcessor somLearningProcessor = somProcessorsFactory.GetStandardLearningProcessor(Iterations, 0.07, gridSideSize, dimention);
            var timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            testResult.TimeStat.Add(new AlgorithmTime("Standard", timeForAlgo));
            Console.WriteLine("Standard:{0}", timeForAlgo);

            somLearningProcessor = somProcessorsFactory.GetParallelLearningProcessor(Iterations, 0.07, gridSideSize, dimention);
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


    }
}
