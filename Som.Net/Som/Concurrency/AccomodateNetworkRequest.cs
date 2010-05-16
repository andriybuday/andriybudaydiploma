using System.Collections.Generic;
using System.Threading;

namespace Som.Concurrency
{
    public struct AccomodateNetworkRequest
    {
        public int Iteration;
        public double Radius;
        public int Start;
        public int End;
        public List<int> EffectedNeurons;
        public AutoResetEvent DoneEvent;
    }
}