
namespace Som.Data
{
    public interface ILearningDataProvider
    {
        int LearningVectorsCount { get; }

        int DataVectorDimention { get; }

        double[] GetLearingDataVector(int vectorIndex); 
    }
}


