using System;

namespace Som.Learning
{
    public class ExponentionalFactorFunction : LearningFactorFunctionBase
    {
        public double StartLearningRate { get; private set; }
        public double MaxIterationsCount { get; private set; }

        public ExponentionalFactorFunction(double startRate, int maxIterationsCount)
            : base(new double[]{startRate, maxIterationsCount})
        {
            StartLearningRate = startRate;
            MaxIterationsCount = maxIterationsCount;
        }

        public override double GetLearningRate(double iteration)
        {
            return StartLearningRate * Math.Exp(- iteration / MaxIterationsCount);
        }
    }
}