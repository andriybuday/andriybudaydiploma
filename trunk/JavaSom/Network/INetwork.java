package Network;

import Topology.*;

	public interface INetwork
	{
		java.util.List<INeuron> getNeurons();
		void setNeurons(java.util.List<INeuron> value);

		ITopology getTopology();
		void setTopology(ITopology value);
	}