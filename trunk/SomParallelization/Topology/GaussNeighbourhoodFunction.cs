using System;

namespace SomParallelization.Topology
{
    public class GaussNeighbourhoodFunction : NeighbourhoodFunctionBase
    {
        public GaussNeighbourhoodFunction(double[] parameters)
            : base(parameters)
        {
            if (parameters.Length != 1) { throw new ArgumentException("For GaussNeighbourhoodFunction count of input parameters should be equal to 1."); }
        }

        public override double GetResult(double distance)
        {
            double denominator = 2 * Parameters[0] * Parameters[0];
            return Math.Exp(-distance * distance / denominator);
        }
    }
}