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
            Neurons = new INeuron[topology.NeuronsCount];
            for (int i = 0; i < topology.NeuronsCount; i++)
            {
                Neurons[i] = new NeuronBase(randomize, maxWeights, activationFunction);
            }
        }

        public NetworkBase(bool randomize, List<double> minWeights, List<double> maxWeights, IActivationFunction activationFunction, ITopology topology)
        {
            Topology = topology;
            Neurons = new INeuron[topology.NeuronsCount];
            for (int i = 0; i < topology.NeuronsCount; i++)
            {
                Neurons[i] = new NeuronBase(randomize, minWeights, maxWeights, activationFunction);
            }
        }

        public INeuron[] Neurons { get; set; }

        public ITopology Topology { get; set; }
    }
}