using System;
using System.Collections.Generic;
using NUnit.Framework;
using Som.Topology;

namespace Som.Tests.Topology
{
    [TestFixture]
    public class SimpleMatrixTopologyTests
    {
        [Test]
        public void GetNeuronsInRadius_Radius0()
        {
            var matrixTopology = new SimpleMatrixTopology(10, 10);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(77, 3.01);
            neuronsInRadius =  new List<int>();
        }

    }
}