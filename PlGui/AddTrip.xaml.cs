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
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddTrip.xaml
    /// </summary>
    public partial class AddTrip : Window
    {
        private IBL bl = BLFactory.GetIBL();
        public AddTrip(object sender)
        {
            InitializeComponent();
            PO.Line line = this.DataContext as PO.Line;
            string str = string.Format("Add time trip for line");
            tripTitleLabel.Content = str;
        }

        private void validateTb_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = e.Text == null || !e.Text.All(char.IsDigit);

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!TimeSpan.TryParse(hourTb.Text + ':' + minutesTb.Text + ':' + secoundsTb.Text, out TimeSpan reuslt))
                {
                MessageBox.Show("Invalid time value");
                return;
            }

            PO.Line line = this.DataContext as PO.Line;
            bool flag;
            try
            {
                bl.AddLineTrip(line.PersonalId, reuslt);
                flag = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                flag = false;
            }
            if (flag)
                MessageBox.Show("Trip on {0} add succefully for line", reuslt.ToString());
                this.Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
