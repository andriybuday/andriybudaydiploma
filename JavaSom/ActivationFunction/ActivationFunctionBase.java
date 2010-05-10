package ActivationFunction;

	public abstract class ActivationFunctionBase implements IActivationFunction
	{
		protected ActivationFunctionBase(double[] parameters)
		{
			if (parameters == null)
			{
				throw new IllegalArgumentException("Paramenters should be not null value for any activation function.");
			}
			setParameters(parameters);
		}

		public abstract double GetResult(double inputValue);

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