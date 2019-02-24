using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MR_Performance_Visualization
{

    public partial class SettingThemesUserControl : UserControl
    {
        private String currentTheme = "";

        public SettingThemesUserControl()
        {
            InitializeComponent();
            currentTheme = "LIGHT";
            selectedThemeActive();
        }

        private void ApplyThemeChange_Click(object sender, RoutedEventArgs e)
        {
            currentThemeText.Text = currentTheme;
            changeTheme();
        }
        private void changeTheme()
        {
            if (currentTheme == "DARK")
            {
                applyDarkTheme();
            }
            else
            {
                applyLightTheme();
            }
        }

        private void selectedThemeActive()
        {
            if (currentTheme == "DARK")
            {
                DarkTheme.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Application.Current.Resources["Main_App_Color"].ToString()));
                LightTheme.BorderBrush = null;
            }
            else
            {
                LightTheme.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Application.Current.Resources["Main_App_Color"].ToString()));
                DarkTheme.BorderBrush = null;
            }
        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            currentTheme = "LIGHT";
            selectedThemeActive();
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            currentTheme = "DARK";
            selectedThemeActive();
        }

        private void applyDarkTheme()
        {
            Application.Current.Resources["Side_Bar_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3E3E42"));
            Application.Current.Resources["Menu_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF686868"));
            Application.Current.Resources["Main_Content_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1E1E1E"));
            Application.Current.Resources["Main_Text_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            Application.Current.Resources["Menu_Icon_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("LightGray"));
        }

        private void applyLightTheme()
        {
            Application.Current.Resources["Side_Bar_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF222222"));
            Application.Current.Resources["Menu_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEEEEEE"));
            Application.Current.Resources["Main_Content_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            Application.Current.Resources["Main_Text_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            Application.Current.Resources["Menu_Icon_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
        }
    }
}
