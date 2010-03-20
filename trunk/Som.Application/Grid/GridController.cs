using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Som.ActivationFunction;
using Som.Application.Base;
using Som.Application.SomExtensions;
using Som.Data;
using Som.Data.Suffle;
using Som.Kohonen;
using Som.Learning;
using Som.Metrics;
using Som.Network;
using Som.Topology;

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


        private ILearningDataProvider LearningDataProvider { get; set; }
        private IMetricFunction MetricFunction { get; set; }
        public ControllableWtmLearningProcessor LearningProcessor { get; private set; }

        public void InitializeSom(ControllableWtmLearningProcessor learningProcessor)
        {
            LearningProcessor = learningProcessor;

            UI.DrawNeuroNet(LearningProcessor.Network.Neurons);
        }

        public int Iteration { get; private set; }
        public void LearningProcessor_NewEpochStarted(object sender, NewEpochStartedEvenArgs e)
        {
            Iteration = e.Iteration;
            UI.UpdateUI();
        }

        public void Next(int iterations)
        {
            List<double> dataPoint;
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < 250; j++)
                {
                    dataPoint = new List<double>() { Random.NextDouble(), Random.NextDouble() };
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
