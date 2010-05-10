package TESTER;

import sunw.io.Serializable;

public class TestResult implements Serializable
	{
		public TestResult()
		{
		}
		public TestResult(int iterations, String gridSize, int inputSpaceDimentions)
		{
			setIterations(iterations);
			setGridSize(gridSize);
			setInputSpaceDimentions(inputSpaceDimentions);
			setTimeStat(new java.util.ArrayList<AlgorithmTime>());
		}

		private int privateIterations;
		public final int getIterations()
		{
			return privateIterations;
		}
		public final void setIterations(int value)
		{
			privateIterations = value;
		}
		private String privateGridSize;
		public final String getGridSize()
		{
			return privateGridSize;
		}
		public final void setGridSize(String value)
		{
			privateGridSize = value;
		}
		private int privateInputSpaceDimentions;
		public final int getInputSpaceDimentions()
		{
			return privateInputSpaceDimentions;
		}
		public final void setInputSpaceDimentions(int value)
		{
			privateInputSpaceDimentions = value;
		}
		private java.util.ArrayList<AlgorithmTime> privateTimeStat;
		public final java.util.ArrayList<AlgorithmTime> getTimeStat()
		{
			return privateTimeStat;
		}
		public final void setTimeStat(java.util.ArrayList<AlgorithmTime> value)
		{
			privateTimeStat = value;
		}
	}