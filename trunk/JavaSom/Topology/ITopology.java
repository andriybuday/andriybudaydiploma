package Topology;

	public interface ITopology
	{
		int getNeuronsCount();
		int getRowCount();
		int getColCount();
		double getWholeTopologyRadius();
		java.util.List<Integer> GetDirectlyConnectedNeurons(int neuronNumber);
		java.util.HashMap<Integer, Double> GetNeuronsInRadius(int neuronNumber, double radius);
		boolean Overlaps(int neuronA, int neuronB, double radius);
	}