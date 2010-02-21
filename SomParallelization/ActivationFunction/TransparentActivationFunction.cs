using System;

namespace SomParallelization.ActivationFunction
{
    public class TransparentActivationFunction : ActivationFunctionBase
    {
        public TransparentActivationFunction(double[] parameters)
            : base(parameters)
        {
            if (parameters.Length != 0) { throw new ArgumentException("For transparent activation function count of input parameters should be equal to 0."); }
        }

        public override double GetResult(double inputValue)
        {
            // x
            return inputValue;
        }
    }
}