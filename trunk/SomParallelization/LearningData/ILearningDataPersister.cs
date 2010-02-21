using System.Collections.Generic;

namespace SomParallelization.LearningData
{
    public interface ILearningDataPersister
    {
        List<List<double>> GetData();

        void SaveData(List<List<double>> data);
    }
}