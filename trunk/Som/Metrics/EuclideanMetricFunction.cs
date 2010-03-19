using System;
using System.Collections.Generic;
using System.Linq;

namespace Som.Metrics
{
    public class EuclideanMetricFunction : IMetricFunction
    {
        public double GetDistance(IList<double> firstVector, IList<double> secondVector)
        {
            if(firstVector.Count != secondVector.Count) throw new ArgumentException("For getting distance both input vectors should be of equal dimention.");

            double sum = 0;
            int vectorsCount = firstVector.Count;

            for (int i = 0; i < vectorsCount; i++)
            {
                var x = (firstVector[i] - secondVector[i]);
                sum += x * x;
            }

            return Math.Sqrt(sum);
        }
    }
}