using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SomParallelization.Kohonen;
using SomParallelization.LearningData;

namespace SomParallelization.Tests.Kohonen
{
    [TestFixture]
    public class TextFileLearningDataPersisterTests
    {
        public void ReadFileToGetData()
        {
            var textFileLearningDataPersister = new TextFileLearningDataPersister("animals.txt");
            List<List<double>> list = textFileLearningDataPersister.GetData();
        }
    }
}
