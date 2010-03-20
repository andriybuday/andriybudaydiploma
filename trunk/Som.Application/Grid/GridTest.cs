using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Som.Application.SomExtensions;
using Som.Network;

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
            var image = Image.FromFile(@"Pictures\girl.bmp");
            GridConnDrawer = new GridConnDrawer();
            //GridConnDrawer.BackGroundImage = image;
            Controller = controller;
            Random = new Random();

            BufferedControl = bufferedControlGrid;
            BufferedControl.SetController(GridConnDrawer);
        }

        private void GridTest_Load(object sender, EventArgs e)
        {

        }

        public Random Random { get; private set; }

        private void buttonInitialize_Click(object sender, EventArgs e)
        {
            //var scale = pictureBoxGridTest.Width < pictureBoxGridTest.Height ? pictureBoxGridTest.Width : pictureBoxGridTest.Height;

            //GridConnDrawer.Draw(pictureBoxGridTest.CreateGraphics(), scale, points);
            var learningProcessorBase = somFactoryUI.GetSomProcessor();

            Controller.InitializeSom((ControllableWtmLearningProcessor)learningProcessorBase);
        }

        public void DrawNeuroNet(IList<INeuron> neurons)
        {
            var scale = BufferedControl.Width < BufferedControl.Height ? BufferedControl.Width : BufferedControl.Height;

            GridConnDrawer.LastRunNeurons = neurons;
            BufferedControl.Dirty = true;

            BufferedControl.Invalidate();
        }

        private void buttonLearn_Click(object sender, EventArgs e)
        {
            Controller.Learn();
        }

        public void UpdateUI()
        {
            BufferedControl.Dirty = true;
            labelIteration.Text = Controller.Iteration.ToString();

         //   DrawPoints(Controller.LearningProcessor.)
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            Controller.Next((int)numericUpDownIterationsPerOnce.Value);
        }
    }
}
