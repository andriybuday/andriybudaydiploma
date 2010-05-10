using System;
using System.Collections.Generic;

namespace Som.Data
{
    public class LearningDataProvider : ILearningDataProvider
    {
        private List<double[]> _learningVectors;

        private ILearningDataPersister LearningDataPersister { get; set; }

        public LearningDataProvider(ILearningDataPersister learningDataPersister)
        {
            LearningDataPersister = learningDataPersister;

            _learningVectors = new List<double[]>();
            var data = LearningDataPersister.GetData();
            int d = 0;
            if(data.Count > 0)
            {
                d = data[0].Count;
            }
            foreach (List<double> vector in data)
            {
                var dVector = new double[d];
                for (int i = 0; i < d; i++)
                {
                    dVector[i] = vector[i];
                }
                _learningVectors.Add(dVector);
            }
        }

        public int LearningVectorsCount
        {
            get { return _learningVectors.Count; }
        }

        public int DataVectorDimention
        {
            get { return (_learningVectors.Count > 0) ? _learningVectors[0].Length : -1; }
        }

        public double[] GetLearingDataVector(int vectorIndex)
        {
            if (_learningVectors.Count <= vectorIndex) throw new ArgumentException("VectorIndex is out of range");

            return _learningVectors[vectorIndex];
        }
    }
}