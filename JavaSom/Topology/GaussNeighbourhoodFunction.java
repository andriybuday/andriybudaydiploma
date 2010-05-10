package Topology;

	public class GaussNeighbourhoodFunction implements INeighbourhoodFunction
	{
		public final double GetDistanceFalloff(double distance, double radius)
		{
			double denominator = 2 * radius * radius;
			return Math.exp(- distance / denominator);
		}
	}