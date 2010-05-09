using System;
using System.Windows.Forms;
using Som.Application.Clusterization;
using Som.Application.Grid;

namespace Som.Application
{
    class Program
    {
        [STAThread]
        [MTAThread]
        static void Main(string[] args)
        {

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new SomMain());
        }
    }
}
