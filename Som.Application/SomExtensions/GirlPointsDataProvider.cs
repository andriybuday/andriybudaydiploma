using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Som.Data;

namespace Som.Application.SomExtensions
{
    public class GirlPointsDataProvider : ILearningDataProvider
    {
        public int D { get; private set; }
        private int _vectorsCountToReturn;

        public Random Random { get; private set; }
        public Image GirlImage { get; set; }

        public GirlPointsDataProvider(int vectorsCountToReturn)
        {
            D = 2;
            _vectorsCountToReturn = vectorsCountToReturn;
            Random = new Random();
        }

        public int LearningVectorsCount
        {
            get { return _vectorsCountToReturn; }
        }

        public int DataVectorDimention
        {
            get { return D; }
        }

        public double[] GetLearingDataVector(int vectorIndex)
        {
            var result = new double[] {Random.NextDouble(), Random.NextDouble()};

            if(GirlImage != null)
            {
                while (!GirlHoldsPoint(result))
                {
                    result = new double[] { Random.NextDouble(), Random.NextDouble() };
                }
            }

            return result;
        }

        private bool GirlHoldsPoint(double[] point)
        {
            var x = (int)(point[0]*GirlImage.Width);
            var y = (int)(point[1] * GirlImage.Height);

            var color = ((Bitmap) GirlImage).GetPixel(x, y);

            if (color.G > 100 && color.B < 100 && color.R < 100)
                return true;

            return false;
        }
    }

}
