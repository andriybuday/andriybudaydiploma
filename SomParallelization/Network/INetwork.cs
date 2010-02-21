using System;
using System.Collections.Generic;
using SomParallelization.Topology;

namespace SomParallelization.Network
{
    public interface INetwork
    {
        IList<INeuron> Neurons { get; set; }

        ITopology Topology { get; set; }
    }
}
