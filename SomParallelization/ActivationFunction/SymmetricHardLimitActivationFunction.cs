using System;

namespace SomParallelization.ActivationFunction
{
    public class SymmetricHardLimitActivationFunction : ActivationFunctionBase
    {
        public SymmetricHardLimitActivationFunction(double[] parameters)
            : base(parameters)
        {
            if (parameters.Length != 1) { throw new ArgumentException("For symmetric hard limit activation function count of input parameters should be equal to 1."); }
        }

        public override double GetResult(double inputValue)
        {
            // if x > p then 1 else -1
            return (inputValue > Parameters[0]) ? 1 : -1;
        }
    }
}