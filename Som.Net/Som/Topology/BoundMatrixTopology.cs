using System;
using System.Collections.Generic;

namespace Som.Topology
{
    public class BoundMatrixTopology : ITopology
    {
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public double WholeTopologyRadius { get; set; }

        public double[] DistancesToWinner{ get; private set;}

        public BoundMatrixTopology(int rowCount, int colCount)
        {
            ColCount = colCount;
            RowCount = rowCount;
            WholeTopologyRadius = Math.Max(rowCount, colCount) / 2.0;

            DistancesToWinner = new double[NeuronsCount];
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

        public List<int> GetNeuronsInRadius(int neuronNumber, double radius)
        {
            //return Optimized_GetNeuronsInRadius(neuronNumber, Radius);

            return Standart_GetNeuronsInRadius(neuronNumber);
        }

        public bool Overlaps(int neuronA, int neuronB, double radius)
        {
            return true;
        }

        private List<int> Standart_GetNeuronsInRadius(int neuronNumber)
        {
            var result = new List<int>();
            result.Add(neuronNumber);
            DistancesToWinner[neuronNumber] = 0;

            var bfsQueue = new List<int>();
            bfsQueue.Add(neuronNumber);

            for (int i = 0; i < WholeTopologyRadius; i++)
            {
                IList<int> currentDirectlyConnected = null;
                var currentLevelConnected = new List<int>();

                foreach (var elem in bfsQueue)
                {
                    currentDirectlyConnected = GetDirectlyConnectedNeurons(elem);
                    currentLevelConnected.AddRange(currentDirectlyConnected);

                    foreach (var connectedNeuronInd in currentDirectlyConnected)
                    {
                        if(!result.Contains(connectedNeuronInd))
                            if (connectedNeuronInd < NeuronsCount)
                            {
                                result.Add(connectedNeuronInd);
                                DistancesToWinner[connectedNeuronInd] = i + 1;
                            }
                    }
                }
                bfsQueue.Clear();
                bfsQueue.AddRange(currentLevelConnected);    
            }

            return result;
        }

        public List<int> Optimized_GetNeuronsInRadius(int neuronNumber, double radius)
        {
            var neuronsCount = RowCount * ColCount;
            var neuronRowPos = neuronNumber % ColCount;

            var sR = radius*radius;
            var rowStart = ColCount * (neuronNumber/ColCount);
            var rowEnd = rowStart + ColCount - 1;
            var colStart = neuronNumber % ColCount;
            var colEnd = colStart + (RowCount-1)*ColCount;

            var result = new List<int>();
            result.Add(neuronNumber);
            DistancesToWinner[neuronNumber] = 0;

            if(sR > RowCount*RowCount + ColCount+ColCount)
            {
                for (int i = 0; i < neuronsCount; i++) result[i] = 0;

                return result;
            }
            
            for (int lev = 0; lev <= Math.Floor(radius); lev++)
            {
                var distFromCenter = Math.Sqrt(sR - lev*lev);
                int intDistFromCenter = (int) Math.Floor(distFromCenter);

                // move up by rows
                var currRowStart = rowStart - lev*ColCount;
                var currRowEnd = currRowStart + ColCount - 1;

                if(currRowStart < 0)
                    currRowStart = RowCount*ColCount + currRowStart;

                if(currRowEnd < 0)
                    currRowEnd = RowCount * ColCount + currRowEnd;

                GetNeuronsInCurrentRow(currRowStart, currRowEnd, neuronRowPos, intDistFromCenter, lev, result);

                // move down by rows
                var currRowStartDownGo = rowStart + lev * ColCount;
                var currRowEndDownGo = currRowStartDownGo + ColCount - 1;

                if (currRowStartDownGo >= neuronsCount)
                    currRowStartDownGo = currRowStartDownGo - neuronsCount;

                if (currRowEndDownGo >= neuronsCount)
                    currRowEndDownGo = currRowEndDownGo - neuronsCount;

                GetNeuronsInCurrentRow(currRowStartDownGo, currRowEndDownGo, neuronRowPos, intDistFromCenter, lev, result);
            }

            return result;
        }

        private void GetNeuronsInCurrentRow(int currRowStart, int currRowEnd, int neuronRowPos, int intDistFromCenter, int level, List<int> result)
        {
            var left = currRowStart + neuronRowPos - intDistFromCenter;
            var right = currRowStart + neuronRowPos + intDistFromCenter;

            double DIST_SHOULD_BE_CALCULATED = 0.0;

            if(2 * intDistFromCenter >= ColCount)
            {
                for (int i = currRowStart; i <= currRowEnd; i++)
                {
                    result.Add(i);
                    DistancesToWinner[i] = DIST_SHOULD_BE_CALCULATED;
                }
            }
            else if(left < currRowStart)
            {
                for (int i = ColCount + left; i <= currRowEnd; ++i)
                {
                    result.Add(i);
                    DistancesToWinner[i] = DIST_SHOULD_BE_CALCULATED;
                }
                for (int i = currRowStart; i <= right; ++i)
                {
                    result.Add(i);
                    DistancesToWinner[i] = DIST_SHOULD_BE_CALCULATED;
                }
            }
            else if (right > currRowEnd)
            {
                for (int i = currRowStart; i <= right - ColCount; ++i)
                {
                    result.Add(i);
                    DistancesToWinner[i] = DIST_SHOULD_BE_CALCULATED;
                }
                for (int i = left; i <= currRowEnd; ++i)
                {
                    result.Add(i);
                    DistancesToWinner[i] = DIST_SHOULD_BE_CALCULATED;
                }
            }
            else
            {
                for (int i = left; i <= right; i++)
                {
                    result.Add(i);
                    DistancesToWinner[i] = DIST_SHOULD_BE_CALCULATED;
                }
            }
        }
    }
}