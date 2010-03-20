using System;

namespace Som.Learning
{
    public class LinearLearningFactorFunction : LearningFactorFunctionBase
    {
        public LinearLearningFactorFunction(double[] parameters)
            : base(parameters)
        {
            if (parameters.Length != 1) { throw new ArgumentException("For LinearLearningFactorFunction count of input parameters should be equal to 1."); }
        }

        public override double GetLearningRate(double k)
        {
            return (Parameters[0] / Parameters[1]) * (Parameters[0] - k);
        }
    }
}