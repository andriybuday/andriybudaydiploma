package ActivationFunction;

	public class TransparentActivationFunction extends ActivationFunctionBase
	{
		public TransparentActivationFunction(double[] parameters)
		{
			super(parameters);
			if (parameters.length != 0)
			{
				throw new IllegalArgumentException("For transparent activation function count of input parameters should be equal to 0.");
			}
		}

		@Override
		public double GetResult(double inputValue)
		{
			// x
			return inputValue;
		}
	}