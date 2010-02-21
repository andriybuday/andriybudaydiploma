using System.Collections.Generic;
using SomParallelization.ActivationFunction;
using SomParallelization.Topology;

namespace SomParallelization.Network
{
    public class NetworkBase : INetwork
    {
        public NetworkBase(bool randomize, IList<double> maxWeights, IActivationFunction activationFunction, ITopology topology)
        {
            Topology = topology;
            Neurons = new List<INeuron>();
            for (int i = 0; i < topology.NeuronsCount; i++)
            {
                INeuron neuron = new NeuronBase(randomize, maxWeights, activationFunction);
                Neurons.Add(neuron);
            }
        }

        public IList<INeuron> Neurons { get; set; }

        public ITopology Topology { get; set; }
    }
}