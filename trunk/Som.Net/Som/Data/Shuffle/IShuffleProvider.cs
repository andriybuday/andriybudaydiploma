using System.Collections.Generic;

namespace Som.Data.Shuffle
{
    public interface IShuffleProvider
    {
        IList<int> Suffle(IList<int> input);
    }
}