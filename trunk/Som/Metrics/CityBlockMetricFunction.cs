using System;
using System.Collections.Generic;

namespace Som.Metrics
{
    public class CityBlockMetricFunction : IMetricFunction
    {
        public double GetDistance(double[] firstVector, double[] secondVector)
        {
            if (firstVector.Length != secondVector.Length) throw new ArgumentException("For getting distance both input vectors should be of equal dimention.");

            double sum = 0;

            for (int i = 0; i < firstVector.Length; i++)
            {
                sum += Math.Abs(firstVector[i] - secondVector[i]);
            }

            return Math.Sqrt(sum);
        }
    }
}