using System;
using System.Collections.Generic;
using Som.ActivationFunction;
using Som.Topology;

namespace Som.Network
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

        public NetworkBase(bool randomize, List<double> minWeights, List<double> maxWeights, IActivationFunction activationFunction, ITopology topology)
        {
            Topology = topology;
            Neurons = new List<INeuron>();
            for (int i = 0; i < topology.NeuronsCount; i++)
            {
                INeuron neuron = new NeuronBase(randomize, minWeights, maxWeights, activationFunction);
                Neurons.Add(neuron);
            }
        }

        public IList<INeuron> Neurons { get; set; }

        public ITopology Topology { get; set; }
    }
}