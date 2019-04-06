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
using System.Windows.Shapes;

namespace MR_Performance_Visualization
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            ChangeMainContent(index);
        }

        private void MoveCursorMenu(int index)
        {
            MenuActiveItemIndicator.OnApplyTemplate();
            ActiveIndicator.Margin = new Thickness(0, (50 + (50 * index)), 0, 0);
        }

        private void ChangeMainContent(int index)
        {
            switch (index)
            {
                case 0:
                    MainContentGrid.Children.Clear();
                    MainContentGrid.Children.Add(new SettingColorsUserControl());
                    break;
                case 1:
                    MainContentGrid.Children.Clear();
                    MainContentGrid.Children.Add(new SettingFontsUserControl());
                    break;
                case 2:
                    MainContentGrid.Children.Clear();
                    MainContentGrid.Children.Add(new SettingThemesUserControl());
                    break;
                default:
                    break;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MenuController.Close_Button_Click(this);
        }
    }
}
