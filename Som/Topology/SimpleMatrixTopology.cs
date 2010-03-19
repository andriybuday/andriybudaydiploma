using System;
using System.Collections.Generic;

namespace Som.Topology
{
    public class SimpleMatrixTopology : ITopology
    {
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        public SimpleMatrixTopology(int rowCount, int colCount, double radius)
        {
            ColCount = colCount;
            RowCount = rowCount;
            Radius = radius;
        }

        public int NeuronsCount { get { return RowCount * ColCount; } }

        public double Radius { get; set; }

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

        public Dictionary<int, double> GetNeuronsInRadius(int neuronNumber)
        {
            return Slow_GetNeuronsInRadius(neuronNumber, Radius);
        }

        public Dictionary<int, double> Slow_GetNeuronsInRadius(int neuronNumber, double radius)
        {
            var neuronsCount = RowCount * ColCount;

            var neuronRowPos = neuronNumber % ColCount;
            var neuronColPos = neuronNumber / ColCount;

            var sR = radius * radius;
            var rowStart = ColCount * (neuronNumber / ColCount);
            var rowEnd = rowStart + ColCount - 1;
            var colStart = neuronNumber % ColCount;
            var colEnd = colStart + (RowCount - 1) * ColCount;

            var result = new Dictionary<int, double>();
            //result.Add(neuronNumber, 0);

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