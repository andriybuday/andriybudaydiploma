using System;
using Som.Application.Base;
using Som.Application.SomExtensions;
using Som.Data;
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


        public ControllableWtmLearningProcessor LearningProcessor { get; private set; }

        public void InitializeSom(ControllableWtmLearningProcessor learningProcessor)
        {
            LearningProcessor = learningProcessor;

            UI.DrawNeuroNet(LearningProcessor.Network.Neurons);
        }

        public int Iteration { get; private set; }

        public void Next(int iterations)
        {
            double[] dataPoint;
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    dataPoint = new double[] { Random.NextDouble(), Random.NextDouble() };
                    LearningProcessor.Next(dataPoint);        
                }
                LearningProcessor.IncrementIteration();
            }
            
            Iteration = LearningProcessor.Iteration;

            UI.DrawNeuroNet(LearningProcessor.Network.Neurons);

            UI.UpdateUI();
        }

        public Random Random { get; private set; }

        public void Learn()
        {
            LearningProcessor.Learn();
            UI.DrawNeuroNet(LearningProcessor.Network.Neurons);
            UI.UpdateUI();
        }
    }
}
