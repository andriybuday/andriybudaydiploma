using System;

namespace Som.ActivationFunction
{
    public class HardLimitActivationFunction : ActivationFunctionBase
    {
        public HardLimitActivationFunction(double[] parameters)
            : base(parameters)
        {
            if (parameters.Length != 1) { throw new ArgumentException("For hard limit activation function count of input parameters should be equal to 1."); }
        }

        public override double GetResult(double inputValue)
        {
            // if x > p then 1 else 0
            return (inputValue > Parameters[0]) ? 1 : 0;
        }
    }
}