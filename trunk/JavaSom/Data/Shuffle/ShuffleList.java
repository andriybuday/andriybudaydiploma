package Data.Shuffle;

	public class ShuffleList extends java.util.ArrayList<Integer>
	{
		private java.util.Random random = new java.util.Random();
		private int privateMaxCount;
		private int getMaxCount()
		{
			return privateMaxCount;
		}
		private void setMaxCount(int value)
		{
			privateMaxCount = value;
		}

		public ShuffleList(int vectorsCount)
		{
			super();
			for (int i = 0; i < vectorsCount; i++)
			{
				add(i);
			}
			setMaxCount(vectorsCount);
		}

		public final void Suffle()
		{
			for (int i = 0; i < getMaxCount(); i++)
			{
				int next = random.nextInt(getMaxCount());
				int a = super.get(next);
				super.set(next, super.get(i));
				super.set(i, a);
			}
		}

	}