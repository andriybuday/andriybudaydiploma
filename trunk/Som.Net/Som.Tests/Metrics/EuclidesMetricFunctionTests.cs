using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Som.Metrics;

namespace Som.Tests.Metrics
{
    [TestFixture]
    public class EuclidesMetricFunctionTests
    {
        [Test]
        public void GetDistance_EnsureReturnsCorrectResult()
        {
            var euclidesMetricFunction = new EuclideanMetricFunction();
            var firstVector = new double[]{0,0};
            var secondVector = new double[]{ 3, 4 };
            double distance = euclidesMetricFunction.GetDistance(firstVector, secondVector);

            Assert.That(distance, Is.EqualTo(5));
        }

        [Test]
        public void GetDistance_GetExucutionTime()
        {
            var euclidesMetricFunction = new EuclideanMetricFunction();
            var N = 10000000;
            var firstVector = new double[N];
            var secondVector = new double[N];

            var random = new Random();
            for (int i = 0; i < N; i++)
            {
                firstVector[i] = random.NextDouble();
                secondVector[i] = random.NextDouble();
            }
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            double distance = euclidesMetricFunction.GetDistance(firstVector, secondVector);
            stopWatch.Stop();

            long elapsedMilliseconds = stopWatch.ElapsedMilliseconds;

            //lambda gives 17-19 ms
        }
    }
}
