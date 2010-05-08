using System.Collections.Generic;

namespace Som.Data.Suffle
{
    public class NotShufflingProvider : IShuffleProvider
    {
        public IList<int> Suffle(IList<int> input)
        {
            return input;
        }
    }
}