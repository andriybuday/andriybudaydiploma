using System;
using System.Collections.Generic;
using Som.LearningProcessor;
using Som.Network;

namespace Som.Application.SomExtensions
{
    public class SomProcessorController
    {
        public int Iteration { get; private set; }
        public int BmnIndex { get; private set; }
        public IList<double> BestMatchingNeuronWeights { get; private set; }
        public double[] DataVector { get; private set; }

        public SomLearningProcessor LearningProcessor { get; private set; }

        public SomProcessorController(SomLearningProcessor learningProcessor)
        {
            LearningProcessor = learningProcessor;
            Reset();
        }

        public void Reset()
        {
            Iteration = 0;
        }

        public void Next(double[] dataVector)
        {
            DataVector = dataVector;
            BmnIndex = LearningProcessor.FindBestMatchingNeuron(dataVector);
            BestMatchingNeuronWeights = LearningProcessor.Network.Neurons[BmnIndex].Weights;
            LearningProcessor.AccommodateNetworkWeights(BmnIndex, dataVector, Iteration);
        }

        public void IncrementIteration()
        {
            Iteration++;            
        }

        public IList<INeuron> GetNetworkNeurons()
        {
            return LearningProcessor.Network.Neurons;
        }

        public void Learn()
        {
            LearningProcessor.Learn();
        }

        public int GetDataVectorsCount()
        {
            return 10; // we need to change this in future
        }
    }
}
