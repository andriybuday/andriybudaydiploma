package Network;

import ActivationFunction.*;

	public interface INeuron
	{
		double[] getWeights();
		void setWeights(double[] value);

		double GetReaction(java.util.List<Double> inputVector);

		IActivationFunction getActivationFunction();
		void setActivationFunction(IActivationFunction value);
	}