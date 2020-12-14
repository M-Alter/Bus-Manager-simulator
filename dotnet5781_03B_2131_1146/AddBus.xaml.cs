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
            bool flag = true;
            while (flag)
            {
                int reg, milege = 0, gas = 1200;
                DateTime beginDate = default(DateTime), serviceDate = default(DateTime);
                // Validate register number input
                if (!int.TryParse(Reg.Text, out reg) || reg == 0 || reg < 1000000 || reg > 99999999)
                {
                    Reg.BorderBrush = Brushes.Red;
                    Reg.Text = string.Empty;
                    MessageBox.Show("Invalid bus number");
                    flag = false; continue;
                }
                // Validate BeginDate input
                if (!DateTime.TryParse(BeginDate.Text, out beginDate) || beginDate == default(DateTime) || beginDate > DateTime.Today)
                {
                    BeginDate.BorderBrush = Brushes.Red;
                    BeginDate.Text = string.Empty;
                    MessageBox.Show("invalid date");
                    flag = false; continue;
                }
                else if (beginDate.Year >= 2018 && reg <= 9999999)
                {
                    BeginDate.BorderBrush = Brushes.Red;
                    MessageBox.Show("Since 2018 register number should be 8 digits");
                    flag = false; continue;
                }
                else if (beginDate.Year < 2018 && reg >= 10000000)
                {
                    BeginDate.BorderBrush = Brushes.Red;
                    MessageBox.Show("Until 2018 register number should be 7 digits");
                    flag = false; continue;
                }
                // Validate milege input
                if (! (Milege.Text.Length == 0||int.TryParse(Milege.Text, out milege)) || milege < 0)
                {
                    Milege.BorderBrush = Brushes.Red;
                    Milege.Text = string.Empty;
                    MessageBox.Show("Invalid milege");
                    milege = 0;                    
                    flag = false; continue;
                }
                // Validate gas input
                if (!(Gas.Text.Length == 0||int.TryParse(Gas.Text, out gas)) || gas < 0 || gas > 1200)
                {
                    Gas.BorderBrush = Brushes.Red;
                    Gas.Text = string.Empty;
                    MessageBox.Show("Invalid gas");
                    gas = 1200;
                    flag = false; continue;
                }
                // Validate ServiceDate input
                serviceDate = beginDate;
                if (!(ServiceDate.Text.Length == 0||DateTime.TryParse(ServiceDate.Text, out serviceDate))|| serviceDate < beginDate || serviceDate > DateTime.Today)
                {
                    ServiceDate.BorderBrush = Brushes.Red;
                    ServiceDate.Text = string.Empty;
                    MessageBox.Show("invalid service date");
                    flag = false; continue;
                }
                flag = true;
                try
                {
                    if (serviceDate == default(DateTime))
                    {
                        MainWindow.buses.Add(new Bus(reg, beginDate, milege, gas, DateTime.Today));
                    }
                    else
                    {
                        MainWindow.buses.Add(new Bus(reg, beginDate, milege, gas, serviceDate));
                    }    
                    MessageBox.Show("The bus added succesfuly");
                    this.Close();
                    break;
                }
                catch (Exception  ex)            
                {
                    MessageBox.Show(ex.Message);
                    flag = false; continue;
                }
            }
                      
        }

        private void addCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
