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
            var matrixTopology = new SimpleMatrixTopology(10, 10, 3);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(77);

        }
    }
}