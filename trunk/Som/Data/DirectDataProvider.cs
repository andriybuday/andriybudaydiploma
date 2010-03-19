using System;
using System.Collections.Generic;

namespace Som.Data
{
    public class DirectDataProvider : ILearningDataPersister
    {
        public List<List<double>> Data { get; private set; }

        public DirectDataProvider(List<List<double>> data)
        {
            Data = data;
        }

        public List<List<double>> GetData()
        {
            return Data;
        }

        public void SaveData(List<List<double>> data)
        {
            throw new NotImplementedException();
        }
    }
}