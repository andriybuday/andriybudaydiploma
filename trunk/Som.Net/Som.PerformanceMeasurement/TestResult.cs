using System;
using System.Collections.Generic;

namespace PerformanceMeasurement
{
    [Serializable]
    public class TestResult
    {
        public TestResult(){}
        public TestResult(int iterations, string gridSize, int inputSpaceDimentions)
        {
            Iterations = iterations;
            GridSize = gridSize;
            InputSpaceDimentions = inputSpaceDimentions;
            TimeStat = new List<AlgorithmTime>();
        }

        public int Iterations { get; set; }
        public string GridSize { get; set; }
        public int InputSpaceDimentions { get; set; }
        public List<AlgorithmTime> TimeStat { get; set; }
    }

    [Serializable]
    public class AlgorithmTime
    {
        public AlgorithmTime() { }
        public AlgorithmTime(string algoName, double aveTime)
        {
            AlgoName = algoName;
            AverageTime = aveTime;
        }

        public string AlgoName { get; set; }
        public double AverageTime { get; set; }
    }
}