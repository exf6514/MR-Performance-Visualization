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
    /// <summary>
    /// Interaction logic for SettingColorsUserControl.xaml
    /// </summary>
    public partial class SettingColorsUserControl : UserControl
    {
        Color choosenColor = new Color();
        Color currentColor = new Color();

        public SettingColorsUserControl()
        {
            InitializeComponent();

            currentColor = (Color)ColorConverter.ConvertFromString(Application.Current.Resources["Main_App_Color"].ToString());

            changeRGBValues(currentColor);
            changeSelectedColorView(currentColor);
        }

        private void Recommended_Color_Click(object sender, RoutedEventArgs e)
        {
            Button buttonClicked = sender as Button;
            changeSelectedColorView(((SolidColorBrush)buttonClicked.Background).Color);
            changeRGBValues(((SolidColorBrush)buttonClicked.Background).Color);
        }
        private void changeSelectedColorView(Color color)
        {
            Brush brush = new SolidColorBrush(color);
            choosenColor = color;
            selectedColorView.Background = brush;
        }
        private void changeRGBValues(Color color)
        {
            RSlider.Value = (int)color.R;
            GSlider.Value = (int)color.G;
            BSlider.Value = (int)color.B;

            RValue.Text = ((int)color.R).ToString();
            GValue.Text = ((int)color.G).ToString();
            BValue.Text = ((int)color.B).ToString();

            HEXValue.Text = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        private Color getColorFromRGBInputs()
        {
            Color rgbColor = new Color();
            rgbColor = Color.FromRgb((byte)RSlider.Value, (byte)GSlider.Value, (byte)BSlider.Value);
            return rgbColor;
        }

        private void RSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RValue.Text = RSlider.Value.ToString();
            Color rgbColor = getColorFromRGBInputs();
            changeRGBValues(rgbColor);
            changeSelectedColorView(rgbColor);
        }

        private void GSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GValue.Text = GSlider.Value.ToString();
            Color rgbColor = getColorFromRGBInputs();
            changeRGBValues(rgbColor);
            changeSelectedColorView(rgbColor);
        }

        private void BSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BValue.Text = BSlider.Value.ToString();
            Color rgbColor = getColorFromRGBInputs();
            changeRGBValues(rgbColor);
            changeSelectedColorView(rgbColor);
        }

        private void ApplyColor_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["Main_App_Color"] = new SolidColorBrush(choosenColor);
            Properties.Settings.Default.Main_App_Color = "#" + choosenColor.R.ToString("X2") + choosenColor.G.ToString("X2") + choosenColor.B.ToString("X2");
            Properties.Settings.Default.Save();
        }
    }
}
