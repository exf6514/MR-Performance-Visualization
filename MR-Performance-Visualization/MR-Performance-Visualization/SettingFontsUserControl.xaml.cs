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
    /// Interaction logic for SettingFontsUserControl.xaml
    /// </summary>
    public partial class SettingFontsUserControl : UserControl
    {
        private String currentFontFamily = "";
        private String currentFontSize = "";
        private String selectedFontFamily = "";
        private String selectedFontSize = "";

        public SettingFontsUserControl()
        {
            InitializeComponent();

            currentFontFamily = "Segoe UI";
            currentFontSize = "12px";

            foreach (System.Drawing.FontFamily font in System.Drawing.FontFamily.Families)
            {
                if(font.Name != ""){ fontFamilyComboBox.Items.Add(font.Name); }
            }

            fontSizeComboBox.Items.Add("8px");
            fontSizeComboBox.Items.Add("10px");
            fontSizeComboBox.Items.Add("12px");
            fontSizeComboBox.Items.Add("14px");

            fontSizeComboBox.SelectedValue = currentFontSize;
            fontFamilyComboBox.SelectedValue = currentFontFamily;
        }

        private void RecommendedFontsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecommendedFontsList.SelectedItems.Count > 0)
            {
                ListViewItem selecteditem = (ListViewItem)RecommendedFontsList.SelectedItem;

                selectedFontFamily = selecteditem.FontFamily.ToString();
                selectedFontSize = selecteditem.FontSize.ToString() + "px";

                changeSelectedFontSizePreview();
                changeSelectedFontFamilyPreview();

                fontSizeComboBox.SelectedValue = selectedFontSize;
                fontFamilyComboBox.SelectedValue = selectedFontFamily;
            }
        }

        private void changeSelectedFontSizePreview()
        {
            SelectedFontName.FontSize = int.Parse(selectedFontSize.Substring(0, selectedFontSize.Length - 2));
            SelectedFontPreview.FontSize = int.Parse(selectedFontSize.Substring(0, selectedFontSize.Length - 2));
        }

        private void changeSelectedFontFamilyPreview()
        {
            SelectedFontName.FontFamily = new FontFamily(selectedFontFamily);
            SelectedFontPreview.FontFamily = new FontFamily(selectedFontFamily);
            SelectedFontName.Text = selectedFontFamily;
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFontFamily = fontFamilyComboBox.SelectedItem.ToString();
            changeSelectedFontFamilyPreview();
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFontSize = fontSizeComboBox.SelectedItem.ToString();
            changeSelectedFontSizePreview();
        }

        private void applyFont()
        {
            double normalFontSize = double.Parse(selectedFontSize.Substring(0, selectedFontSize.Length - 2));

            Application.Current.Resources["Main_App_Font_Family"] = new FontFamily(selectedFontFamily);
            Application.Current.Resources["Main_App_Font_Size_Smallest"] = normalFontSize - 5;
            Application.Current.Resources["Main_App_Font_Size_Small"] = normalFontSize - 2;
            Application.Current.Resources["Main_App_Font_Size_Normal"] = normalFontSize;
            Application.Current.Resources["Main_App_Font_Size_Big"] = normalFontSize + 4;
            Application.Current.Resources["Main_App_Font_Size_Biggest"] = normalFontSize + 14;

            Properties.Settings.Default.Main_App_Font_Family = selectedFontFamily;
            Properties.Settings.Default.Main_App_Font_Size_Smallest = normalFontSize - 5;
            Properties.Settings.Default.Main_App_Font_Size_Small = normalFontSize - 2;
            Properties.Settings.Default.Main_App_Font_Size_Normal = normalFontSize;
            Properties.Settings.Default.Main_App_Font_Size_Big = normalFontSize + 4;
            Properties.Settings.Default.Main_App_Font_Size_Biggest = normalFontSize + 14;

            Properties.Settings.Default.Save();

            currentFontFamily = selectedFontFamily;
            currentFontSize = selectedFontSize;
        }

        private void ApplyFontChange_Click(object sender, RoutedEventArgs e)
        {
            applyFont();
        }
    }
}
