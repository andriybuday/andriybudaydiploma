using System;
using System.Collections.Generic;
using System.Text;

namespace Som.Metrics
{
    public interface IMetricFunction
    {
        double GetDistance(IList<Double> firstVector, IList<Double> secondVector);
    }
}
