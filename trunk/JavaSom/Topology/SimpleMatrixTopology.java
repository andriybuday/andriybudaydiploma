package Topology;

	public class SimpleMatrixTopology implements ITopology
	{
		private int privateRowCount;
		public final int getRowCount()
		{
			return privateRowCount;
		}
		private void setRowCount(int value)
		{
			privateRowCount = value;
		}
		private int privateColCount;
		public final int getColCount()
		{
			return privateColCount;
		}
		private void setColCount(int value)
		{
			privateColCount = value;
		}
		private double privateWholeTopologyRadius;
		public final double getWholeTopologyRadius()
		{
			return privateWholeTopologyRadius;
		}
		private void setWholeTopologyRadius(double value)
		{
			privateWholeTopologyRadius = value;
		}

		public SimpleMatrixTopology(int rowCount, int colCount)
		{
			setColCount(colCount);
			setRowCount(rowCount);

			setWholeTopologyRadius(Math.max(getColCount(), getRowCount()) / 2.0);
		}

		public final int getNeuronsCount()
		{
			return getRowCount() * getColCount();
		}

		public final java.util.List<Integer> GetDirectlyConnectedNeurons(int neuronNumber)
		{
			java.util.ArrayList<Integer> result = new java.util.ArrayList<Integer>();
			int upper = neuronNumber - getColCount();
			int down = neuronNumber + getColCount();
			int right = neuronNumber + 1;
			int left = neuronNumber - 1;

			if (upper >= 0)
			{
				result.add(upper);
			}
			if (down < getNeuronsCount())
			{
				result.add(down);
			}
			if (right % getColCount() != 0)
			{
				result.add(right);
			}
			if ((left + 1) % getColCount() != 0)
			{
				result.add(left);
			}

			return result;
		}

		public final java.util.HashMap<Integer, Double> GetNeuronsInRadius(int neuronNumber, double radius)
		{
			//return Slow_GetNeuronsInRadius(neuronNumber, radius);
			return Fast_SquarNeibo(neuronNumber, radius);

		}

		public final boolean Overlaps(int neuronA, int neuronB, double radius)
		{
			int nColPosA = neuronA % getColCount();
			int nRowPosA = neuronA / getColCount();
			int nColPosB = neuronB % getColCount();
			int nRowPosB = neuronB / getColCount();
			int colDiff = Math.abs(nColPosA - nColPosB);
			int rowDiff = Math.abs(nRowPosA - nRowPosB);
			return (colDiff*colDiff + rowDiff*rowDiff) < (radius*radius);
		}

		public final java.util.HashMap<Integer, Double> Fast_SquarNeibo(int neuronNumber, double radius)
		{
			java.util.HashMap<Integer, Double> result = new java.util.HashMap<Integer, Double>();

			int nColPos = neuronNumber % getColCount();
			int nRowPos = neuronNumber / getColCount();

			int r = (int)Math.floor(radius + 1);

			//runs through rows
			int higherLevel = nColPos - r;
			if(higherLevel < 0)
			{
				higherLevel = 0;
			}
			int lowerLevel = higherLevel + 2*r;
			if (lowerLevel > getRowCount())
			{
				lowerLevel = getRowCount();
			}

			//runs through columns
			int left = nRowPos - r;
			if (left < 0)
			{
				left = 0;
			}
			int right = left + 2*r;
			if (right > getColCount())
			{
				right = getColCount();
			}


			for (int col = higherLevel; col < lowerLevel; col++)
			{
				for (int row = left; row < right; row++)
				{
					double currDist = GetDistance(col, row, nColPos, nRowPos);
					if (currDist < radius)
					{
						result.put(row * getColCount() + col, currDist);
					}
				}
			}

			return result;
		}

		public final java.util.HashMap<Integer, Double> Slow_GetNeuronsInRadius(int neuronNumber, double radius)
		{
			int neuronsCount = getRowCount() * getColCount();

			int neuronColPos = neuronNumber % getColCount();
			int neuronRowPos = neuronNumber / getColCount();

			java.util.HashMap<Integer, Double> result = new java.util.HashMap<Integer, Double>();

			for (int col = 0; col < getColCount(); col++)
			{
				for (int row = 0; row < getRowCount(); row++)
				{
					double currDist = GetDistance(col, row, neuronColPos, neuronRowPos);
					if (currDist < radius)
					{
						result.put(row * getColCount() + col, currDist);
					}
				}
			}

			return result;
		}


		private double GetDistance(int col, int row, int neuronColPos, int neuronRowPos)
		{
			int xD = (neuronColPos - col);
			int yD = (neuronRowPos - row);
			return Math.sqrt(xD * xD + yD * yD);
		}
	}