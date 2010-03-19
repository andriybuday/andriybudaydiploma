using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Som.ActivationFunction;
using Som.Application.Base;
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
            UI = new GridTest(this);
        }

        protected GridTest UI{ get; set;}

        public void ShowScreen()
        {
            UI.Show();
        }


        private ILearningDataProvider LearningDataProvider { get; set; }
        private IMetricFunction MetricFunction { get; set; }
        private LearningProcessorBase LearningProcessor;

        public void InitializeSom()
        {
            //learning data
            //LearningDataProvider = new CompletelyRandomDataProvider(2, 250);
            LearningDataProvider = new TwoDimentionsDataProvider(2, 100);
            var dataVectorDimention = LearningDataProvider.DataVectorDimention;
            var maxWeights = new List<double>();
            var minWeights = new List<double>();
            for (int i = 0; i < dataVectorDimention; i++)
            {
                minWeights.Add(0.25);
                maxWeights.Add(0.75);
            }

            ITopology topology = new SimpleMatrixTopology(3, 3, 2);

            IActivationFunction activationFunction = new TransparentActivationFunction(new double[] { });
            //IActivationFunction activationFunction = new LinearActivationFunction(new double[] {0.2, 0.3});

            INetwork network = new NetworkBase(true, minWeights, maxWeights, activationFunction, topology);

            MetricFunction = new EuclideanMetricFunction();
            //ILearningFactorFunction learningFactorFunction = new GaussFactorFunction(new double[] { 0.5 });
            ILearningFactorFunction learningFactorFunction = new ConstantFactorFunction(new double[] {0.1});
            INeighbourhoodFunction neighbourhoodFunction = new GaussNeighbourhoodFunction(5);
            ISuffleProvider suffleProvider = new NotSufflingProvider();
            LearningProcessor = new WTMLearningProcessor(
                LearningDataProvider, network, topology, MetricFunction, learningFactorFunction, neighbourhoodFunction, 3000, suffleProvider);

            LearningProcessor.NewEpochStarted += new LearningProcessorBase.LearningProcessingHandler<NewEpochStartedEvenArgs>(LearningProcessor_NewEpochStarted);

            UI.DrawPoints(LearningProcessor.Network.Neurons.Select(x => x.Weights.ToList()).ToList());
        }

        public int Iteration { get; private set; }
        public void LearningProcessor_NewEpochStarted(object sender, NewEpochStartedEvenArgs e)
        {
            Iteration = e.Iteration;
            UI.UpdateUI();
        }

        public void Learn()
        {
            LearningProcessor.Learn();
            UI.DrawPoints(LearningProcessor.Network.Neurons.Select(x => x.Weights.ToList()).ToList());
        }
    }
}
