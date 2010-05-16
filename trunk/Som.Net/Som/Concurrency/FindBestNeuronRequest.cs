using System.Threading;

namespace Som.Concurrency
{
    public struct FindBestNeuronRequest
    {
        public int Start;
        public int End;
        public AutoResetEvent DoneEvent;
    }
}