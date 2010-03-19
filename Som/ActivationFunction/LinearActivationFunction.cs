using System;

namespace Som.ActivationFunction
{
    public class LinearActivationFunction : ActivationFunctionBase
    {
        public LinearActivationFunction(double[] parameters) : base(parameters)
        {
            if(parameters.Length != 2) { throw new ArgumentException("For linear activation function count of input parameters should be equal to 2."); }
        }

        public override double GetResult(double inputValue)
        {
            // a * x + b
            return Parameters[0]*inputValue + Parameters[1];
        }
    }
}