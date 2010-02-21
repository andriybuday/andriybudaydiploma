using SomParallelization.Learning;
using SomParallelization.LearningData;
using SomParallelization.Metrics;
using SomParallelization.Network;
using SomParallelization.Topology;

namespace SomParallelization.Kohonen
{
    public class WTALearningProcessor : LearningProcessorBase
    {
        public WTALearningProcessor(ILearningData learningData, INetwork network, ITopology topology, IMetricFunction metricFunction, ILearningFactorFunction learningFactorFunction, INeighbourhoodFunction neighbourhoodFunction, int maxIterationsCount)
            : base(learningData, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, maxIterationsCount)
        {
        }
    }
}
