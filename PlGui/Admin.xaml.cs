using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
        IBL bl = BLFactory.GetIBL(); 
        public Admin()
        {
            InitializeComponent();

            foreach (var item in bl.GetAllBuses())
            {
                buses.Add(item);
            }
            buseslview.DataContext = buses;
        }
    }
}
