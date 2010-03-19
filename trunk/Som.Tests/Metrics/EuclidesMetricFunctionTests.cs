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
            var firstVector = new List<double>(){0,0};
            var secondVector = new List<double>(){3,4};
            double distance = euclidesMetricFunction.GetDistance(firstVector, secondVector);

            Assert.That(distance, Is.EqualTo(5));
        }

        [Test]
        public void GetDistance_GetExucutionTime()
        {
            var euclidesMetricFunction = new EuclideanMetricFunction();
            var firstVector = new List<double>();
            var secondVector = new List<double>();

            var random = new Random();
            for (int i = 0; i < 10000000; i++)
            {
                firstVector.Add(random.NextDouble());
                secondVector.Add(random.NextDouble());
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
