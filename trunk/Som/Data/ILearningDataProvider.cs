using System.Collections.Generic;
using Som.Kohonen;

namespace Som.Data
{
    public interface ILearningDataProvider
    {
        int LearningVectorsCount { get; }

        int DataVectorDimention { get; }

        IList<double> GetLearingDataVector(int vectorIndex); 
    }
}


