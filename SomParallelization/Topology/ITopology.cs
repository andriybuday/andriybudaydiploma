using System.Collections.Generic;
using SomParallelization.Network;

namespace SomParallelization.Topology
{
    public interface ITopology
    {
        int NeuronsCount { get; }

        int RowCount { get; }

        int ColCount { get; }
        
        int Radius { get; set; }

        int GetNeuronNumber(Location location);
        
        Location GetNeuronLocation(int neuronNumber);

        IList<int> GetDirectlyConnectedNeurons(int neuronNumber);

        IList<int> GetNeuronsInRadius(int neuronNumber);
    }
}
