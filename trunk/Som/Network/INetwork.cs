using System;
using System.Collections.Generic;
using Som.Topology;

namespace Som.Network
{
    public interface INetwork
    {
        INeuron[] Neurons { get; set; }

        ITopology Topology { get; set; }
    }
}
