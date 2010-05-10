using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Som.Network;
using Som.Topology;

namespace Som.Application.Grid
{
    public class GridConnDrawer: IBufferedControlController
    {
        public Brush DotBrush { get; private set; }
        public Pen LinePen { get; private set; }
        public int DotSize { get; private set; }
        public int HalfDotSize { get; private set; }

        public ITopology Topology { get; private set; }
        public Image BackGroundImage { get; set; }
        public GridConnDrawer()
        {
            DotBrush = Brushes.Red;
            DotSize = 4;
            HalfDotSize = DotSize/2;
            LinePen = new Pen(Color.Black, DotSize-2 > 1 ? DotSize-2 : 1);
        }

        public void Draw(Graphics graphics, int width, int height)
        {
            if (LastRunNeurons == null) return;

            int scaling = width < height ? width : height;

          
            this.Draw(graphics, scaling, LastRunNeurons);
        }

        public IList<INeuron> LastRunNeurons { get; set; }

        public void Draw(Graphics g, int scalingSize, IList<INeuron> neurons)
        {
            LastRunNeurons = neurons;
            var points = neurons.Select(x => x.Weights.ToList()).ToList();

            InitializeTopology(neurons.Count);

            ClearDrawingSurface(g);

            if(scalingSize < 10 || scalingSize > 2000) throw new ArgumentException("scalingSize for drawing should be in (10;2000)");
            foreach (List<double> point in points)
            {
                if (point.Count != 2)
                    throw new ArgumentException("Some input points are not of correct dimention - should be 2");
                if(point[0] > 1.0 || point[0] < 0.0 || point[1] > 1.0 || point[1] < 0.0)
                    throw new ArgumentException("All points should be in range [0.0; 1.0]");
            }
            
            // this is O(N^2), but come on.. this is just drawing...            

            for (int i = 0; i < points.Count; i++)
            {
                var neiboPoints = Topology.GetDirectlyConnectedNeurons(i);

                foreach (var neiboPointInd in neiboPoints)
                {
                    DrawConnectionLine(g, points[i], points[neiboPointInd], scalingSize);    
                }
            }

            foreach (List<double> point in points)
            {
               DrawDot(g, DotBrush, point, scalingSize);
            }
        }

        private void ClearDrawingSurface(Graphics g)
        {
            g.Clear(Color.White);

            if (BackGroundImage != null)
            {
                g.DrawImage(BackGroundImage, 0, 0);
            }
        }

        private void InitializeTopology(int count)
        {
            var n = (int)Math.Sqrt(count);
            Topology = new SimpleMatrixTopology(n, n);
        }


        private double[,] FillInCacheMatrix(List<List<double>> points)
        {
            var n = points.Count;
            var result = new double[n,n];

            for (int i = 0; i < n; i++)
            {
                int jEnd = i;
                for (int j = 0; j < jEnd; j++)
                {
                    result[i, j] = result[j, i] = GetDistance(points[i], points[j]);
                }
            }

            return result;
        }

        private double GetDistance(List<double> p1, List<double> p2)
        {
            double xD = p1[0] - p2[0];
            double yD = p1[1] - p2[1];
            return Math.Sqrt(xD*xD + yD*yD);
        }

        private List<int> FindFourNeighborhoodPoints(int pointInd, double[,] distances)
        {
            var four = 4;
            var n = (int)Math.Sqrt(distances.Length);

            //suppose they are sorderd by increasing distance...
            var result = new List<int>() {0, 1, 2, 3};

            for (int i = 0; i < n; i++)
            {
                var dist = distances[pointInd, i];

                // determine if found one of the four best matching and if we need to move result
                bool shouldMove = false;
                int moveInd = 0;
                for (int j = four-1; j >= 0; j--)
                {
                    var oneOfTheBestInd = result[j];
                    if(dist < distances[pointInd, oneOfTheBestInd])
                    {
                        shouldMove = true;
                        moveInd = j;
                    }
                }

                // moving result
                if(shouldMove)
                {
                    var currentToWrite = i;
                    var currentToBlowUp = 0;
                    for (int j = moveInd; j < four; j++)
                    {
                        currentToBlowUp = result[j];
                        result[j] = currentToWrite;
                        currentToWrite = currentToBlowUp;
                    }
                }

            }

            return result;
        }

        private void DrawDot(Graphics graphics, Brush brush, List<double> point, int scalingSize)
        {
            float x1 = Scale(point[0], scalingSize);
            float y1 = Scale(point[1], scalingSize);

            graphics.FillEllipse(brush, x1 - HalfDotSize, y1 - HalfDotSize, DotSize, DotSize);
        }

        private void DrawConnectionLine(Graphics graphics, List<double> point, List<double> neiboPoint, int scalingSize)
        {
            float x1 = Scale(point[0], scalingSize);
            float y1 = Scale(point[1], scalingSize);
            float x2 = Scale(neiboPoint[0], scalingSize);
            float y2 = Scale(neiboPoint[1], scalingSize);

            graphics.DrawLine(LinePen, x1, y1, x2, y2);
        }

        private float Scale(double coord, int scalingSize)
        {
            return (float) (coord*scalingSize);
        }

    }
}
