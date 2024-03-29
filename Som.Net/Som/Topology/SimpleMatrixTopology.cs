﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Som.Topology
{
    public class SimpleMatrixTopology : ITopology
    {
        private int _n;
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public double WholeTopologyRadius { get; private set; }

        public double[] DistancesToWinner { get; private set; }

        public SimpleMatrixTopology(int rowCount, int colCount)
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
            int right = neuronNumber + 1;
            int left = neuronNumber - 1;

            if (upper >= 0) result.Add(upper);
            if (down < NeuronsCount) result.Add(down);
            if (right % ColCount != 0) result.Add(right);
            if ((left + 1) % ColCount != 0) result.Add(left);

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

        public List<int> FindNeiboInCycleFastSearch(int neuronNumber, double radius)
        {
            var result = new List<int>();

            var nColPos = neuronNumber % ColCount;
            var nRowPos = neuronNumber / ColCount;

            var r = (int)Math.Floor(radius + 1);
            
            //runs through rows
            var higherLevel = nColPos - r;
            if(higherLevel < 0)higherLevel = 0;
            var lowerLevel = higherLevel + 2*r;
            if (lowerLevel > RowCount) lowerLevel = RowCount;

            //runs through columns
            var left = nRowPos - r;
            if (left < 0) left = 0;
            var right = left + 2*r;
            if (right > ColCount) right = ColCount;


            for (int col = higherLevel; col < lowerLevel; col++)
            {
                for (int row = left; row < right; row++)
                {
                    var currDist = GetDistance(col, row, nColPos, nRowPos);
                    if (currDist < radius)
                    {
                        result.Add(row * ColCount + col);
                        DistancesToWinner[row*ColCount + col] = currDist;
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