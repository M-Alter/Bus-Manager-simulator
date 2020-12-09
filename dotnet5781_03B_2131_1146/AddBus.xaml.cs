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
            int reg = 0;
            DateTime beginDate = default(DateTime);
            if( reg != 0 || !int.TryParse(Reg.Text, out reg))
            {
                this.Close();
                MessageBox.Show("please enter a valid number");
                return;
            }
            if (beginDate != default(DateTime) || !DateTime.TryParse(BeginDate.Text, out beginDate))
            {
                this.Close();
                MessageBox.Show("please enter a date in format DD-MM-YYYY");
                return;
            }
            try
            {
                MainWindow.buses.Add(new Bus(reg, beginDate));
            }
            catch (Exception  ex)            
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();           
        }

       
    }
}
