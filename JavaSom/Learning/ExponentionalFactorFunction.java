package Learning;

	public class ExponentionalFactorFunction extends LearningFactorFunctionBase
	{
		private double privateStartLearningRate;
		public final double getStartLearningRate()
		{
			return privateStartLearningRate;
		}
		private void setStartLearningRate(double value)
		{
			privateStartLearningRate = value;
		}
		private double privateMaxIterationsCount;
		public final double getMaxIterationsCount()
		{
			return privateMaxIterationsCount;
		}
		private void setMaxIterationsCount(double value)
		{
			privateMaxIterationsCount = value;
		}

		public ExponentionalFactorFunction(double startRate, int maxIterationsCount)
		{
			super(new double[]{startRate, maxIterationsCount});
			setStartLearningRate(startRate);
			setMaxIterationsCount(maxIterationsCount);
		}

		@Override
		public double GetLearningRate(double iteration)
		{
			return getStartLearningRate() * Math.exp(- iteration / getMaxIterationsCount());
		}
	}