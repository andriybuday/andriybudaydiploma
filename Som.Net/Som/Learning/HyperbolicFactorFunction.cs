using System;

namespace Som.Learning
{
    public class HyperbolicFactorFunction : LearningFactorFunctionBase
    {
        public HyperbolicFactorFunction(double[] parameters)
            : base(parameters)
        {
            if (parameters.Length != 2) { throw new ArgumentException("For HyperbolicFactorFunction count of input parameters should be equal to 2."); }
        }

        public override double GetLearningRate(double k)
        {
            return Parameters[0]/(Parameters[1] + k);
        }
    }
}