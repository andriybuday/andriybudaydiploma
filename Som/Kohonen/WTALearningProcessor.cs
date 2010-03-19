using Som.Data.Suffle;
using Som.Learning;
using Som.Data;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.Kohonen
{
    public class WTALearningProcessor : LearningProcessorBase
    {
        public WTALearningProcessor(ILearningDataProvider learningDataProvider, INetwork network, ITopology topology, IMetricFunction metricFunction, ILearningFactorFunction learningFactorFunction, INeighbourhoodFunction neighbourhoodFunction, int maxIterationsCount, ISuffleProvider suffleProvider)
            : base(learningDataProvider, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, maxIterationsCount, suffleProvider)
        {
        }
    }
}
