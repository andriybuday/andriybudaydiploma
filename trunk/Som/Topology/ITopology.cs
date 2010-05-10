using System.Collections.Generic;
using Som.Network;

namespace Som.Topology
{
    public interface ITopology
    {
        int NeuronsCount { get; }

        int RowCount { get; }

        int ColCount { get; }
        
        double WholeTopologyRadius { get; }
        double[] DistancesToWinner { get; }

        int GetNeuronNumber(Location location);
        
        Location GetNeuronLocation(int neuronNumber);

        IList<int> GetDirectlyConnectedNeurons(int neuronNumber);

        List<int> GetNeuronsInRadius(int neuronNumber, double radius);

        bool Overlaps(int neuronA, int neuronB, double radius);
    }
}
