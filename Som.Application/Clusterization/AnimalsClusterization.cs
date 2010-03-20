using System;
using System.Drawing;
using System.Windows.Forms;

namespace Som.Application.Clusterization
{
    public partial class AnimalsClusterization : Form
    {
        public AnimalsClusterization(AnimalsClusterizationController controller)
        {
            InitializeComponent();
            Controller = controller;
        }

        private void btnLoadData_Click(object sender, System.EventArgs e)
        {
            //var openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    Controller.LoadData(openFileDialog.FileName);
            //}
            Controller.LoadData(@"D:\Education\Diploma\SomParallelization\Som.Application\Clusterization\Zoo.data");    
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
                    
                    var name = Controller.GetBestName(networkNeurons[nInd]);

                    _dataGVMap[j, i].Value = name;
                    _dataGVMap[j, i].Style.BackColor = GetColor(name);

                    nInd++;
                }
            }
            _dataGVMap.Update();
        }

        private Color GetColor(string name)
        {
            switch (name)
            {
                case ("Cow") : return Color.Green;
                case ("Hourse") : return Color.Green;
                case ("Zebra"): return Color.Green;
                case ("Pigeon"): return Color.Blue;
                case ("Chicken"): return Color.Blue;
                case ("Duck"): return Color.Blue;
                case ("Goose"): return Color.Blue;
                case ("Owl"): return Color.Blue;
                case ("Hawk"): return Color.Blue;
                case ("Eagle"): return Color.Blue;
                case ("Fox"): return Color.Red;
                case ("Dog"): return Color.Red;
                case ("Wolf"): return Color.Red;
                case ("Cat"): return Color.Red;
                case ("Tiger"): return Color.Red;
                case ("Lion"): return Color.Red;
                default:
                    return Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnLoadData_Click(sender, e);
            btnLearn_Click(sender, e);
            btnDrawMap_Click(sender, e);
        }
    }
}


