using System;
using System.Collections.Generic;

namespace Som.Data
{
    public class LearningDataProvider : ILearningDataProvider
    {
        private List<List<double>> _learningVectors;

        private ILearningDataPersister LearningDataPersister { get; set; }

        public LearningDataProvider(ILearningDataPersister learningDataPersister)
        {
            LearningDataPersister = learningDataPersister;

            _learningVectors = LearningDataPersister.GetData();
        }

        public int LearningVectorsCount
        {
            get { return _learningVectors.Count; }
        }

        public int DataVectorDimention
        {
            get { return (_learningVectors.Count > 0) ? _learningVectors[0].Count : -1; }
        }

        public IList<double> GetLearingDataVector(int vectorIndex)
        {
            if (_learningVectors.Count <= vectorIndex) throw new ArgumentException("VectorIndex is out of range");

            return _learningVectors[vectorIndex];
        }
    }
}