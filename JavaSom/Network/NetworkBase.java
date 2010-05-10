package Network;

import ActivationFunction.*;
import Topology.*;

	public class NetworkBase implements INetwork
	{
		public NetworkBase(boolean randomize, java.util.List<Double> maxWeights, IActivationFunction activationFunction, ITopology topology)
		{
			setTopology(topology);
			setNeurons(new java.util.ArrayList<INeuron>());
			for (int i = 0; i < topology.getNeuronsCount(); i++)
			{
				INeuron neuron = new NeuronBase(randomize, maxWeights, activationFunction);
				getNeurons().add(neuron);
			}
		}

		public NetworkBase(boolean randomize, java.util.ArrayList<Double> minWeights, java.util.ArrayList<Double> maxWeights, IActivationFunction activationFunction, ITopology topology)
		{
			setTopology(topology);
			setNeurons(new java.util.ArrayList<INeuron>());
			for (int i = 0; i < topology.getNeuronsCount(); i++)
			{
				INeuron neuron = new NeuronBase(randomize, minWeights, maxWeights, activationFunction);
				getNeurons().add(neuron);
			}
		}

		private java.util.List<INeuron> privateNeurons;
		public final java.util.List<INeuron> getNeurons()
		{
			return privateNeurons;
		}
		public final void setNeurons(java.util.List<INeuron> value)
		{
			privateNeurons = value;
		}

		private ITopology privateTopology;
		public final ITopology getTopology()
		{
			return privateTopology;
		}
		public final void setTopology(ITopology value)
		{
			privateTopology = value;
		}
	}