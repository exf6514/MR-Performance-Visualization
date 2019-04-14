using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

// Creates a namespace
namespace MR_Performance_Visualization
{
    /**
    * Creates a class called MenuController that manages buttons functions
    */
    class MenuController
    {
        // Instantiate Utils constructor
        Utils utils = new Utils();

        /**
        * Shutdowns the application
        */
        public static void Power_Button_Click()
        {
            Application.Current.Shutdown();
        }

        /**
        * Shows application settings
        */
        public void Settings_Button_Click(Window window)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            utils.ApplyEffect(window);
            settingsWindow.ShowDialog();
            utils.ClearEffect(window);
        }

        /**
        * Provides helpful information to users
        */
        public void Help_Button_Click(Window window)
        {
            HelpWindow helpWindow = new HelpWindow();
            utils.ApplyEffect(window);
            helpWindow.ShowDialog();
            utils.ClearEffect(window);
        }

        /**
        * Closes the application
        */
        public static void Close_Button_Click(Window window)
        {
            window.Close();
        }
    }
}
