﻿using System.Collections.Generic;
using Som.Network;

namespace Som.Topology
{
    public interface ITopology
    {
        int NeuronsCount { get; }

        int RowCount { get; }

        int ColCount { get; }
        
        double WholeTopologyRadius { get; }

        int GetNeuronNumber(Location location);
        
        Location GetNeuronLocation(int neuronNumber);

        IList<int> GetDirectlyConnectedNeurons(int neuronNumber);

        Dictionary<int, double> GetNeuronsInRadius(int neuronNumber, double radius);
    }
}