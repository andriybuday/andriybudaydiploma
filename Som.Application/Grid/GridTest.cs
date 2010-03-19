using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Som.Application.Grid
{
    public partial class GridTest : Form
    {
        private BufferedControl BufferedControl;
        public GridController Controller { get; private set; }

        public GridConnDrawer GridConnDrawer { get; private set; }

        public GridTest(GridController controller)
        {
            InitializeComponent();
            GridConnDrawer = new GridConnDrawer();
            Controller = controller;
            Random = new Random();

            BufferedControl = bufferedControlGrid;
        }

        private void GridTest_Load(object sender, EventArgs e)
        {

        }

        public Random Random { get; private set; }

        private void buttonInitialize_Click(object sender, EventArgs e)
        {
            //var scale = pictureBoxGridTest.Width < pictureBoxGridTest.Height ? pictureBoxGridTest.Width : pictureBoxGridTest.Height;

            //GridConnDrawer.Draw(pictureBoxGridTest.CreateGraphics(), scale, points);

            Controller.InitializeSom();
        }

        public void DrawPoints(List<List<double>> points)
        {
            var scale = BufferedControl.Width < BufferedControl.Height ? BufferedControl.Width : BufferedControl.Height;

            GridConnDrawer.Draw(BufferedControl.CreateGraphics(), scale, points);
            BufferedControl.Dirty = true;
        }

        private void buttonLearn_Click(object sender, EventArgs e)
        {
            Controller.Learn();
        }

        public void UpdateUI()
        {
            labelIteration.Text = Controller.Iteration.ToString();
        }
    }
}
