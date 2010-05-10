package Learning;

	public interface ILearningFactorFunction
	{
		double GetLearningRate(double inputValue);

		double[] getParameters();
		void setParameters(double[] value);
	}