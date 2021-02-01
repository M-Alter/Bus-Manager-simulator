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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for UpdateTimeAndDictance.xaml
    /// </summary>
    public partial class UpdateTimeAndDistance : Window
    {
        public UpdateTimeAndDistance()
        {
            InitializeComponent();
        }


        //get only number
        private void validateTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) => e.Handled = e.Text == null || !e.Text.All(char.IsDigit);


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!(int.TryParse(tboxHour.Text, out int hour) && int.TryParse(tboxMin.Text, out int min) && int.TryParse(tboxSec.Text, out int sec) && double.TryParse(tboxDist.Text, out double distance)))
            {
                MessageBox.Show("Enter numbers only", "",MessageBoxButton.OK);
            }
            DialogResult = true;
        }
    }
}
