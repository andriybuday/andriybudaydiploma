using System.Collections.Generic;

namespace Som.Data
{
    public interface ILearningDataPersister
    {
        List<List<double>> GetData();

        void SaveData(List<List<double>> data);
    }
}