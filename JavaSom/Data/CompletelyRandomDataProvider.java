package Data;

import java.util.Random;

public class CompletelyRandomDataProvider implements ILearningDataProvider
	{
		private int privateD;
		public final int getD()
		{
			return privateD;
		}
		private void setD(int value)
		{
			privateD = value;
		}
		private int _vectorsCountToReturn;

		private java.util.Random privateRandom;
		public final java.util.Random getRandom()
		{
			return privateRandom;
		}
		private void setRandom(java.util.Random value)
		{
			privateRandom = value;
		}
		public CompletelyRandomDataProvider(int dimentionSize, int vectorsCountToReturn)
		{
			setD(dimentionSize);
			_vectorsCountToReturn = vectorsCountToReturn;
			setRandom(new Random());
		}

		public final int getLearningVectorsCount()
		{
			return _vectorsCountToReturn;
		}

		public final int getDataVectorDimention()
		{
			return getD();
		}

		public final double[] GetLearingDataVector(int vectorIndex)
		{
			double[] result = new double[getD()];
			for (int i = 0; i < getD(); i++)
			{
				result[i] = getRandom().nextDouble();
			}
			return result;
		}
	}