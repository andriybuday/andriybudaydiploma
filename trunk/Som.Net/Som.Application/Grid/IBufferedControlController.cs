using System.Drawing;

namespace Som.Application.Grid
{
    public interface IBufferedControlController
    {
        void Draw(Graphics graphics, int width, int height);
    }
}