package ActivationFunction;

	public interface IActivationFunction
	{
		double GetResult(double inputValue);

		double[] getParameters();
		void setParameters(double[] value);
	}