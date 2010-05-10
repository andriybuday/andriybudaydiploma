package Topology;

	public class DefaultRadiusProvider implements IRadiusProvider
	{
		private int privateMaxIterations;
		public final int getMaxIterations()
		{
			return privateMaxIterations;
		}
		private void setMaxIterations(int value)
		{
			privateMaxIterations = value;
		}
		private double privateTopologyRadius;
		public final double getTopologyRadius()
		{
			return privateTopologyRadius;
		}
		private void setTopologyRadius(double value)
		{
			privateTopologyRadius = value;
		}
		private double privateTimeConstant;
		public final double getTimeConstant()
		{
			return privateTimeConstant;
		}
		private void setTimeConstant(double value)
		{
			privateTimeConstant = value;
		}

		public DefaultRadiusProvider(int maxIterations, double startRadius)
		{
			setMaxIterations(maxIterations);
			setTopologyRadius(startRadius);

			setTimeConstant(maxIterations / Math.log(startRadius));
		}

		public final double GetRadius(int iteration)
		{
			return getTopologyRadius()*Math.exp(-iteration/getTimeConstant());
		}
	}