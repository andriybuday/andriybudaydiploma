using System;
using System.Collections.Generic;
using System.IO;
using SomParallelization.ActivationFunction;
using SomParallelization.Kohonen;
using SomParallelization.Learning;
using SomParallelization.LearningData;
using SomParallelization.Metrics;
using SomParallelization.Network;
using SomParallelization.Topology;

namespace SomParallelization.Demo
{
    public class AnimalsClusterizationController
    {
        private ILearningData LearningData { get; set; }
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
            LearningData = new LearningData.LearningData(new TextFileLearningDataPersister(fileName));
            var dataVectorDimention = LearningData.DataVectorDimention;
            var maxWeights = new List<double>();
            for (int i = 0; i < dataVectorDimention; i++)
                maxWeights.Add(1);

            ITopology topology = new MatrixTopology(15, 10, 5);

            IActivationFunction activationFunction = new TransparentActivationFunction(new double[] {});
            INetwork network = new NetworkBase(true, maxWeights, activationFunction, topology );

            MetricFunction = new EuclideanMetricFunction();
            ILearningFactorFunction learningFactorFunction = new GaussFactorFunction(new double[]{0.5});
            INeighbourhoodFunction neighbourhoodFunction = new GaussNeighbourhoodFunction(new double[]{5});
            LearningProcessor = new WTMLearningProcessor(
                LearningData, network, topology, MetricFunction, learningFactorFunction, neighbourhoodFunction, 3000);
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
            for (int i = 0; i < LearningData.LearningVectorsCount; i++)
            {
                var dataVector = LearningData.GetLearingDataVector(i);
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