package LearningProcessor;

import Data.*;
import Data.Shuffle.*;
import Learning.*;
import Metrics.*;
import Network.*;
import Topology.*;

import java.util.concurrent.CyclicBarrier;

public class ConcurrencySomLearningProcessor extends SomLearningProcessor
	{
		private int privateGridDivideNumber;
		public final int getGridDivideNumber()
		{
			return privateGridDivideNumber;
		}
		private void setGridDivideNumber(int value)
		{
			privateGridDivideNumber = value;
		}
		private java.util.HashMap<Integer, Double> privateBestMatchingNeurons;
		private java.util.HashMap<Integer, Double> getBestMatchingNeurons()
		{
			return privateBestMatchingNeurons;
		}
		private void setBestMatchingNeurons(java.util.HashMap<Integer, Double> value)
		{
			privateBestMatchingNeurons = value;
		}
		private ManualResetEvent[] privateDoneEvents;
		private ManualResetEvent[] getDoneEvents()
		{
			return privateDoneEvents;
		}
		private void setDoneEvents(ManualResetEvent[] value)
		{
			privateDoneEvents = value;
		}

		public ConcurrencySomLearningProcessor(ILearningDataProvider learningDataProvider, INetwork network, IMetricFunction metricFunction, ILearningFactorFunction learningFactorFunction, INeighbourhoodFunction neighbourhoodFunction, int maxIterationsCount, IShuffleProvider shuffleProvider)
		{
			super(learningDataProvider, network, metricFunction, learningFactorFunction, neighbourhoodFunction, maxIterationsCount, shuffleProvider);
			setGridDivideNumber(2);
			setDoneEvents(new ManualResetEvent[getGridDivideNumber()]);
		}

		@Override
		public int FindBestMatchingNeuron(double[] dataVector)
		{
			int result = -1;
			double minDistance = Double.MAX_VALUE;
			int neuronsCount = getNetwork().getNeurons().size();

			setBestMatchingNeurons(new java.util.HashMap<Integer, Double>());
			int sectionLength = neuronsCount/getGridDivideNumber();
			int start = 0;
			int end = sectionLength;
			for (int i = 0; i < getGridDivideNumber(); i++)
			{
				//ScheduleFindBestMatchingNeuron(start, end, dataVector);
				getDoneEvents()[i] = new ManualResetEvent(false);
				FindBestNeuronRequest request = new FindBestNeuronRequest(start, end, dataVector, getDoneEvents()[i]);
				ThreadPool.QueueUserWorkItem(AsynchronousFindBestMatchingNeuron, request);

				start = end;
				end += sectionLength;
				if (i == getGridDivideNumber() - 1)
				{
					end = neuronsCount;
				}
			}

			//joining threads
			WaitHandle.WaitAll(getDoneEvents());


			for (java.util.Map.Entry<Integer, Double> matchingNeuron : getBestMatchingNeurons().entrySet())
			{
				double distance = matchingNeuron.getValue();
				if (distance < minDistance)
				{
					minDistance = distance;
					result = matchingNeuron.getKey();
				}
			}

			return result;
		}

		@Override
		public void AccommodateNetworkWeights(int bestNeuronNum, double[] dataVector, int iteration)
		{
			double radius = getRadiusProvider().GetRadius(iteration);
			java.util.HashMap<Integer, Double> effectedNeurons = getTopology().GetNeuronsInRadius(bestNeuronNum, radius);

			int effectedNeuronsCount = effectedNeurons.size();
			if (effectedNeuronsCount >= 10)
			{
				int sectionLength = effectedNeuronsCount / getGridDivideNumber();
				int start = 0;
				int end = sectionLength;
				for (int i = 0; i < getGridDivideNumber(); i++)
				{
					getDoneEvents()[i] = new ManualResetEvent(false);
					AccomodateNetworkRequest request = new AccomodateNetworkRequest(iteration, radius, start, end, effectedNeurons, dataVector, getDoneEvents()[i]);

					ThreadPool.QueueUserWorkItem(AsynchronousAccommodateNetworkWeights, request);

					start = end;
					end += sectionLength;
					if (i == getGridDivideNumber() - 1)
					{
						end = effectedNeuronsCount;
					}
				}

				//joining threads
				WaitHandle.WaitAll(getDoneEvents());
			}
			else
			{
				for (int effectedNeuron : effectedNeurons.keySet())
				{
					double distance = effectedNeurons.get(effectedNeuron);

					AccommodateNeuronWeights(effectedNeuron, dataVector, iteration, distance, radius);
				}

			}
		}

		protected void AsynchronousAccommodateNetworkWeights(Object request)
		{
			AccomodateNetworkRequest findRequest = (AccomodateNetworkRequest)request;
			int iteration = findRequest.getIteration();
			double radius = findRequest.getRadius();

			java.util.Iterator<java.util.Map.Entry<Integer, Double>> enumerator = findRequest.getEffectedNeurons().entrySet().iterator();
			int counter = 0;
			while (enumerator.hasNext())
			{
				if(counter < findRequest.getStart())
				{
					++counter;
					continue;
				}
				if(counter >= findRequest.getEnd())
				{
					break;
				}

				AccommodateNeuronWeights(enumerator.next().getKey(), findRequest.getDataVector(), iteration, enumerator.next().getValue(), radius);

				++counter;
			}

			findRequest.getDoneEvent().Set();
		}

		private void ScheduleFindBestMatchingNeuron(int start, int end, double[] dataVector)
		{
			int result = -1;
			double minDistance = Double.MAX_VALUE;

			for (int i = start; i < end; i++)
			{
				double distance = getMetricFunction().GetDistance(getNetwork().getNeurons().get(i).getWeights(), dataVector);
				if (distance < minDistance)
				{
					minDistance = distance;
					result = i;
				}
			}
			Monitor.Enter(this);
			getBestMatchingNeurons().put(result, minDistance);
			Monitor.Exit(this);
		}

		private void AsynchronousFindBestMatchingNeuron(Object request)
		{
			FindBestNeuronRequest findRequest = (FindBestNeuronRequest)request;
			this.ScheduleFindBestMatchingNeuron(findRequest.getStart(), findRequest.getEnd(), findRequest.getDataVector());
			findRequest.getDoneEvent().Set();
		}


		private static class AccomodateNetworkRequest
		{
			private int privateIteration;
			public final int getIteration()
			{
				return privateIteration;
			}
			private void setIteration(int value)
			{
				privateIteration = value;
			}
			private double privateRadius;
			public final double getRadius()
			{
				return privateRadius;
			}
			private void setRadius(double value)
			{
				privateRadius = value;
			}
			private int privateStart;
			public final int getStart()
			{
				return privateStart;
			}
			private void setStart(int value)
			{
				privateStart = value;
			}
			private int privateEnd;
			public final int getEnd()
			{
				return privateEnd;
			}
			private void setEnd(int value)
			{
				privateEnd = value;
			}
			private java.util.HashMap<Integer, Double> privateEffectedNeurons;
			public final java.util.HashMap<Integer, Double> getEffectedNeurons()
			{
				return privateEffectedNeurons;
			}
			private void setEffectedNeurons(java.util.HashMap<Integer, Double> value)
			{
				privateEffectedNeurons = value;
			}
			private double[] privateDataVector;
			public final double[] getDataVector()
			{
				return privateDataVector;
			}
			private void setDataVector(double[] value)
			{
				privateDataVector = value;
			}
			private ManualResetEvent privateDoneEvent;
			public final ManualResetEvent getDoneEvent()
			{
				return privateDoneEvent;
			}
			private void setDoneEvent(ManualResetEvent value)
			{
				privateDoneEvent = value;
			}

			public AccomodateNetworkRequest(int iteration, double radius, int start, int end, java.util.HashMap<Integer, Double> effectedNeurons, double[] dataVector, ManualResetEvent doneEvent)
			{
				setIteration(iteration);
				setRadius(radius);
				setStart(start);
				setEnd(end);
				setEffectedNeurons(effectedNeurons);
				setDataVector(dataVector);
				setDoneEvent(doneEvent);
			}
		}

		private static class FindBestNeuronRequest
		{
			private int privateStart;
			public final int getStart()
			{
				return privateStart;
			}
			private void setStart(int value)
			{
				privateStart = value;
			}
			private int privateEnd;
			public final int getEnd()
			{
				return privateEnd;
			}
			private void setEnd(int value)
			{
				privateEnd = value;
			}
			private double[] privateDataVector;
			public final double[] getDataVector()
			{
				return privateDataVector;
			}
			private void setDataVector(double[] value)
			{
				privateDataVector = value;
			}
			final CyclicBarrier barrier;
			public final ManualResetEvent getDoneEvent()
			{
				return privateDoneEvent;
			}
			private void setDoneEvent(ManualResetEvent value)
			{
				privateDoneEvent = value;
			}
			public FindBestNeuronRequest(int start, int end, double[] dataVector, ManualResetEvent doneEvent)
			{
				setStart(start);
				setEnd(end);
				setDataVector(dataVector);
				setDoneEvent(doneEvent);
			}
		}

	}