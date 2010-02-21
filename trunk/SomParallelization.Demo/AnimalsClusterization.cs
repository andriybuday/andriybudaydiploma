using System;
using System.Windows.Forms;

namespace SomParallelization.Demo
{
    public partial class AnimalsClusterization : Form
    {
        public AnimalsClusterization()
        {
            InitializeComponent();
            Controller = new AnimalsClusterizationController();
        }

        private void btnLoadData_Click(object sender, System.EventArgs e)
        {
            //var openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    Controller.LoadData(openFileDialog.FileName);
            //}
            Controller.LoadData(@"D:\Education\Diploma\SomParallelization\SomParallelization.Demo\Zoo\Zoo.data");    
        }

        protected AnimalsClusterizationController Controller { get; set; }

        private void btnLearn_Click(object sender, EventArgs e)
        {
            Controller.Learn();
        }

        private void btnDrawMap_Click(object sender, EventArgs e)
        {
            var topology = Controller.GetTopology();
            var networkNeurons = Controller.GetNetworkNeurons();

            int rows = _dataGVMap.RowCount = topology.RowCount;
            int cols = _dataGVMap.ColumnCount = topology.ColCount;

            int nInd = 0;
            if(networkNeurons.Count != rows * cols) throw new ApplicationException("Something wrong with network setup.");

            for (int j = 0; j < cols; j++)
            {
                _dataGVMap.Columns[j].Width = 55;
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    
                    _dataGVMap[j, i].ToolTipText = networkNeurons[nInd].ToString();

                    _dataGVMap[j, i].Value = Controller.GetBestName(networkNeurons[nInd]);

                    nInd++;
                }
            }
            _dataGVMap.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnLoadData_Click(sender, e);
            btnLearn_Click(sender, e);
            btnDrawMap_Click(sender, e);
        }
    }
}


