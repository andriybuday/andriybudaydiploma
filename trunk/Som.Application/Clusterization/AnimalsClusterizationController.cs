using System;
using System.Collections.Generic;
using System.IO;
using Som.ActivationFunction;
using Som.Data.Suffle;
using Som.Kohonen;
using Som.Learning;
using Som.Data;
using Som.Metrics;
using Som.Network;
using Som.Topology;

namespace Som.Application.Clusterization
{
    public class AnimalsClusterizationController
    {
        private ILearningDataProvider LearningDataProvider { get; set; }
        private IMetricFunction MetricFunction { get; set; }
        private LearningProcessorBase LearningProcessor;

        private string FileName { get; set; }

        public void LoadData(string fileName)
        {
            FileName = fileName;
            InitializeSom(fileName);
        }

        private void InitializeSom(string fileName)
        {
            //learning data
            var textFileLearningDataPersister = new TextFileLearningDataPersister(fileName);
            LearningDataProvider = new LearningDataProvider(textFileLearningDataPersister);
            var dataVectorDimention = LearningDataProvider.DataVectorDimention;
            var maxWeights = new List<double>();
            for (int i = 0; i < dataVectorDimention; i++)
                maxWeights.Add(1);

            ITopology topology = new BoundMatrixTopology(20, 20, 6);

            IActivationFunction activationFunction = new TransparentActivationFunction(new double[] {});
            INetwork network = new NetworkBase(true, maxWeights, activationFunction, topology );

            MetricFunction = new EuclideanMetricFunction();
            ILearningFactorFunction learningFactorFunction = new GaussFactorFunction(new double[]{0.5});
            INeighbourhoodFunction neighbourhoodFunction = new GaussNeighbourhoodFunction(6);
            ISuffleProvider suffleProvider = new SuffleProvider();
            LearningProcessor = new WTMLearningProcessor(
                LearningDataProvider, network, topology, MetricFunction, learningFactorFunction, neighbourhoodFunction, 1000, suffleProvider);
        }

        public void Learn()
        {
            LearningProcessor.Learn();
        }

        public ITopology GetTopology()
        {
            return LearningProcessor.Topology;
        }

        public IList<INeuron> GetNetworkNeurons()
        {
            return LearningProcessor.Network.Neurons;
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
                    var strings = line.Split(new char[] {' '});
                    names.Add(strings[0]);
                }
            }
            return names;
        }
    }
}