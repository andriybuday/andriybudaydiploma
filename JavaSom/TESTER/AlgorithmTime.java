package TESTER;

import sunw.io.Serializable;

public class AlgorithmTime implements Serializable
	{
		public AlgorithmTime()
		{
		}
		public AlgorithmTime(String algoName, double aveTime)
		{
			setAlgoName(algoName);
			setAverageTime(aveTime);
		}

		private String privateAlgoName;
		public final String getAlgoName()
		{
			return privateAlgoName;
		}
		public final void setAlgoName(String value)
		{
			privateAlgoName = value;
		}
		private double privateAverageTime;
		public final double getAverageTime()
		{
			return privateAverageTime;
		}
		public final void setAverageTime(double value)
		{
			privateAverageTime = value;
		}
	}