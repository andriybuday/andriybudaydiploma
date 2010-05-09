using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Som.Data;
using Som.Data.Shuffle;

namespace Som.Tests.LearningData
{
    [TestFixture]
    public class SuffleListTests
    {
        [Test]
        public void ValidateShuffleMethod()
        {
            var suffleList = new ShuffleList(10);
            Assert.That(suffleList[0], Is.EqualTo(0));
            Assert.That(suffleList[1], Is.EqualTo(1));
            Assert.That(suffleList[9], Is.EqualTo(9));

            suffleList.Suffle();

            int matches = 0;
            for (int i = 0; i < suffleList.Count; i++)
            {
                if (suffleList[i] == i) matches++;
            }

            Assert.That(matches / (double)suffleList.Count, Is.LessThan(0.2));
        }
    }
}
