using System;
using System.Collections.Generic;

namespace Som.Data.Shuffle
{
    public class ShuffleList : List<int>
    {
        private Random random = new Random();
        private int MaxCount { get; set; }

        public ShuffleList(int vectorsCount)
            : base()
        {
            for (int i = 0; i < vectorsCount; i++) Add(i);
            MaxCount = vectorsCount;
        }

        public void Suffle()
        {
            for (int i = 0; i < MaxCount; i++)
            {
                int next = random.Next(MaxCount);
                int a = this[next];
                this[next] = this[i];
                this[i] = a;
            }
        }

    }
}