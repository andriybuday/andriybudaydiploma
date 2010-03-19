using System.Collections.Generic;

namespace Som.Data.Suffle
{
    public class NotSufflingProvider : ISuffleProvider
    {
        public IList<int> Suffle(IList<int> input)
        {
            return input;
        }
    }
}