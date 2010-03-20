using System;

namespace Som.Topology
{
    public class DefaultRadiusProvider : IRadiusProvider
    {
        public int MaxIterations { get; private set; }
        public double TopologyRadius { get; private set; }
        public double TimeConstant { get; private set; }

        public DefaultRadiusProvider(int maxIterations, double startRadius)
        {
            MaxIterations = maxIterations;
            TopologyRadius = startRadius;

            TimeConstant = maxIterations / Math.Log(startRadius);
        }

        public double GetRadius(int iteration)
        {
            return TopologyRadius*Math.Exp(-iteration/TimeConstant);
        }
    }
}