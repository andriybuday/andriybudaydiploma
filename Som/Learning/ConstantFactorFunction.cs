using System;

namespace Som.Learning
{
    public class ConstantFactorFunction : LearningFactorFunctionBase
    {
        public ConstantFactorFunction(double[] parameters)
            : base(parameters)
        {
            if (parameters.Length != 1) { throw new ArgumentException("For ConstantFactorFunction count of input parameters should be equal to 1."); }
        }

        public override double GetResult(double k)
        {
            return Parameters[0];
        }
    }
}