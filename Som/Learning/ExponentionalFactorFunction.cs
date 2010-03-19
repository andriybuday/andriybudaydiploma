using System;

namespace Som.Learning
{
    public class ExponentionalFactorFunction : LearningFactorFunctionBase
    {
        public ExponentionalFactorFunction(double[] parameters)
            : base(parameters)
        {
            if (parameters.Length != 2) { throw new ArgumentException("For ExponentionalFactorFunction count of input parameters should be equal to 2."); }
        }

        public override double GetResult(double k)
        {
            return Parameters[0]*Math.Exp(-Parameters[1]*k);
        }
    }
}