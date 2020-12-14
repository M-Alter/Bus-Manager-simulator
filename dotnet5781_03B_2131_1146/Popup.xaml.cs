using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace dotnet5781_03B_2131_1146
{
    /// <summary>
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
        private Bus myBus;
        public Popup(Bus MyBus)
        {
            InitializeComponent();
            this.myBus = MyBus;
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
            Button button = (Button)sender;
            Bus currentBus = (Bus)button.DataContext;
            if (currentBus.BusState == State.BUSY)
            {
                MessageBox.Show("Bus can't be refueled while driving");
                return;
            }
            if (currentBus.BusState == State.REFUELING || currentBus.BusState == State.SERVICING)
            {
                MessageBox.Show("Bus can't be refueled now");
                return;
            }
            if (currentBus.Gas == 1200)
            {
                MessageBox.Show("This bus already refueled");
                return;
            }
            currentBus.BusState = State.REFUELING;
            currentBus.setBusStateColor();

            Thread thread = null;
            this.IsEnabled = false;
            thread = new Thread(() =>
            {
                currentBus.Gas = 120;
                this.Dispatcher.Invoke(() =>
                {
                    FuelBar.Foreground = Brushes.LightGreen;
                });
                for (int i = 11; i > 0; i--)
                {
                    Thread.Sleep(1000);
                    currentBus.Gas += 100;
                    this.Dispatcher.Invoke(() =>
                    {
                        currentBus.BusStateString = String.Format("Reday in {0}", i.ToString());
                    });
                }
                currentBus.BusState = State.READY;
                currentBus.setBusStateColor();
            });
            thread.Start();
            this.IsEnabled = true;


        }

        private void Pick_Click(object sender, RoutedEventArgs e)
        {
            PickBus pick = new PickBus(myBus);
            pick.Show();
        }
    }
}
