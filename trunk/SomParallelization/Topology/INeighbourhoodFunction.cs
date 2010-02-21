using System;

namespace SomParallelization.Topology
{
    public interface INeighbourhoodFunction
    {
        double GetResult(double inputValue);

        double[] Parameters { get; set; }
    }

    public abstract class NeighbourhoodFunctionBase : INeighbourhoodFunction
    {
        protected NeighbourhoodFunctionBase(double[] parameters)
        {
            if (parameters == null) { throw new ArgumentException("Paramenters should be not null value for any neighbourhood function."); }
            Parameters = parameters;
        }

        public abstract double GetResult(double inputValue);

        public double[] Parameters { get; set; }
    }
}
