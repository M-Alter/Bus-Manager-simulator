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

            myBus.setBusStateColor();

        }

        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Bus currentBus = (Bus)button.DataContext;
            if (currentBus.BusState != State.READY)
            {
                MessageBox.Show("This bus can't be availeble");
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
            thread = new Thread(() =>
            {
                currentBus.Gas = 0;
                for (int i = 13; i > 0; i--)
                {
                    Thread.Sleep(1000);
                    currentBus.Gas += 100;
                    this.Dispatcher.Invoke(() =>
                    {
                        currentBus.BusStateString = String.Format("Ready in {0}", i);
                    });
                }
                currentBus.BusState = State.READY;
                currentBus.setBusStateColor();
            });
            thread.Start();
        }

        private void Pick_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            PickBus pick = new PickBus((Bus)button.DataContext);
            Bus bus = (Bus)button.DataContext;
            if (bus.BusState != State.READY)
            {
                MessageBox.Show("This bus can't be availeble");
                return;
            }
            pick.Show();
        }

        private void Service_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Bus currentBus = (Bus)button.DataContext;
            if (currentBus.BusState != State.READY && currentBus.BusState != State.NOTREADY)
            {
                MessageBox.Show("This bus can't be availeble");
                return;
            }
            currentBus.BusState = State.SERVICING;
            currentBus.setBusStateColor();
            Thread thread = null;
            thread = new Thread(() =>
            {
                for (int i = 288; i > 0; i--)
                {
                    Thread.Sleep(1000);
                    this.Dispatcher.Invoke(() =>
                    {
                        currentBus.BusStateString = String.Format("Ready in {0}", i);
                    });

                }
                currentBus.BusState = State.READY;
                currentBus.setBusStateColor();
            });
            thread.Start();
            currentBus.Gas = 1200;
        }
    }
}
