using System;

namespace Som.ActivationFunction
{
    public interface IActivationFunction
    {
        double GetResult(double inputValue);

        double[] Parameters { get; set; }
    }

    public abstract class ActivationFunctionBase : IActivationFunction
    {
        protected ActivationFunctionBase(double[] parameters)
        {
            if (parameters == null) { throw new ArgumentException("Paramenters should be not null value for any activation function."); }
            Parameters = parameters;
        }

        public abstract double GetResult(double inputValue);

        public double[] Parameters { get; set; }
    }
}


