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

        public IList<double> GetLearingDataVector(int vectorIndex)
        {
            var result = new List<double>();
            for (int i = 0; i < D; i++)
            {
                result.Add(Random.NextDouble());
            }
            return result;
        }
    }

    public class TwoDimentionsDataProvider : ILearningDataProvider
    {
        public int D { get; private set; }
        private int _vectorsCountToReturn;

        public Random Random { get; private set; }
        public TwoDimentionsDataProvider(int dimentionSize, int vectorsCountToReturn)
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

        public IList<double> GetLearingDataVector(int vectorIndex)
        {
            var result = new List<double>();
            for (int i = 0; i < D; i++)
            {
                result.Add(Random.Next(10)*0.1);
            }
            return result;
        }
    }

}