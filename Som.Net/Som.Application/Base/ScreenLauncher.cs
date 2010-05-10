using System;
using System.Windows.Forms;

namespace Som.Application.Base
{
    public class ScreenLauncher
    {
        public static void LaunchScreen<T>() where T : IScreenController
        {
            try
            {
                var screenController = (IScreenController)Activator.CreateInstance<T>();
                screenController.ShowScreen();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}