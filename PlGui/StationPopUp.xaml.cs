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
    /// Interaction logic for StationPopUp.xaml
    /// </summary>
    public partial class StationPopUp : Window
    {
        public StationPopUp()
        {
            InitializeComponent();
        }

        public StationPopUp(Station station)
        {
            InitializeComponent();
            Title = string.Format("Station Number: " + station.Code + " | info");
        }
    }
}
