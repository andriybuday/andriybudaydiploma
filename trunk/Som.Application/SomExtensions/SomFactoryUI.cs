using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Som.ActivationFunction;
using Som.Data;
using Som.Data.Suffle;
using Som.Kohonen;
using Som.Learning;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.Application.SomExtensions
{
    public partial class SomFactoryUI : UserControl
    {
        public SomFactoryUI()
        {
            InitializeComponent();
        }

        private ILearningDataProvider LearningDataProvider { get; set; }
        private IMetricFunction MetricFunction { get; set; }
        private ControllableWtmLearningProcessor LearningProcessor { get; set; }       

        public LearningProcessorBase GetSomProcessor()
        {
            var iterations = (int)numericUpDownIterations.Value;
            var startLearningRate = 0.07;
            Double.TryParse(textBoxLearningRate.Text, out startLearningRate);
            if (startLearningRate <= 0) startLearningRate = 0.07;

            var wh = (int)numericUpDownRows.Value;

            //learning data
            LearningDataProvider = new TwoDimentionsDataProvider(2, 250);

            //GIRL

            //LearningDataProvider = new GirlPointsDataProvider(250);
            //var image = Image.FromFile(@"Pictures\girl.bmp");
            //((GirlPointsDataProvider)LearningDataProvider).GirlImage = image;


            var dataVectorDimention = LearningDataProvider.DataVectorDimention;
            var maxWeights = new List<double>();
            var minWeights = new List<double>();
            for (int i = 0; i < dataVectorDimention; i++)
            {
                minWeights.Add(0.25);
                maxWeights.Add(0.75);
            }

            ITopology topology = new SimpleMatrixTopology(wh, wh);

            IActivationFunction activationFunction = new TransparentActivationFunction(new double[] { });

            INetwork network = new NetworkBase(true, minWeights, maxWeights, activationFunction, topology);

            MetricFunction = new EuclideanMetricFunction();
            ILearningFactorFunction learningFactorFunction = new ExponentionalFactorFunction(startLearningRate, iterations);
            INeighbourhoodFunction neighbourhoodFunction = new GaussNeighbourhoodFunction();
            ISuffleProvider suffleProvider = new NotSufflingProvider();
            LearningProcessor = new ControllableWtmLearningProcessor(
                LearningDataProvider, network, topology, MetricFunction, learningFactorFunction, neighbourhoodFunction, iterations, suffleProvider);

            return LearningProcessor;
        }

    }
}
