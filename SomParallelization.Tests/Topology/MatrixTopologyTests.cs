using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using SomParallelization.Topology;

namespace SomParallelization.Tests.Topology
{
    [TestFixture]
    public class MatrixTopologyTests
    {
        [Test]
        public void GetNeuronsInRadius_Radius0()
        {
            var matrixTopology = new MatrixTopology(4, 6, 0);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(15);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(1));
            Assert.IsTrue(neuronsInRadius.Contains(15));
        }

        [Test]
        public void GetNeuronsInRadius_Radius1()
        {
            var matrixTopology = new MatrixTopology(4, 6, 1);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(15);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(5));
            Assert.IsTrue(neuronsInRadius.Contains(15));
            Assert.IsTrue(neuronsInRadius.Contains(9));
            Assert.IsTrue(neuronsInRadius.Contains(16));
            Assert.IsTrue(neuronsInRadius.Contains(21));
            Assert.IsTrue(neuronsInRadius.Contains(14));
        }

        [Test]
        public void GetNeuronsInRadius_Radius1_DownRightCorner()
        {
            var matrixTopology = new MatrixTopology(4, 6, 1);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(23);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(5));
            Assert.IsTrue(neuronsInRadius.Contains(23));
            Assert.IsTrue(neuronsInRadius.Contains(17));
            Assert.IsTrue(neuronsInRadius.Contains(18));
            Assert.IsTrue(neuronsInRadius.Contains(5));
            Assert.IsTrue(neuronsInRadius.Contains(22));
        }

        [Test]
        public void GetNeuronsInRadius_Radius1_LeftSide()
        {
            var matrixTopology = new MatrixTopology(4, 6, 1);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(6);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(5));
            Assert.IsTrue(neuronsInRadius.Contains(6));
            Assert.IsTrue(neuronsInRadius.Contains(0));
            Assert.IsTrue(neuronsInRadius.Contains(7));
            Assert.IsTrue(neuronsInRadius.Contains(12));
            Assert.IsTrue(neuronsInRadius.Contains(12));
        }

        [Test]
        public void GetNeuronsInRadius_Radius2()
        {
            var matrixTopology = new MatrixTopology(3, 6, 2);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(3);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(11));
            Assert.IsTrue(neuronsInRadius.Contains(1));
            Assert.IsTrue(neuronsInRadius.Contains(2));
            Assert.IsTrue(neuronsInRadius.Contains(3));
            Assert.IsTrue(neuronsInRadius.Contains(4));
            Assert.IsTrue(neuronsInRadius.Contains(5));
            Assert.IsTrue(neuronsInRadius.Contains(8));
            Assert.IsTrue(neuronsInRadius.Contains(9));
            Assert.IsTrue(neuronsInRadius.Contains(10));
            Assert.IsTrue(neuronsInRadius.Contains(14));
            Assert.IsTrue(neuronsInRadius.Contains(15));
            Assert.IsTrue(neuronsInRadius.Contains(16));
        }
    }
}
