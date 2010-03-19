using System.Collections.Generic;

namespace Som.Data.Suffle
{
    public interface ISuffleProvider
    {
        IList<int> Suffle(IList<int> input);
    }
}