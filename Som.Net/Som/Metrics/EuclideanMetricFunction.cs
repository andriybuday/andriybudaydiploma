using System;
using System.Collections.Generic;
using System.Linq;

namespace Som.Metrics
{
    public class EuclideanMetricFunction : IMetricFunction
    {
        public double GetDistance(double[] firstVector, double[] secondVector)
        {
            if(firstVector.Length != secondVector.Length) throw new ArgumentException("For getting distance both input vectors should be of equal dimention.");

            double sum = 0;
            int vectorsCount = firstVector.Length;

            for (int i = 0; i < vectorsCount; i++)
            {
                var x = (firstVector[i] - secondVector[i]);
                sum += x * x;
            }

            return Math.Sqrt(sum);
        }
    }
}