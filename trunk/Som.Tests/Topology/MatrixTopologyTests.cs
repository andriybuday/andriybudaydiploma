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
            Assert.IsTrue(neuronsInRadius.ContainsKey(15));
        }

        [Test]
        public void GetNeuronsInRadius_Radius1()
        {
            var matrixTopology = new BoundMatrixTopology(4, 6);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(15, 1);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(5));
            Assert.IsTrue(neuronsInRadius.ContainsKey(15));
            Assert.IsTrue(neuronsInRadius.ContainsKey(9));
            Assert.IsTrue(neuronsInRadius.ContainsKey(16));
            Assert.IsTrue(neuronsInRadius.ContainsKey(21));
            Assert.IsTrue(neuronsInRadius.ContainsKey(14));
        }

        [Test]
        public void GetNeuronsInRadius_Radius1_DownRightCorner()
        {
            var matrixTopology = new BoundMatrixTopology(4, 6);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(23, 1);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(5));
            Assert.IsTrue(neuronsInRadius.ContainsKey(23));
            Assert.IsTrue(neuronsInRadius.ContainsKey(17));
            Assert.IsTrue(neuronsInRadius.ContainsKey(18));
            Assert.IsTrue(neuronsInRadius.ContainsKey(5));
            Assert.IsTrue(neuronsInRadius.ContainsKey(22));
        }

        [Test]
        public void GetNeuronsInRadius_Radius1_LeftSide()
        {
            var matrixTopology = new BoundMatrixTopology(4, 6);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(6, 1);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(5));
            Assert.IsTrue(neuronsInRadius.ContainsKey(6));
            Assert.IsTrue(neuronsInRadius.ContainsKey(0));
            Assert.IsTrue(neuronsInRadius.ContainsKey(7));
            Assert.IsTrue(neuronsInRadius.ContainsKey(12));
            Assert.IsTrue(neuronsInRadius.ContainsKey(12));
        }

        [Test]
        public void GetNeuronsInRadius_Radius2()
        {
            var matrixTopology = new BoundMatrixTopology(3, 6);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(3, 2);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(11));
            Assert.IsTrue(neuronsInRadius.ContainsKey(1));
            Assert.IsTrue(neuronsInRadius.ContainsKey(2));
            Assert.IsTrue(neuronsInRadius.ContainsKey(3));
            Assert.IsTrue(neuronsInRadius.ContainsKey(4));
            Assert.IsTrue(neuronsInRadius.ContainsKey(5));
            Assert.IsTrue(neuronsInRadius.ContainsKey(8));
            Assert.IsTrue(neuronsInRadius.ContainsKey(9));
            Assert.IsTrue(neuronsInRadius.ContainsKey(10));
            Assert.IsTrue(neuronsInRadius.ContainsKey(14));
            Assert.IsTrue(neuronsInRadius.ContainsKey(15));
            Assert.IsTrue(neuronsInRadius.ContainsKey(16));
        }

        [Test]
        public void GetNeuronsInRadius_ShouldTakeWholeMap()
        {
            var matrixTopology = new BoundMatrixTopology(1, 2);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(1, 7);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(2));
            Assert.IsTrue(neuronsInRadius.ContainsKey(1));
            Assert.IsTrue(neuronsInRadius.ContainsKey(0));
        }

        [Test]
        public void GetNeuronsInRadius_ShouldBeGoodNest_Corner()
        {
            var matrixTopology = new BoundMatrixTopology(10, 10);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(88, 4);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(41));
            Assert.IsTrue(neuronsInRadius.ContainsKey(88));
            Assert.IsTrue(neuronsInRadius.ContainsKey(57));
            Assert.IsTrue(neuronsInRadius.ContainsKey(58));
            Assert.IsTrue(neuronsInRadius.ContainsKey(59));
            Assert.IsTrue(neuronsInRadius.ContainsKey(0));
            Assert.IsTrue(neuronsInRadius.ContainsKey(6));
            Assert.IsTrue(neuronsInRadius.ContainsKey(8));
            Assert.IsTrue(neuronsInRadius.ContainsKey(57));
        }

        [Test]
        public void GetNeuronsInRadius_ShouldBeGoodNest_StartCorner()
        {
            var matrixTopology = new BoundMatrixTopology(10, 10);
            var neuronsInRadius = matrixTopology.GetNeuronsInRadius(0, 2);

            Assert.That(neuronsInRadius.Count, Is.EqualTo(13));
            Assert.IsTrue(neuronsInRadius.ContainsKey(80));
            Assert.IsTrue(neuronsInRadius.ContainsKey(90));
            Assert.IsTrue(neuronsInRadius.ContainsKey(91));
            Assert.IsTrue(neuronsInRadius.ContainsKey(99));
            Assert.IsTrue(neuronsInRadius.ContainsKey(0));
            Assert.IsTrue(neuronsInRadius.ContainsKey(2));
            Assert.IsTrue(neuronsInRadius.ContainsKey(20));
        }

        [Test]
        public void Optimized_GetNeuronsInRadius_RunIt()
        {
            var matrixTopology = new BoundMatrixTopology(10, 10);
            var neuronsInRadius = matrixTopology.Optimized_GetNeuronsInRadius(25, 4);
        }
    }
}
