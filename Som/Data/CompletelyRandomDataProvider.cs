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

    public class FourDimentionalConstProvider : ILearningDataProvider
    {
        public int D { get; private set; }
        private int _vectorsCountToReturn;

        public Random Random { get; private set; }
        public FourDimentionalConstProvider(int dimentionSize, int vectorsCountToReturn)
        {
            D = dimentionSize;
            _vectorsCountToReturn = vectorsCountToReturn;
            if(D != 4)throw new ArgumentException("D should be equal to 4");
            if (vectorsCountToReturn != 1) throw new ArgumentException("Vectorst to return should be 1");
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

        private double[] vector = new double[] {0.2, 0.4, 0.7, 0.9};
        public double[] GetLearingDataVector(int vectorIndex)
        {
            return vector;
        }
    }
}