using System;

namespace Som.Topology
{
    public interface INeighbourhoodFunction
    {
        double GetDistanceFalloff(double distance, double radius);
    }
}
