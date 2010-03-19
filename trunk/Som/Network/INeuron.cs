using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Som.ActivationFunction;

namespace Som.Network
{
    public interface INeuron
    {
        IList<Double> Weights { get; set; }

        Double GetReaction(IList<Double> inputVector);

        IActivationFunction ActivationFunction { get; set; }
    }
}
