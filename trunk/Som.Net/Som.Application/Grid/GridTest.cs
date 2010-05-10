using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            GridConnDrawer = new GridConnDrawer();
            BufferedControl = bufferedControlGrid;
            BufferedControl.SetController(GridConnDrawer);
            LoadSiluete();
            Controller = controller;
            Random = new Random();
        }

        private void GridTest_Load(object sender, EventArgs e)
        {

        }

        public Random Random { get; private set; }

        private void buttonInitialize_Click(object sender, EventArgs e)
        {
            //1l0O
            //var scale = pictureBoxGridTest.Width < pictureBoxGridTest.Height ? pictureBoxGridTest.Width : pictureBoxGridTest.Height;

            //GridConnDrawer.Draw(pictureBoxGridTest.CreateGraphics(), scale, points);
            var learningProcessorBase = somFactoryUI.GetSomProcessor();

            Controller.InitializeSom(learningProcessorBase, somFactoryUI.LearningDataProvider);
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
            labelPrevTime.Text = labelTime.Text;
            labelTime.Text = string.Empty;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Controller.Learn();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            var passedTime = string.Format("{0}H:{1}M:{2}S:{3}ms", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            labelTime.Text = passedTime;
        }

        public void UpdateUI()
        {
            BufferedControl.Dirty = true;
            labelIteration.Text = Controller.Iteration.ToString();

         //   DrawPoints(Controller.somLearningProcessor.)
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            Controller.Next((int)numericUpDownIterationsPerOnce.Value);
        }

        private void checkBoxShowGirl_CheckedChanged(object sender, EventArgs e)
        {
            LoadSiluete();
        }

        private void LoadSiluete()
        {
            if(checkBoxShowGirl.Checked)
            {
                var image = Image.FromFile(@"Pictures\girl.bmp");
                GridConnDrawer.BackGroundImage = image;
            }
            else
            {
                GridConnDrawer.BackGroundImage = null;
            }
            BufferedControl.Dirty = true;
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            timer1.Interval = 42;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Controller.Next((int)numericUpDownIterationsPerOnce.Value);
            if(Controller.Iteration >= Controller.GetMaxIterations())
            {
                timer1.Stop();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
