using System.Collections.Generic;
using SomParallelization.Kohonen;

namespace SomParallelization.LearningData
{
    public interface ILearningData
    {
        int LearningVectorsCount { get; }

        int DataVectorDimention { get; }

        IList<double> GetLearingDataVector(int vectorIndex); 
    }
}


