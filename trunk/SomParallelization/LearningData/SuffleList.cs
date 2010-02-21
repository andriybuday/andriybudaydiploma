using System;
using System.Collections.Generic;

namespace SomParallelization.LearningData
{
    public class SuffleList : List<int>
    {
        private int MaxCount { get; set; }

        public SuffleList(int vectorsCount) :base()
        {
            for (int i = 0; i < vectorsCount; i++) Add(i);
        }

        public void Suffle()
        {
            var random = new Random();
            for (int i = 0; i < MaxCount; i++)
            {
                int next = random.Next(MaxCount);
                int a = this[next];
                this[next] = this[a];
                this[a] = a;    
            }
        }
    }
}