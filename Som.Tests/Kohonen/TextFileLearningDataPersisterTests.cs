using System.Collections.Generic;
using NUnit.Framework;
using Som.Data;

namespace Som.Tests.Kohonen
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
