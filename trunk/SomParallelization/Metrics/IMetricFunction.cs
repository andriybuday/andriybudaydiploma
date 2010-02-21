using System;
using System.Collections.Generic;
using System.Text;

namespace SomParallelization.Metrics
{
    public interface IMetricFunction
    {
        double GetDistance(IList<Double> firstVector, IList<Double> secondVector);
    }
}
