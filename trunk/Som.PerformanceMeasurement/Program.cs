using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Som.ActivationFunction;
using Som.Data;
using Som.Learning;
using Som.LearningProcessor;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace PerformanceMeasurement
{
    public class Program
    {
        public static void Main(String[] args)
        {
            Program program = new Program();
            program.Run();

        }

        private void Run()
        {
            ReadConfiguration();

            List<TestResult> testResults = new List<TestResult>();

            Console.WriteLine("Start...");
            foreach (int gridSideSize in Grids)
            {
                Console.WriteLine("Grid {0}X{0}...", gridSideSize);    
                foreach (int dimention in Dimentions)
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
            StreamWriter streamWriter = File.CreateText(fileName);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TestResult>));
            xmlSerializer.Serialize(streamWriter,testResults);
            streamWriter.Close();
        }

        private TestResult GetOneTestResult(int gridSideSize, int dimention)
        {
            TestResult testResult = new TestResult(Iterations, string.Format("{0}X{1}", gridSideSize, gridSideSize), dimention);

            SomProcessorsFactory somProcessorsFactory = new SomProcessorsFactory(Iterations, dimention, gridSideSize, 0.07);

            SomLearningProcessor somLearningProcessor = somProcessorsFactory.GetStandardLearningProcessor(Iterations, 0.07, gridSideSize, dimention);
            double timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
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
            double averageTime = 0.0;
            for (int i = 0; i < Tests; i++)
            {
                double timeForStandardAlgo = GetTimeForExecutingLearn(somLearningProcessor);
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
            string dimentionsString = ConfigurationSettings.AppSettings["Dimentions"];
            string[] dimentions = dimentionsString.Split(',');
            Dimentions = new List<int>();
            foreach (string dimention in dimentions)
            {
                Dimentions.Add(Int32.Parse(dimention));
            }
            string gridsString = ConfigurationSettings.AppSettings["Grids"];
            string[] grids = gridsString.Split(',');
            Grids = new List<int>();
            foreach (string grid in grids)
            {
                Grids.Add(Int32.Parse(grid));
            }
        }


    }
}
