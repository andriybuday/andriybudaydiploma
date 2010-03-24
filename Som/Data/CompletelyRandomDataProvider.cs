using System;
using System.Collections.Generic;

namespace Som.Data
{
    public class CompletelyRandomDataProvider : ILearningDataProvider
    {
        public int D { get; private set; }
        private int _vectorsCountToReturn;

        public Random Random { get; private set; }
        public CompletelyRandomDataProvider(int dimentionSize, int vectorsCountToReturn)
        {
            D = dimentionSize;
            _vectorsCountToReturn = vectorsCountToReturn;
            Random = new Random();
        }

        public int LearningVectorsCount
        {
            get { return _vectorsCountToReturn; }
        }

        public int DataVectorDimention
        {
            get { return D; }
        }

        public double[] GetLearingDataVector(int vectorIndex)
        {
            var result = new double[D];
            for (int i = 0; i < D; i++)
            {
                result[i] = Random.NextDouble();
            }
            return result;
        }
    }
}