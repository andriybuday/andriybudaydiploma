﻿using System;

namespace Som.Learning
{
    public interface ILearningFactorFunction
    {
        double GetLearningRate(double inputValue);

        double[] Parameters { get; set; }
    }

    public abstract class LearningFactorFunctionBase : ILearningFactorFunction
    {
        protected LearningFactorFunctionBase(double[] parameters)
        {
            if (parameters == null) { throw new ArgumentException("Paramenters should be not null value for any learning function."); }
            Parameters = parameters;
        }

        public abstract double GetLearningRate(double inputValue);

        public double[] Parameters { get; set; }
    }
}