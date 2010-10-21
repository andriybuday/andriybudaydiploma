using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Som.ActivationFunction;
using Som.Concurrency;
using Som.Data;
using Som.Data.Shuffle;
using Som.Learning;
using Som.LearningProcessor;
using Som.Metrics;
using Som.Network;
using Som.StandardDividing;
using Som.Topology;

namespace Som.Application.SomExtensions
{
    public partial class SomFactoryUI : UserControl
    {
        public SomFactoryUI()
        {
            InitializeComponent();
            comboBoxProcessorType.SelectedIndex = 0;
            comboBoxDataProvider.SelectedIndex = 0;
        }

        public ILearningDataProvider LearningDataProvider { get; set; }
        public IMetricFunction MetricFunction { get; set; }

        public ILearningProcessor GetSomProcessor()
        {
            var iterations = (int)numericUpDownIterations.Value;
            var startLearningRate = 0.07;
            Double.TryParse(textBoxLearningRate.Text, out startLearningRate);
            if (startLearningRate <= 0) startLearningRate = 0.07;

            var inputVectorsCount = (int)numericUpDownInputSpaceNumber.Value;

            var wh = (int)numericUpDownRows.Value;

            //learning data
            if(comboBoxDataProvider.SelectedIndex == 0)
            {
                LearningDataProvider = new CompletelyRandomDataProvider(2, inputVectorsCount);    
            }
            else
            {
                LearningDataProvider = new GirlPointsDataProvider(inputVectorsCount);
                var image = Image.FromFile(@"Pictures\girl.bmp");
                ((GirlPointsDataProvider)LearningDataProvider).GirlImage = image;    
            }

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
            IShuffleProvider shuffleProvider = new NotShufflingProvider();

            ILearningProcessor somLearningProcessor;
            if(comboBoxProcessorType.SelectedIndex == 0)
            {
                somLearningProcessor = new StandardSomLearningProcessor(LearningDataProvider, network, MetricFunction, learningFactorFunction,
                                                                    neighbourhoodFunction, iterations, shuffleProvider);    
            }
            else
            {
                somLearningProcessor = new DivideGridV2(LearningDataProvider, network, MetricFunction, learningFactorFunction,
                                                                    neighbourhoodFunction, iterations, shuffleProvider, 2);
            }
            return somLearningProcessor;
        }
    }
}
