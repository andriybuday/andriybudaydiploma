package Learning;

	public abstract class LearningFactorFunctionBase implements ILearningFactorFunction
	{
		protected LearningFactorFunctionBase(double[] parameters)
		{
			if (parameters == null)
			{
				throw new IllegalArgumentException("Paramenters should be not null value for any learning function.");
			}
			setParameters(parameters);
		}

		public abstract double GetLearningRate(double inputValue);

		private double[] privateParameters;
		public final double[] getParameters()
		{
			return privateParameters;
		}
		public final void setParameters(double[] value)
		{
			privateParameters = value;
		}
	}