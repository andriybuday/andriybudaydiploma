using System;

namespace Som.Topology
{
    public class GaussNeighbourhoodFunction : INeighbourhoodFunction
    {
        public double GetDistanceFalloff(double distance, double radius)
        {
            double denominator = 2 * radius * radius;
            return Math.Exp(-(distance * distance / denominator));
        }
    }
}