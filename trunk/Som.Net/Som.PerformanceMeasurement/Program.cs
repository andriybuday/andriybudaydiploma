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

            var beforeTesting = DateTime.Now;
            program.Run();

            foreach (var s in args)
            {
                program.FromFile = true;
                program.FileName = s;
                program.Run();
            }
            var afterTesting = DateTime.Now;
            var testingTook = afterTesting.Subtract(beforeTesting);
            Console.WriteLine("Whole testing took {0}. Thank you for being patient.", testingTook);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public Program()
        {
            ReadConfiguration();
        }
        private void Run()
        {
            List<TestResult> testResults = new List<TestResult>();

            Console.WriteLine();
            Console.WriteLine("TEST SET");
            Console.WriteLine("Tries to know average: {0}", Tests);
            Console.WriteLine("Iterations: {0}", Iterations);
            if (FromFile) Console.WriteLine("FromFile: {0}", FileName);

            Console.WriteLine();
            Console.WriteLine("Start...");
            Console.WriteLine();

            foreach (int gridSideSize in Grids)
            {
                Console.WriteLine("Grid {0}X{0}...", gridSideSize);    

                if(FromFile)
                {
                    var testResult = GetOneTestResult(gridSideSize, 0);
                    testResults.Add(testResult);
                }
                else
                {
                    foreach (int dimention in Dimentions)
                    {
                        Console.WriteLine("Dimentin {0}...", dimention);

                        var testResult = GetOneTestResult(gridSideSize, dimention);
                        testResults.Add(testResult);
                    }    
                }
                
            }
            Console.WriteLine("Saving to file...");
            //SaveTestResults(testResults);
            //Console.WriteLine("TEST SET finished. Will be closed in 3 seconds...");
            //Thread.Sleep(3000);
            //Console.ReadKey();
        }

        private void SaveTestResults(List<TestResult> testResults)
        {
            string fileName = string.Format("ResultsFor_{0}_{1}_{2}_{3}_{4}_{5}.xml", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
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


            somLearningProcessor = somProcessorsFactory.GetParallelLearningProcessor(2);
            timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            testResult.TimeStat.Add(new AlgorithmTime(string.Format("Paralleled (v1), {0} Thrs", 2), timeForAlgo));
            Console.WriteLine("Paralleled (v1), {1} Thrs: {0}", timeForAlgo, 2);    

            /*
            somLearningProcessor = somProcessorsFactory.GetStandardLearningProcessor();
            timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            testResult.TimeStat.Add(new AlgorithmTime("On-line (v1)", timeForAlgo));
            Console.WriteLine("On-line (v1):{0}", timeForAlgo);

            for (int i = 0; i < ThreadNumbers.Count; i++)
            {
                somLearningProcessor = somProcessorsFactory.GetParallelLearningProcessor(ThreadNumbers[i]);
                timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
                testResult.TimeStat.Add(new AlgorithmTime(string.Format("Paralleled (v1), {0} Thrs", ThreadNumbers[i]), timeForAlgo));
                Console.WriteLine("Paralleled (v1), {1} Thrs: {0}", timeForAlgo, ThreadNumbers[i]);    
            }
            */


            /// Version 2
            /*
            somLearningProcessor = somProcessorsFactory.GetOnlineV2();
            timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            testResult.TimeStat.Add(new AlgorithmTime("On-line (v2)", timeForAlgo));
            Console.WriteLine("On-line (v2):{0}", timeForAlgo);

            for (int i = 0; i < ThreadNumbers.Count; i++)
            {
                somLearningProcessor = somProcessorsFactory.GetParalleledV2(ThreadNumbers[i]);
                timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
                testResult.TimeStat.Add(new AlgorithmTime(string.Format("Paralleled (v2), {0} Thrs", ThreadNumbers[i]), timeForAlgo));
                Console.WriteLine("Paralleled (v2), {1} Thrs: {0}", timeForAlgo, ThreadNumbers[i]);
            }
            */

            //somLearningProcessor = somProcessorsFactory.GetTrainsProcessor(2);
            //timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            //testResult.TimeStat.Add(new AlgorithmTime("Trains, 2Thrs:", timeForAlgo));
            //Console.WriteLine("Tains, 2Thrs: {0}", timeForAlgo);

            //somLearningProcessor = somProcessorsFactory.GetParallelLearningProcessor(4);
            //timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
            //testResult.TimeStat.Add(new AlgorithmTime("Paralleled, 4Thrs:", timeForAlgo));
            //Console.WriteLine("Paralleled, 4Thrs: {0}", timeForAlgo);

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
