using System;
using Som.Application.Base;
using Som.Application.SomExtensions;
using Som.Data;
using Som.LearningProcessor;
using Som.Metrics;

namespace Som.Application.Grid
{
    public class GridController : IScreenController
    {
        public GridController()
        {
            Random = new Random();
            UI = new GridTest(this);
        }

        protected GridTest UI{ get; set;}

        public void ShowScreen()
        {
            UI.Show();
        }


        public SomProcessorController ProcessorController { get; private set; }
        public ILearningDataProvider DataProvider { get; private set; }

        public void InitializeSom(SomLearningProcessor processorController, ILearningDataProvider dataProvider)
        {
            ProcessorController = new SomProcessorController(processorController);
            DataProvider = dataProvider;

            UI.DrawNeuroNet(ProcessorController.GetNetworkNeurons());
        }

        public int Iteration { get; private set; }

        public void Next(int iterations)
        {
            
            double[] dataPoint;
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < ProcessorController.GetDataVectorsCount(); j++)
                {
                    dataPoint = DataProvider.GetLearingDataVector(j);
                    ProcessorController.Next(dataPoint);        
                }
                ProcessorController.IncrementIteration();
            }
            
            Iteration = ProcessorController.Iteration;

            UI.DrawNeuroNet(ProcessorController.GetNetworkNeurons());

            UI.UpdateUI();
        }

        public Random Random { get; private set; }

        public void Learn()
        {
            ProcessorController.Learn();
            UI.DrawNeuroNet(ProcessorController.GetNetworkNeurons());
            UI.UpdateUI();
        }

        public int GetMaxIterations()
        {
            return ProcessorController.LearningProcessor.MaxIterationsCount;
        }
    }
}
