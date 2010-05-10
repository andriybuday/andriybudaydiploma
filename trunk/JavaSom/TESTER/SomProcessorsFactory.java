package TESTER;

import ActivationFunction.*;
import Data.*;
import Data.Shuffle.*;
import Learning.*;
import LearningProcessor.*;
import Metrics.*;
import Network.*;
import Topology.*;

	public class SomProcessorsFactory
	{
		private int privateIterations;
		public final int getIterations()
		{
			return privateIterations;
		}
		private void setIterations(int value)
		{
			privateIterations = value;
		}
		private int privateDimentions;
		public final int getDimentions()
		{
			return privateDimentions;
		}
		private void setDimentions(int value)
		{
			privateDimentions = value;
		}
		private int privateGridOneSideSize;
		public final int getGridOneSideSize()
		{
			return privateGridOneSideSize;
		}
		private void setGridOneSideSize(int value)
		{
			privateGridOneSideSize = value;
		}
		private double privateStartLearningRate;
		public final double getStartLearningRate()
		{
			return privateStartLearningRate;
		}
		private void setStartLearningRate(double value)
		{
			privateStartLearningRate = value;
		}

		public SomProcessorsFactory(int iterations, int dimentions, int gridOneSideSize, double startLearningRate)
		{
			setIterations(iterations);
			setDimentions(dimentions);
			setGridOneSideSize(gridOneSideSize);
			setStartLearningRate(startLearningRate);
		}

		public final SomLearningProcessor GetStandardLearningProcessor(int iterations, double startLearningRate, int wh, int dimentions)
		{

			//learning data
			ILearningDataProvider LearningDataProvider = new CompletelyRandomDataProvider(dimentions, 1);

			int dataVectorDimention = LearningDataProvider.getDataVectorDimention();
			java.util.ArrayList<Double> maxWeights = new java.util.ArrayList<Double>();
			for (int i = 0; i < dataVectorDimention; i++)
			{
				maxWeights.add(1.0);
			}

			ITopology topology = new SimpleMatrixTopology(wh, wh);

			IActivationFunction activationFunction = new TransparentActivationFunction(new double[] { });

			INetwork network = new NetworkBase(true, maxWeights, activationFunction, topology);

			IMetricFunction metricFunction = new EuclideanMetricFunction();
			ILearningFactorFunction learningFactorFunction = new ExponentionalFactorFunction(startLearningRate, iterations);
			INeighbourhoodFunction neighbourhoodFunction = new GaussNeighbourhoodFunction();
			IShuffleProvider shuffleProvider = new NotShufflingProvider();
			SomLearningProcessor somLearningProcessor = new SomLearningProcessor(LearningDataProvider, network, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, shuffleProvider);

			return somLearningProcessor;
		}

		public final SomLearningProcessor GetParallelLearningProcessor(int iterations, double startLearningRate, int wh, int dimentions)
		{

			//learning data
			ILearningDataProvider LearningDataProvider = new CompletelyRandomDataProvider(dimentions, 1);

			int dataVectorDimention = LearningDataProvider.getDataVectorDimention();
			java.util.ArrayList<Double> maxWeights = new java.util.ArrayList<Double>();
			for (int i = 0; i < dataVectorDimention; i++)
			{
				maxWeights.add(1.0);
			}

			ITopology topology = new SimpleMatrixTopology(wh, wh);

			IActivationFunction activationFunction = new TransparentActivationFunction(new double[] { });

			INetwork network = new NetworkBase(true, maxWeights, activationFunction, topology);

			IMetricFunction metricFunction = new EuclideanMetricFunction();
			ILearningFactorFunction learningFactorFunction = new ExponentionalFactorFunction(startLearningRate, iterations);
			INeighbourhoodFunction neighbourhoodFunction = new GaussNeighbourhoodFunction();
			IShuffleProvider shuffleProvider = new NotShufflingProvider();
			//somLearningProcessor somLearningProcessor = new somLearningProcessor(LearningDataProvider, network, topology, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, shuffleProvider);
			SomLearningProcessor somLearningProcessor = new ConcurrencySomLearningProcessor(LearningDataProvider, network, metricFunction, learningFactorFunction, neighbourhoodFunction, iterations, shuffleProvider);

			return somLearningProcessor;
		}
	}