using System;

namespace Som.Topology
{
    public class GaussNeighbourhoodFunction : NeighbourhoodFunctionBase
    {
        public double Radius { get; private set; }


        public GaussNeighbourhoodFunction(double radius)
            : base(new double[]{radius})
        {
            Radius = radius;
        }

        public override double GetResult(double distance)
        {
            double denominator = 2 * Parameters[0] * Parameters[0];
            return Math.Exp(-(distance * distance / denominator));
        }
    }
}