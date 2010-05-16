using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using Som.LearningProcessor;

namespace PerformanceMeasurement
{
    public class Program
    {
        public static void Main(String[] args)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            //int completionPortThreads;
            //int workerThreads;
            //ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);

            //ThreadPool.SetMaxThreads(50, 1000);
            //ThreadPool.

            // collect garbage now to minimize interference
            GC.Collect();

            Program program = new Program();
            program.Run();
        }

        private void Run()
        {
            ReadConfiguration();

            List<TestResult> testResults = new List<TestResult>();

            Console.WriteLine("Tries to know average: {0}", Tests);
            Console.WriteLine("Iterations: {0}", Iterations);
            if (FromFile) Console.WriteLine("FromFile: {0}", FileName);

            Console.WriteLine();
            Console.WriteLine("Start...");
            Console.WriteLine();

            foreach (int gridSideSize in Grids)
            {
                Console.WriteLine("Grid {0}X{0}...", gridSideSize);    
                foreach (int dimention in Dimentions)
                {
                    if(!FromFile) { Console.WriteLine("Dimentin {0}...", dimention); }

                    var testResult = GetOneTestResult(gridSideSize, dimention);
                    testResults.Add(testResult);
                }
            }
            SaveTestResults(testResults);
            Console.WriteLine("Finished!");
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

            SomProcessorsFactory somProcessorsFactory = new SomProcessorsFactory(Iterations, dimention, gridSideSize, 0.07, FromFile, FileName);

            ILearningProcessor somLearningProcessor = null;
            double timeForAlgo = 0;

            somLearningProcessor = somProcessorsFactory.GetStandardLearningProcessor();
            timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            testResult.TimeStat.Add(new AlgorithmTime("Standard", timeForAlgo));
            Console.WriteLine("Standard:{0}", timeForAlgo);

            somLearningProcessor = somProcessorsFactory.GetParallelLearningProcessor(2);
            timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            testResult.TimeStat.Add(new AlgorithmTime("Paralleled, 2Thrs:", timeForAlgo));
            Console.WriteLine("Paralleled, 2Thrs: {0}", timeForAlgo);

            //somLearningProcessor = somProcessorsFactory.GetTrainsProcessor(2);
            //timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            //testResult.TimeStat.Add(new AlgorithmTime("Trains, 2Thrs:", timeForAlgo));
            //Console.WriteLine("Tains, 2Thrs: {0}", timeForAlgo);

            somLearningProcessor = somProcessorsFactory.GetParallelLearningProcessor(4);
            timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            testResult.TimeStat.Add(new AlgorithmTime("Paralleled, 4Thrs:", timeForAlgo));
            Console.WriteLine("Paralleled, 4Thrs: {0}", timeForAlgo);

            //somLearningProcessor = somProcessorsFactory.GetParallelLearningProcessor(8);
            //timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            //testResult.TimeStat.Add(new AlgorithmTime("Paralleled, 8Thrs:", timeForAlgo));
            //Console.WriteLine("Paralleled, 8Thrs: {0}", timeForAlgo);

            return testResult;
        }

        private double GetAverageTimeForAlgo(ILearningProcessor somLearningProcessor, TestResult testResult)
        {
            double mult = 1.0/Tests;
            double averageTime = 0.0;
            for (int i = 0; i < Tests; i++)
            {
                // collect garbage now to minimize interference
                GC.Collect();

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
        protected List<int> ThreadNumbers { get; set; }
        protected string FileName { get; set; }
        protected bool FromFile { get; set; }

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

            string threadsString = ConfigurationSettings.AppSettings["Threads"];
            string[] threads = threadsString.Split(',');
            ThreadNumbers = new List<int>();
            foreach (string threadNum in threads)
            {
                ThreadNumbers.Add(Int32.Parse(threadNum));
            }
            FromFile = Boolean.Parse(ConfigurationSettings.AppSettings["FromFile"]);
            FileName = ConfigurationSettings.AppSettings["FileName"];
        }

    }
}
