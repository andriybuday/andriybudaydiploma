using System;
using System.Collections.Generic;

namespace Som.Metrics
{
    public class CityBlockMetricFunction : IMetricFunction
    {
        public double GetDistance(IList<double> firstVector, IList<double> secondVector)
        {
            if (firstVector.Count != secondVector.Count) throw new ArgumentException("For getting distance both input vectors should be of equal dimention.");

            double sum = 0;

            for (int i = 0; i < firstVector.Count; i++)
            {
                sum += Math.Abs(firstVector[i] - secondVector[i]);
            }

            return Math.Sqrt(sum);
        }
    }
}