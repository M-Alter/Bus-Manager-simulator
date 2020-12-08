using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;

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
            Title = String.Format("Bus no: {0} info", myBus.RegString);
            if (myBus.Gas < 120)
            {
                FuelBar.Foreground = Brushes.Red;
            }
            else
            {
                FuelBar.Foreground = Brushes.LightGreen;
            }
            PBarValue.Text = String.Format("{0}km", myBus.Gas.ToString());
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



        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                mybus.Refuel();
            }).Start();

            FuelBar.Value = mybus.Gas;
            FuelBar.Foreground = Brushes.LightGreen;

        }

        private void Pick_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
