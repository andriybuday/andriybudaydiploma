package Metrics;

	public class EuclideanMetricFunction implements IMetricFunction
	{
		public final double GetDistance(double[] firstVector, double[] secondVector)
		{
			if(firstVector.length != secondVector.length)
			{
				throw new IllegalArgumentException("For getting distance both input vectors should be of equal dimention.");
			}

			double sum = 0;
			int vectorsCount = firstVector.length;

			for (int i = 0; i < vectorsCount; i++)
			{
				double x = (firstVector[i] - secondVector[i]);
				sum += x * x;
			}

			return Math.sqrt(sum);
		}
	}