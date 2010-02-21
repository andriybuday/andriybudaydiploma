using System;
using System.Linq;
using System.Collections.Generic;

namespace SomParallelization.Topology
{
    public class MatrixTopology : ITopology
    {
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        public MatrixTopology(int rowCount, int colCount, int radius)
        {
            ColCount = colCount;
            RowCount = rowCount;
            Radius = radius;
        }

        public int NeuronsCount { get { return RowCount * ColCount; } }

        public int Radius { get; set; }

        public int GetNeuronNumber(Location location)
        {
            throw new NotImplementedException();
        }

        public Location GetNeuronLocation(int neuronNumber)
        {
            throw new NotImplementedException();
        }

        public IList<int> GetDirectlyConnectedNeurons(int neuronNumber)
        {
            int upper = neuronNumber - ColCount;
            int down = neuronNumber + ColCount;
            int right;
            int left;

            if (upper < 0) { upper += NeuronsCount; }
            if (down > NeuronsCount) { down -= NeuronsCount; }
            right = ((neuronNumber + 1) % ColCount == 0) ? (neuronNumber - ColCount + 1) : (neuronNumber + 1);
            left = (neuronNumber % ColCount == 0) ? (neuronNumber + ColCount - 1) : (neuronNumber - 1);

            return new List<int>() { upper, down, right, left };
        }

        public IList<int> GetNeuronsInRadius(int neuronNumber)
        {
            var result = new List<int>() { neuronNumber };

            var bfsQueue = new List<int>();
            bfsQueue.Add(neuronNumber);

            for (int i = 0; i < Radius; i++)
            {
                IList<int> currentDirectlyConnected = null;
                foreach (var elem in bfsQueue)
                {
                    currentDirectlyConnected = GetDirectlyConnectedNeurons(elem);

                    result.AddRange(currentDirectlyConnected);
                }
                bfsQueue.Clear();
                bfsQueue.AddRange(currentDirectlyConnected);    
            }

            return result.Select(x => (x < NeuronsCount) ? x : 0).Distinct().ToList();
        }
    }
}