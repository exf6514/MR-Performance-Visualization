﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            Properties.Settings.Default.Side_Bar_Color = "#FF3E3E42";
            Properties.Settings.Default.Menu_Color = "#FF686868";
            Properties.Settings.Default.Main_Content_Color = "#FF1E1E1E";
            Properties.Settings.Default.Main_Text_Color = "White";
            Properties.Settings.Default.Menu_Icon_Color = "LightGray";

            Properties.Settings.Default.Save();
        }

        private void applyLightTheme()
        {
            Application.Current.Resources["Side_Bar_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF222222"));
            Application.Current.Resources["Menu_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEEEEEE"));
            Application.Current.Resources["Main_Content_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            Application.Current.Resources["Main_Text_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            Application.Current.Resources["Menu_Icon_Color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));

            Properties.Settings.Default.Side_Bar_Color = "#FF222222";
            Properties.Settings.Default.Menu_Color = "#FFEEEEEE";
            Properties.Settings.Default.Main_Content_Color = "White";
            Properties.Settings.Default.Main_Text_Color = "Black";
            Properties.Settings.Default.Menu_Icon_Color = "Gray";

            Properties.Settings.Default.Save();
        }
    }
}
