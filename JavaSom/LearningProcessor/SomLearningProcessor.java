package LearningProcessor;

import Data.Shuffle.*;
import Learning.*;
import Data.*;
import Metrics.*;
import Network.*;
import Topology.*;

	public class SomLearningProcessor implements ILearningProcessor
	{
		private INetwork privateNetwork;
		public final INetwork getNetwork()
		{
			return privateNetwork;
		}
		private void setNetwork(INetwork value)
		{
			privateNetwork = value;
		}
		private ITopology privateTopology;
		public final ITopology getTopology()
		{
			return privateTopology;
		}
		private void setTopology(ITopology value)
		{
			privateTopology = value;
		}

		private IShuffleProvider privateShuffleProvider;
		protected final IShuffleProvider getShuffleProvider()
		{
			return privateShuffleProvider;
		}
		protected final void setShuffleProvider(IShuffleProvider value)
		{
			privateShuffleProvider = value;
		}
		private ILearningDataProvider privateLearningDataProvider;
		protected final ILearningDataProvider getLearningDataProvider()
		{
			return privateLearningDataProvider;
		}
		protected final void setLearningDataProvider(ILearningDataProvider value)
		{
			privateLearningDataProvider = value;
		}
		private IRadiusProvider privateRadiusProvider;
		protected final IRadiusProvider getRadiusProvider()
		{
			return privateRadiusProvider;
		}
		private void setRadiusProvider(IRadiusProvider value)
		{
			privateRadiusProvider = value;
		}
		private INeighbourhoodFunction privateNeighbourhoodFunction;
		protected final INeighbourhoodFunction getNeighbourhoodFunction()
		{
			return privateNeighbourhoodFunction;
		}
		protected final void setNeighbourhoodFunction(INeighbourhoodFunction value)
		{
			privateNeighbourhoodFunction = value;
		}
		private IMetricFunction privateMetricFunction;
		protected final IMetricFunction getMetricFunction()
		{
			return privateMetricFunction;
		}
		protected final void setMetricFunction(IMetricFunction value)
		{
			privateMetricFunction = value;
		}
		private ILearningFactorFunction privateLearningFactorFunction;
		protected final ILearningFactorFunction getLearningFactorFunction()
		{
			return privateLearningFactorFunction;
		}
		protected final void setLearningFactorFunction(ILearningFactorFunction value)
		{
			privateLearningFactorFunction = value;
		}

		private int privateMaxIterationsCount;
		public final int getMaxIterationsCount()
		{
			return privateMaxIterationsCount;
		}
		protected final void setMaxIterationsCount(int value)
		{
			privateMaxIterationsCount = value;
		}

		public SomLearningProcessor(ILearningDataProvider learningDataProvider, INetwork network, IMetricFunction metricFunction, ILearningFactorFunction learningFactorFunction, INeighbourhoodFunction neighbourhoodFunction, int maxIterationsCount, IShuffleProvider shuffleProvider)
		{
			setLearningDataProvider(learningDataProvider);
			setNetwork(network);
			setTopology(network.getTopology());
			setMetricFunction(metricFunction);
			setLearningFactorFunction(learningFactorFunction);
			setNeighbourhoodFunction(neighbourhoodFunction);
			setMaxIterationsCount(maxIterationsCount);
			setShuffleProvider(shuffleProvider);

			setRadiusProvider(new DefaultRadiusProvider(maxIterationsCount, getTopology().getWholeTopologyRadius()));
		}

		public void Learn()
		{
			int vectorsCount = getLearningDataProvider().getLearningVectorsCount();
			java.util.List<Integer> suffleList = new ShuffleList(vectorsCount);

			for (int iteration = 0; iteration < getMaxIterationsCount(); iteration++)
			{
				//if ((iteration % 1000) == 0) Console.Write(string.Format("{0} ", iteration));
				suffleList = getShuffleProvider().Suffle(suffleList);

				for (int dataInd = 0; dataInd < vectorsCount; dataInd++)
				{
					double[] dataVector = getLearningDataProvider().GetLearingDataVector(suffleList.get(dataInd));

					int bestNeuronNum = FindBestMatchingNeuron(dataVector);

					AccommodateNetworkWeights(bestNeuronNum, dataVector, iteration);
				}
			}
		}

		public int FindBestMatchingNeuron(double[] dataVector)
		{
			int result = -1;
			double minDistance = Double.MAX_VALUE;
			for (int i = 0; i < getNetwork().getNeurons().size(); i++)
			{
				double distance = getMetricFunction().GetDistance(getNetwork().getNeurons().get(i).getWeights(), dataVector);
				if (distance < minDistance)
				{
					minDistance = distance;
					result = i;
				}
			}
			return result;
		}

		public void AccommodateNetworkWeights(int bestNeuronNum, double[] dataVector, int iteration)
		{
			double radius = getRadiusProvider().GetRadius(iteration);
			java.util.HashMap<Integer, Double> effectedNeurons = getTopology().GetNeuronsInRadius(bestNeuronNum, radius);

			for (int effectedNeuron : effectedNeurons.keySet())
			{
				double distance = effectedNeurons.get(effectedNeuron);

				AccommodateNeuronWeights(effectedNeuron, dataVector, iteration, distance, radius);
			}
		}

		protected void AccommodateNeuronWeights(int neuronNumber, double[] dataVector, int iteration, double distance, double radius)
		{
			double[] neuronWghts = getNetwork().getNeurons().get(neuronNumber).getWeights();

			double learningRate = getLearningFactorFunction().GetLearningRate(iteration);
			double falloffRate = getNeighbourhoodFunction().GetDistanceFalloff(distance, radius);

			for (int i = 0; i < neuronWghts.length; i++)
			{
				double weight = neuronWghts[i];
				neuronWghts[i] += learningRate * falloffRate * (dataVector[i] - weight);
			}
		}
	}