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
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
        private Bus mybus;
        public Popup(Bus myBus)
        {
            this.mybus = myBus;
            InitializeComponent();
            Title = String.Format("Bus no: {0} info",myBus.RegString);
            if (myBus.Gas < 120)
            {
                FuelBar.Foreground = Brushes.Red;
            }
            else
            {
                FuelBar.Foreground = Brushes.LightGreen;
            }
            PBarValue.Text = String.Format("{0}km",myBus.Gas.ToString());
            switch (myBus.BusState)
            {
                case State.READY:
                    BusState.Fill = Brushes.LawnGreen;
                    break;
                case State.BUSY:
                    BusState.Fill = Brushes.Red;
                    break;
                case State.REFUELING:
                    BusState.Fill = Brushes.Orange;
                    break;
                case State.SERVICING:
                    BusState.Fill = Brushes.Gray;
                    break;
                default:
                    break;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            
            mybus.Refuel();
        }

       
    }
}
