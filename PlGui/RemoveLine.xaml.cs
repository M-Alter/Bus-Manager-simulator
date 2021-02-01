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
    /// Interaction logic for RemoveLine.xaml
    /// </summary>
    public partial class RemoveLine : Window
    {

        private IBL bl = BLFactory.GetIBL();

        public RemoveLine()
        {
            InitializeComponent();
            lineCBox.ItemsSource = bl.GetAllLines();
        }


        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected = lineCBox.SelectedItem as BO.Line;
            if (selected is BO.Line)
            {
                try
                {
                    bl.RemoveLine(selected.PersonalId);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
