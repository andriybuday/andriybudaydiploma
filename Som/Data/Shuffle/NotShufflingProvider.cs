using System.Collections.Generic;

namespace Som.Data.Shuffle
{
    public class NotShufflingProvider : IShuffleProvider
    {
        public IList<int> Suffle(IList<int> input)
        {
            return input;
        }
    }
}