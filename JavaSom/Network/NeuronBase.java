package Network;

import ActivationFunction.*;

import java.util.Random;

public class NeuronBase implements INeuron
	{
		private static java.util.Random random = new java.util.Random();
		public NeuronBase(double[] weights, IActivationFunction activationFunction)
		{
			if (weights == null)
			{
				throw new IllegalArgumentException("weights array could not be null");
			}
			if (weights.length == 0)
			{
				throw new IllegalArgumentException("weights array could not be null or have 0 elements.");
			}
			if (activationFunction == null)
			{
				throw new IllegalArgumentException("AtivationFunciton for neuron could not be null.");
			}

			setWeights(weights);
			setActivationFunction(activationFunction);
		}

		public NeuronBase(boolean randomize, java.util.List<Double> maxWeights, IActivationFunction activationFunction)
		{
			this(randomize, null, maxWeights, activationFunction);
		}

		public NeuronBase(boolean randomize, java.util.List<Double> minWeights, java.util.List<Double> maxWeights, IActivationFunction activationFunction)
		{
			if (maxWeights == null && maxWeights.isEmpty())
			{
				throw new IllegalArgumentException("weights array could not be null or have 0 elements.");
			}
			if (activationFunction == null)
			{
				throw new IllegalArgumentException("AtivationFunciton for neuron could not be null.");
			}
			if(minWeights != null)
			{
				if (minWeights.size() != maxWeights.size())
				{
					throw new IllegalArgumentException("weights constraints should be of the same dimentions");
				}
			}

			setActivationFunction(activationFunction);

			int d = maxWeights.size();
			if (randomize)
			{
				setWeights(new double[d]);

				if(minWeights != null)
				{
					for (int i = 0; i < d; i++)
					{
						double currentRandomized = minWeights.get(i) + random.nextDouble() * (maxWeights.get(i) - minWeights.get(i));

						getWeights()[i] = currentRandomized;
					}
				}
				else
				{
					for (int i = 0; i < d; i++)
					{
						getWeights()[i] = random.nextDouble() * maxWeights.get(i);
					}
				}
			}
			else
			{
				for (int i = 0; i < d; i++)
				{
					getWeights()[i] = maxWeights.get(i);
				}
			}
		}

		private double[] privateWeights;
		public final double[] getWeights()
		{
			return privateWeights;
		}
		public final void setWeights(double[] value)
		{
			privateWeights = value;
		}

		public final double GetReaction(java.util.List<Double> inputVector)
		{
			if(getWeights().length != inputVector.size())
			{
				throw new IllegalArgumentException("Input vector length should be equal to current neuron Weights vector length.");
			}

			double sum = 0;

			for (int i = 0; i < getWeights().length; i++)
			{
				sum += getWeights()[i]*inputVector.get(i);
			}

			return getActivationFunction().GetResult(sum);
		}

		private IActivationFunction privateActivationFunction;
		public final IActivationFunction getActivationFunction()
		{
			return privateActivationFunction;
		}
		public final void setActivationFunction(IActivationFunction value)
		{
			privateActivationFunction = value;
		}

		@Override
		public String toString()
		{
			StringBuilder sb = new StringBuilder();
			for (double r : getWeights())
			{
				sb.append(r);
				sb.append(" ");
			}
			return sb.toString();
		}
	}