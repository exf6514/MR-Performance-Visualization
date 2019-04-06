using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MR_Performance_Visualization
{
    class MenuController
    {
        Utils utils = new Utils();

        public static void Power_Button_Click()
        {
            Application.Current.Shutdown();
        }

        public void Settings_Button_Click(Window window)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            utils.ApplyEffect(window);
            settingsWindow.ShowDialog();
            utils.ClearEffect(window);
        }

        public void Help_Button_Click(Window window)
        {
            HelpWindow helpWindow = new HelpWindow();
            utils.ApplyEffect(window);
            helpWindow.ShowDialog();
            utils.ClearEffect(window);
        }

        public static void Close_Button_Click(Window window)
        {
            window.Close();
        }
    }
}
