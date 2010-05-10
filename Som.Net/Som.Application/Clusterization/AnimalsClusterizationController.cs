using System;
using System.Collections.Generic;
using System.IO;
using Som.ActivationFunction;
using Som.Application.Base;
using Som.Data.Shuffle;
using Som.Learning;
using Som.Data;
using Som.LearningProcessor;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.Application.Clusterization
{
    public class AnimalsClusterizationController : IScreenController
    {
        private ILearningDataProvider LearningDataProvider { get; set; }
        private IMetricFunction MetricFunction { get; set; }
        private ILearningProcessor SomLearningProcessor;


        public AnimalsClusterizationController()
        {
            UI = new AnimalsClusterization(this);
        }

        public AnimalsClusterization UI { get; private set; }
        public void ShowScreen()
        {
            UI.Show();
        }

        private string FileName { get; set; }
        public void LoadData(string fileName)
        {
            FileName = fileName;
            InitializeSom(fileName);
        }

        private void InitializeSom(string fileName)
        {
            var iterations = 500;
            var startLearningRate = 0.07;
            var wh = 16;

            //learning data
            var textFileLearningDataPersister = new TextFileLearningDataPersister(fileName);
            LearningDataProvider = new LearningDataProvider(textFileLearningDataPersister);
            var dataVectorDimention = LearningDataProvider.DataVectorDimention;
            var maxWeights = new List<double>();
            for (int i = 0; i < dataVectorDimention; i++)
                maxWeights.Add(1);

            ITopology topology = new SimpleMatrixTopology(wh, wh);
            //ITopology topology = new BoundMatrixTopology(wh, wh);

            IActivationFunction activationFunction = new TransparentActivationFunction(new double[] { });

            INetwork network = new NetworkBase(true, null, maxWeights, activationFunction, topology);

            MetricFunction = new EuclideanMetricFunction();
            ILearningFactorFunction learningFactorFunction = new ExponentionalFactorFunction(startLearningRate, iterations);
            INeighbourhoodFunction neighbourhoodFunction = new GaussNeighbourhoodFunction();
            IShuffleProvider shuffleProvider = new ShuffleProvider();

            SomLearningProcessor = new DivideGridAndAccomodationArea(
                LearningDataProvider, network, MetricFunction, learningFactorFunction, neighbourhoodFunction, 1000, shuffleProvider, 2);
            //SomLearningProcessor = new SomLearningProcessor(
            //    LearningDataProvider, network, MetricFunction, learningFactorFunction, neighbourhoodFunction, 1000, shuffleProvider);
        }

        public void Learn()
        {
            SomLearningProcessor.Learn();
        }

        public ITopology GetTopology()
        {
            return SomLearningProcessor.Topology;
        }

        public IList<INeuron> GetNetworkNeurons()
        {
            return SomLearningProcessor.Network.Neurons;
        }

        public string GetBestName(INeuron networkNeuron)
        {
            string bestName = string.Empty;
            List<string> names = LoadNames();
            double minDistance = Double.MaxValue;
            for (int i = 0; i < LearningDataProvider.LearningVectorsCount; i++)
            {
                var dataVector = LearningDataProvider.GetLearingDataVector(i);
                var distance = MetricFunction.GetDistance(networkNeuron.Weights, dataVector);
                if(minDistance > distance)
                {
                    minDistance = distance;
                    bestName = names[i];
                }
            }
            return bestName;
        }

        private List<string> LoadNames()
        {
            var names = new List<string>();
            using(var streamReader = new StreamReader(FileName))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var strings = line.Split(new char[] {' ', ','});
                    double d;
                    var firstParsable = Double.TryParse(strings[0], out d);
                    //var lastParsable = Double.TryParse(strings[strings.Length - 1], out d);
                    if(firstParsable)
                    {
                        names.Add(strings[strings.Length - 1]);    
                    }
                    else
                    {
                        names.Add(strings[0]);    
                    }
                }
            }
            return names;
        }
    }
}