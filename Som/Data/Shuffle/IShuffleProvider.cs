using System.Collections.Generic;

namespace Som.Data.Suffle
{
    public interface IShuffleProvider
    {
        IList<int> Suffle(IList<int> input);
    }
}