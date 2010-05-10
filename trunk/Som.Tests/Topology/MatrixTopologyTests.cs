using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Som.Topology;

namespace Som.Tests.Topology
{
    [TestFixture]
    public class MatrixTopologyTests
    {
        [Test]
        public void GetNeuronsInRadius_Radius0()
        {
            var matrixTopology = new BoundMatrixTopology(4, 6);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(15, 0);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(1));
            Assert.IsTrue(neuronsInRadius.Contains(15));
        }

        [Test]
        public void GetNeuronsInRadius_Radius1()
        {
            var matrixTopology = new BoundMatrixTopology(4, 6);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(15, 1);

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
            var matrixTopology = new BoundMatrixTopology(4, 6);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(23, 1);

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
            var matrixTopology = new BoundMatrixTopology(4, 6);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(6, 1);

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
            var matrixTopology = new BoundMatrixTopology(3, 6);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(3, 2);

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

        [Test]
        public void GetNeuronsInRadius_ShouldTakeWholeMap()
        {
            var matrixTopology = new BoundMatrixTopology(1, 2);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(1, 7);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(2));
            Assert.IsTrue(neuronsInRadius.Contains(1));
            Assert.IsTrue(neuronsInRadius.Contains(0));
        }

        [Test]
        public void GetNeuronsInRadius_ShouldBeGoodNest_Corner()
        {
            var matrixTopology = new BoundMatrixTopology(10, 10);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(88, 4);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(41));
            Assert.IsTrue(neuronsInRadius.Contains(88));
            Assert.IsTrue(neuronsInRadius.Contains(57));
            Assert.IsTrue(neuronsInRadius.Contains(58));
            Assert.IsTrue(neuronsInRadius.Contains(59));
            Assert.IsTrue(neuronsInRadius.Contains(0));
            Assert.IsTrue(neuronsInRadius.Contains(6));
            Assert.IsTrue(neuronsInRadius.Contains(8));
            Assert.IsTrue(neuronsInRadius.Contains(57));
        }

        [Test]
        public void GetNeuronsInRadius_ShouldBeGoodNest_StartCorner()
        {
            var matrixTopology = new BoundMatrixTopology(10, 10);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(0, 2);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(13));
            Assert.IsTrue(neuronsInRadius.Contains(80));
            Assert.IsTrue(neuronsInRadius.Contains(90));
            Assert.IsTrue(neuronsInRadius.Contains(91));
            Assert.IsTrue(neuronsInRadius.Contains(99));
            Assert.IsTrue(neuronsInRadius.Contains(0));
            Assert.IsTrue(neuronsInRadius.Contains(2));
            Assert.IsTrue(neuronsInRadius.Contains(20));
        }

        [Test]
        public void Optimized_GetNeuronsInRadius_RunIt()
        {
            var matrixTopology = new BoundMatrixTopology(10, 10);
            var neuronsInRadius = matrixTopology.Optimized_GetNeuronsInRadius(25, 4);
        }
    }
}
