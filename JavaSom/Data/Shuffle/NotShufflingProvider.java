package Data.Shuffle;

	public class NotShufflingProvider implements IShuffleProvider
	{
		public final java.util.List<Integer> Suffle(java.util.List<Integer> input)
		{
			return input;
		}
	}