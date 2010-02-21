using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SomParallelization.ActivationFunction;

namespace SomParallelization.Network
{
    public class NeuronBase : INeuron
    {
        private static readonly Random Random = new Random();
        public NeuronBase(IList<double> weights, IActivationFunction activationFunction)
        {
            if (weights == null && weights.Count == 0) throw new ArgumentException("weights array could not be null or have 0 elements.");
            if (activationFunction == null) throw new ArgumentException("AtivationFunciton for neuron could not be null.");

            Weights = weights;
            ActivationFunction = activationFunction;
        }

        public NeuronBase(bool randomize, IList<double> maxWeights, IActivationFunction activationFunction)
        {
            if(maxWeights == null && maxWeights.Count == 0) throw new ArgumentException( "weights array could not be null or have 0 elements.");
            if (activationFunction == null) throw new ArgumentException("AtivationFunciton for neuron could not be null.");

            ActivationFunction = activationFunction;

            if(randomize)
            {
                
                Weights = new List<double>();

                foreach (var currentMaxWeight in maxWeights)
                {
                    Weights.Add(Random.NextDouble() * currentMaxWeight);
                }
            }
            else
            {
                Weights = maxWeights;
            }
        }

        public IList<double> Weights { get; set; }

        public double GetReaction(IList<double> inputVector)
        {
            if(Weights.Count != inputVector.Count) throw new ArgumentException("Input vector length should be equal to current neuron Weights vector length.");

            double sum = 0;

            for (int i = 0; i < Weights.Count; i++)
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