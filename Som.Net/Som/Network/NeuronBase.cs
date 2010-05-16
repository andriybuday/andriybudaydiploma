using System;
using System.Collections.Generic;
using System.Text;
using Som.ActivationFunction;

namespace Som.Network
{
    public class NeuronBase : INeuron
    {
        private static readonly Random Random = new Random();
        public NeuronBase(double[] weights, IActivationFunction activationFunction)
        {
            if (weights == null) throw new ArgumentException("weights array could not be null");
            if (weights.Length == 0) throw new ArgumentException("weights array could not be null or have 0 elements.");
            if (activationFunction == null) throw new ArgumentException("AtivationFunciton for neuron could not be null.");

            Weights = weights;
            ActivationFunction = activationFunction;
        }

        public NeuronBase(bool randomize, IList<double> maxWeights, IActivationFunction activationFunction)
            :this(randomize, null, maxWeights, activationFunction) {}

        public NeuronBase(bool randomize, IList<double> minWeights, IList<double> maxWeights, IActivationFunction activationFunction)
        {
            if (maxWeights == null && maxWeights.Count == 0) throw new ArgumentException("weights array could not be null or have 0 elements.");
            if (activationFunction == null) throw new ArgumentException("AtivationFunciton for neuron could not be null.");
            if(minWeights != null)
            {
                if (minWeights.Count != maxWeights.Count)
                    throw new ArgumentException("weights constraints should be of the same dimentions");
            }

            ActivationFunction = activationFunction;

            var d = maxWeights.Count;
            if (randomize)
            {    
                Weights = new double[d];

                if(minWeights != null)
                {
                    for (int i = 0; i < d; i++)
                    {
                        double currentRandomized = minWeights[i] + Random.NextDouble() * (maxWeights[i] - minWeights[i]);

                        Weights[i] = currentRandomized;
                    }    
                }
                else
                {
                    for (int i = 0; i < d; i++) Weights[i] = Random.NextDouble() * maxWeights[i];
                }
            }
            else
            {
                for (int i = 0; i < d; i++) Weights[i] = maxWeights[i];
            }
        }

        public double[] Weights { get; set; }

        public double GetReaction(double[] inputVector)
        {
            if(Weights.Length != inputVector.Length) throw new ArgumentException("Input vector length should be equal to current neuron Weights vector length.");

            double sum = 0;

            for (int i = 0; i < Weights.Length; i++)
            {
                sum += Weights[i]*inputVector[i];
            }

            return ActivationFunction.GetResult(sum);
        }

        public IActivationFunction ActivationFunction { get; set;}

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var r in Weights)
            {
                sb.Append(Math.Round(r, 3));
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}