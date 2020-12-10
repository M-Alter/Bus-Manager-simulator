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

namespace dotnet5781_03B_2131_1146
{
    /// <summary>
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        public AddBus()
        {
            InitializeComponent();
        }

        private void addSave_Click(object sender, RoutedEventArgs e)
        {
            bool fleg = true;
            while (fleg)
            {
                int reg;
                DateTime beginDate = default(DateTime);
                if (!int.TryParse(Reg.Text, out reg) || reg == 0 || reg < 1000000 || reg > 99999999)
                {
                    Reg.BorderBrush = Brushes.Red;
                    Reg.Text = string.Empty;
                    MessageBox.Show("Invalid bus number");
                    fleg = false; continue;
                }
                if (!DateTime.TryParse(BeginDate.Text, out beginDate) || beginDate == default(DateTime))
                {
                    BeginDate.BorderBrush = Brushes.Red;
                    BeginDate.Text = string.Empty;
                    MessageBox.Show("invalid date");
                    fleg = false; continue;
                }
                fleg = true;
                try
                {
                    MainWindow.buses.Add(new Bus(reg, beginDate));
                    MessageBox.Show("The bus added succesfuly");
                    this.Close();
                    break;
                }
                catch (Exception  ex)            
                {
                    MessageBox.Show(ex.Message);
                    fleg = false; continue;
                }
            }
                      
        }

        private void addCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
