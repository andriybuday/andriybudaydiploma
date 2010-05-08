using System;
using System.Collections.Generic;

namespace Som.Data.Suffle
{
    public class ShuffleProvider : IShuffleProvider
    {
        private Random random = new Random();

        public IList<int> Suffle(IList<int> input)
        {
            var count = input.Count;
            for (int i = 0; i < count; i++)
            {
                int next = random.Next(count);
                int a = input[next];
                input[next] = input[i];
                input[i] = a;
            }
            return input;
        }
    }
}