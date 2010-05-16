using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using Som.Topology;

namespace Som.Concurrency
{
    public class ConcurrentSimpleMatrixTopology : ITopology
    {
        private int _n;
        private int nColPos;
        private int nRowPos;
        private int higherLevel;
        private int left;
        private int right;
        private double Radius;
        private List<int> CurrentResult;
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public double WholeTopologyRadius { get; private set; }

        public double[] DistancesToWinner { get; private set; }

        public ConcurrentSimpleMatrixTopology(int rowCount, int colCount)
        {
            ColCount = colCount;
            RowCount = rowCount;
            _n = RowCount*ColCount;
            //var bitArray = new BitArray(NeuronsCount, false);
            //bitArray.
            DistancesToWinner = new double[NeuronsCount];

            WholeTopologyRadius = Math.Max(ColCount, RowCount) / 2.0;
        }

        public int NeuronsCount { get { return _n; } }

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
            int directlyRight = neuronNumber + 1;
            int directlyLeft = neuronNumber - 1;

            if (upper >= 0) result.Add(upper);
            if (down < NeuronsCount) result.Add(down);
            if (directlyRight % ColCount != 0) result.Add(directlyRight);
            if ((directlyLeft + 1) % ColCount != 0) result.Add(directlyLeft);

            return result;
        }

        public List<int> GetNeuronsInRadius(int neuronNumber, double radius)
        {
            //return Slow_GetNeuronsInRadius(neuronNumber, radius);
            return FindNeiboInCycleFastSearch(neuronNumber, radius);

        }

        public bool Overlaps(int neuronA, int neuronB, double radius)
        {
            var nColPosA = neuronA % ColCount;
            var nRowPosA = neuronA / ColCount;
            var nColPosB = neuronB % ColCount;
            var nRowPosB = neuronB / ColCount;
            var colDiff = Math.Abs(nColPosA - nColPosB);
            var rowDiff = Math.Abs(nRowPosA - nRowPosB);
            return (colDiff*colDiff + rowDiff*rowDiff) < (radius*radius);
        }
        private object locker = new object();
        private AutoResetEvent done = new AutoResetEvent(false);
        public List<int> FindNeiboInCycleFastSearch(int neuronNumber, double radius)
        {
            Radius = radius;
            CurrentResult = new List<int>();

            nColPos = neuronNumber % ColCount;
            nRowPos = neuronNumber / ColCount;

            var r = (int)Math.Floor(radius + 1);
            
            //runs through rows
            higherLevel = nColPos - r;
            if(higherLevel < 0)higherLevel = 0;
            var lowerLevel = higherLevel + 2*r;
            if (lowerLevel > RowCount) lowerLevel = RowCount;

            //runs through columns
            left = nRowPos - r;
            if (left < 0) left = 0;
            right = left + 2*r;
            if (right > ColCount) right = ColCount;

            var mid = (lowerLevel - higherLevel)/2;

            ThreadPool.UnsafeQueueUserWorkItem(AsyncFindNeibo, higherLevel + mid);

            for (int col = higherLevel + mid; col < lowerLevel; col++)
            {
                for (int row = left; row < right; row++)
                {
                    var currDist = GetDistance(col, row, nColPos, nRowPos);
                    if (currDist < radius)
                    {
                        lock (locker)
                        {
                            CurrentResult.Add(row * ColCount + col);    
                        }
                        DistancesToWinner[row * ColCount + col] = currDist;
                    }
                }
            }

            done.WaitOne();

            return CurrentResult;
        }

        private void AsyncFindNeibo(object theLowerLevel)
        {
            var intTheLowerLevel = (int)theLowerLevel;
            for (int col = higherLevel; col < intTheLowerLevel; col++)
            {
                for (int row = left; row < right; row++)
                {
                    var currDist = GetDistance(col, row, nColPos, nRowPos);
                    if (currDist < Radius)
                    {
                        lock (locker)
                        {
                            CurrentResult.Add(row * ColCount + col);
                        }
                        DistancesToWinner[row*ColCount + col] = currDist;
                    }
                }
            }
            done.Set();
        }

        public List<int> Slow_GetNeuronsInRadius(int neuronNumber, double radius)
        {
            var neuronsCount = RowCount * ColCount;

            var neuronColPos = neuronNumber % ColCount;
            var neuronRowPos = neuronNumber / ColCount;

            var result = new List<int>();

            for (int col = 0; col < ColCount; col++)
            {
                for (int row = 0; row < RowCount; row++)
                {
                    var currDist = GetDistance(col, row, neuronColPos, neuronRowPos);
                    if (currDist < radius)
                    {
                        result.Add(row * ColCount + col);
                        DistancesToWinner[row * ColCount + col] = currDist;
                    }
                }
            }

            return result;
        }


        private double GetDistance(int col, int row, int neuronColPos, int neuronRowPos)
        {
            var xD = (neuronColPos - col);
            var yD = (neuronRowPos - row);
            return Math.Sqrt(xD * xD + yD * yD);
        }
    }
}