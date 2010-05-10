package TESTER;

import Som.ActivationFunction.*;
import Som.Data.*;
import Som.Learning.*;
import Som.LearningProcessor.*;
import Som.Metrics.*;
import Som.Network.*;
import Som.Topology.*;

public class Program
	{
		public static void main(String[] args)
		{
			Program program = new Program();
			program.Run();

		}

		private void Run()
		{
			ReadConfiguration();

			java.util.ArrayList<TestResult> testResults = new java.util.ArrayList<TestResult>();

			System.out.println("Start...");
			for (int gridSideSize : getGrids())
			{
				//System.out.println("Grid {0}X{0}...", gridSideSize);
				for (int dimention : getDimentions())
				{
					//System.out.println("Dimentin {0}...", dimention);
					TestResult testResult = GetOneTestResult(gridSideSize, dimention);
					testResults.add(testResult);
				}
			}
			SaveTestResults(testResults);
			System.out.println("End.");
			System.out.println("Press any key to exit...");
			//Console.ReadKey();
		}

		private void SaveTestResults(java.util.ArrayList<TestResult> testResults)
		{
			//String fileName = String.format("ResultsFor_%1$s_%2$s_%3$s_%4$s_%5$s.xml", new java.util.Date().Year, new java.util.Date().Month, new java.util.Date().Day, new java.util.Date().Hour, new java.util.Date().Minute);
			//StreamWriter streamWriter = File.CreateText(fileName);
			//XmlSerializer xmlSerializer = new XmlSerializer(java.util.ArrayList<TESTER.TestResult>.class);
			//xmlSerializer.Serialize(streamWriter,testResults);
			//streamWriter.Close();
		}

		private TestResult GetOneTestResult(int gridSideSize, int dimention)
		{
			TestResult testResult = new TestResult(getIterations(), String.format("%1$sX%2$s", gridSideSize, gridSideSize), dimention);

			SomProcessorsFactory somProcessorsFactory = new SomProcessorsFactory(getIterations(), dimention, gridSideSize, 0.07);

			ILearningProcessor somLearningProcessor = somProcessorsFactory.GetStandardLearningProcessor(getIterations(), 0.07, gridSideSize, dimention);
			double timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
			testResult.getTimeStat().add(new AlgorithmTime("Standard", timeForAlgo));
			System.out.println("Standard:{0}", timeForAlgo);

			somLearningProcessor = somProcessorsFactory.GetParallelLearningProcessor(getIterations(), 0.07, gridSideSize, dimention);
			timeForAlgo = GetAverageTimeForAlgo(somLearningProcessor, testResult);
			testResult.getTimeStat().add(new AlgorithmTime("Paralleled", timeForAlgo));
			System.out.println("Paralleled:{0}", timeForAlgo);

			return testResult;
		}

		private double GetAverageTimeForAlgo(ILearningProcessor somLearningProcessor, TestResult testResult)
		{
			double mult = 1.0/getTests();
			double averageTime = 0.0;
			for (int i = 0; i < getTests(); i++)
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

		private int privateTests;
		public final int getTests()
		{
			return privateTests;
		}
		private void setTests(int value)
		{
			privateTests = value;
		}
		private int privateIterations;
		public final int getIterations()
		{
			return privateIterations;
		}
		private void setIterations(int value)
		{
			privateIterations = value;
		}
		private java.util.ArrayList<Integer> privateDimentions;
		public final java.util.ArrayList<Integer> getDimentions()
		{
			return privateDimentions;
		}
		private void setDimentions(java.util.ArrayList<Integer> value)
		{
			privateDimentions = value;
		}
		private java.util.ArrayList<Integer> privateGrids;
		public final java.util.ArrayList<Integer> getGrids()
		{
			return privateGrids;
		}
		private void setGrids(java.util.ArrayList<Integer> value)
		{
			privateGrids = value;
		}

		private void ReadConfiguration()
		{
			setTests(Integer.parseInt(ConfigurationSettings.AppSettings["Tests"]));
			setIterations(Integer.parseInt(ConfigurationSettings.AppSettings["Iterations"]));
			String dimentionsString = ConfigurationSettings.AppSettings["Dimentions"];
			String[] dimentions = dimentionsString.split("[,]", -1);
			setDimentions(new java.util.ArrayList<Integer>());
			for (String dimention : dimentions)
			{
				getDimentions().add(Integer.parseInt(dimention));
			}
			String gridsString = ConfigurationSettings.AppSettings["Grids"];
			String[] grids = gridsString.split("[,]", -1);
			setGrids(new java.util.ArrayList<Integer>());
			for (String grid : grids)
			{
				getGrids().add(Integer.parseInt(grid));
			}
		}


	}