namespace Som.Topology
{
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Location()
            :this(0,0)
        {
        }

        private Location(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}