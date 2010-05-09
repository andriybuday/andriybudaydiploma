﻿using System;
using System.Collections.Generic;
using Som.ActivationFunction;

namespace Som.Network
{
    public interface INeuron
    {
        double[] Weights { get; set; }

        Double GetReaction(IList<Double> inputVector);

        IActivationFunction ActivationFunction { get; set; }
    }
}