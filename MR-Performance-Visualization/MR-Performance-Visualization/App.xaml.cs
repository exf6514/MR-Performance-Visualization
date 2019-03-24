using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MR_Performance_Visualization
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            initializeSettings();
        }

        public void initializeSettings()
        {
            Application.Current.Resources["Main_App_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(MR_Performance_Visualization.Properties.Settings.Default.Main_App_Color));
            Application.Current.Resources["Main_App_Font_Family"] = new FontFamily(MR_Performance_Visualization.Properties.Settings.Default.Main_App_Font_Family);
            Application.Current.Resources["Main_App_Font_Size_Big"] = MR_Performance_Visualization.Properties.Settings.Default.Main_App_Font_Size_Big;
            Application.Current.Resources["Main_App_Font_Size_Biggest"] = MR_Performance_Visualization.Properties.Settings.Default.Main_App_Font_Size_Biggest;
            Application.Current.Resources["Main_App_Font_Size_Normal"] = MR_Performance_Visualization.Properties.Settings.Default.Main_App_Font_Size_Normal;
            Application.Current.Resources["Main_App_Font_Size_Small"] = MR_Performance_Visualization.Properties.Settings.Default.Main_App_Font_Size_Small;
            Application.Current.Resources["Main_App_Font_Size_Smallest"] = MR_Performance_Visualization.Properties.Settings.Default.Main_App_Font_Size_Smallest;
            Application.Current.Resources["Main_Content_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(MR_Performance_Visualization.Properties.Settings.Default.Main_Content_Color));
            Application.Current.Resources["Main_Text_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(MR_Performance_Visualization.Properties.Settings.Default.Main_Text_Color));
            Application.Current.Resources["Menu_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(MR_Performance_Visualization.Properties.Settings.Default.Menu_Color));
            Application.Current.Resources["Menu_Icon_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(MR_Performance_Visualization.Properties.Settings.Default.Menu_Icon_Color));
        }
    }
}
