using System;
using System.Collections.Generic;
using System.IO;

namespace Som.Data
{
    public class TextFileLearningDataPersister : ILearningDataPersister
    {
        private string FileName { get; set; }

        public TextFileLearningDataPersister(string fileName)
        {
            FileName = fileName;
        }
        
        public List<List<double>> GetData()
        {
            var result = new List<List<double>>();

            var streamReader = new StreamReader(FileName);
            int latestRowElements = -1;
            string line;
            while((line = streamReader.ReadLine()) != null)
            {
                var values = new List<double>();
                string[] stringValues = line.Split(new char[] {',', ' ', '\t'});
                foreach (var svalue in stringValues)
                {
                    double value;
                    Double.TryParse(svalue, out value);
                    values.Add(value);
                }

                if (latestRowElements < 0)
                {
                    latestRowElements = values.Count;
                }
                else if(latestRowElements != values.Count)
                {
                    throw new ArgumentException("Input file with learning data is in uncorrect state.");
                }

                result.Add(values);
            }

            return result;
        }

        public void SaveData(List<List<double>> data)
        {
            throw new NotImplementedException();
        }
    }
}