using System;

namespace SomParallelization.Learning
{
    public class GaussFactorFunction : LearningFactorFunctionBase
    {
        public GaussFactorFunction(double[] parameters)
            : base(parameters)
        {
            if (parameters.Length != 1) { throw new ArgumentException("For GaussFactorFunction count of input parameters should be equal to 1."); }
        }

        public override double GetResult(double k)
        {
            return Math.Exp(-k*k/(2*Parameters[0]*Parameters[0]));
        }
    }
}