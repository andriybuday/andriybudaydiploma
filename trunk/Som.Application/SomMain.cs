using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Som.Application.Base;
using Som.Application.Clusterization;
using Som.Application.Grid;

namespace Som.Application
{
    public partial class SomMain : Form
    {
        public SomMain()
        {
            //Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            InitializeComponent();
        }

        private void buttonGridTest_Click(object sender, EventArgs e)
        {
            ScreenLauncher.LaunchScreen<GridController>();
        }

        private void SomMain_Shown(object sender, EventArgs e)
        {
            //ScreenLauncher.LaunchScreen<GridController>();
        }

        private void buttonAnimalsClusterization_Click(object sender, EventArgs e)
        {
            ScreenLauncher.LaunchScreen<AnimalsClusterizationController>();
        }
    }
}
