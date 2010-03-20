using System;
using System.Collections.Generic;

namespace Som.Topology
{
    public class SimpleMatrixTopology : ITopology
    {
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public double WholeTopologyRadius { get; private set; }

        public SimpleMatrixTopology(int rowCount, int colCount)
        {
            ColCount = colCount;
            RowCount = rowCount;

            WholeTopologyRadius = Math.Max(ColCount, RowCount) / 2.0;
        }

        public int NeuronsCount { get { return RowCount * ColCount; } }

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
            var result = new List<int>();
            int upper = neuronNumber - ColCount;
            int down = neuronNumber + ColCount;
            int right = neuronNumber + 1;
            int left = neuronNumber - 1;

            if (upper >= 0) result.Add(upper);
            if (down < NeuronsCount) result.Add(down);
            if (right % ColCount != 0) result.Add(right);
            if ((left+1) % ColCount != 0) result.Add(left);

            return result;
        }

        public Dictionary<int, double> GetNeuronsInRadius(int neuronNumber, double radius)
        {
            return Slow_GetNeuronsInRadius(neuronNumber, radius);
        }

        public Dictionary<int, double> Slow_GetNeuronsInRadius(int neuronNumber, double radius)
        {
            var neuronsCount = RowCount * ColCount;

            var neuronColPos = neuronNumber % ColCount;
            var neuronRowPos = neuronNumber / ColCount;

            var result = new Dictionary<int, double>();

            for (int col = 0; col < ColCount; col++)
            {
                for (int row = 0; row < RowCount; row++)
                {
                    var currDist = GetDistance(col, row, neuronColPos, neuronRowPos);
                    if(currDist < radius)
                    {
                        result.Add(row*ColCount + col, currDist);
                    }
                }
            }

            return result;
        }

        private double GetDistance(int col, int row, int neuronColPos, int neuronRowPos)
        {
            var xD = (neuronColPos - col);
            var yD = (neuronRowPos - row);
            return Math.Sqrt(xD*xD + yD*yD);
        }
    }
}