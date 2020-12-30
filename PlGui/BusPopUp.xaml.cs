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
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for BusPopUp.xaml
    /// </summary>
    public partial class BusPopUp : Window
    {
        public BusPopUp()
        {
            InitializeComponent();
        }


        public BusPopUp(Bus bus)
        {
            InitializeComponent();
            Title = string.Format("License Number: " + bus.LicenseNum + " | info");
        }
    }
}
