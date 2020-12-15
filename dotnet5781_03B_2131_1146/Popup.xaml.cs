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
            myBus.setBusStateColor();
        }

        /// <summary>
        /// event handler for a click on the refuel button of the popup bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Bus currentBus = (Bus)button.DataContext;
            if (currentBus.BusState != State.READY)
            {
                MessageBox.Show("This bus is currently unavaileble to be refueled");
                return;
            }
            float fuel = currentBus.Gas;
            if (fuel == 1200)
            {
                MessageBox.Show("This bus is already refueled");
                return;
            }
            currentBus.BusState = State.REFUELING;
            currentBus.setBusStateColor();
            //thread that refuels the bus and puts the bus to sleep for 2 hours
            Thread thread = null;
            thread = new Thread(() =>
            {
                //currentBus.Gas = 0;
                for (int i = 12; i > 0; i--)
                {
                    Thread.Sleep(1000);
                    currentBus.Gas += (1200 - fuel) / 12;
                    this.Dispatcher.Invoke(() =>
                    {
                        currentBus.BusStateString = String.Format("Ready in {0}", i);
                    });
                }
                currentBus.setBusState();
            });
            thread.Start();
        }

        /// <summary>
        /// event handler for a click on the pick button of popup window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pick_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            PickBus pick = new PickBus((Bus)button.DataContext);
            Bus bus = (Bus)button.DataContext;
            if (bus.BusState != State.READY)
            {
                MessageBox.Show("This bus is currently unavailable for a ride");
                return;
            }
            pick.Show();
        }

        /// <summary>
        /// event handler for a click on the service button in the popup window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Service_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Bus currentBus = (Bus)button.DataContext;
            if (currentBus.BusState != State.READY && currentBus.BusState != State.NOTREADY)
            {
                MessageBox.Show("This bus is currently unavaileble for service");
                return;
            }
            currentBus.BusState = State.SERVICING;
            currentBus.setBusStateColor();
            //thread that sleeps the bus for a day 
            Thread thread = null;
            thread = new Thread(() =>
            {
                for (int i = 144; i > 0; i--)
                {
                    Thread.Sleep(1000);
                    this.Dispatcher.Invoke(() =>
                    {
                        currentBus.BusStateString = String.Format("Ready in {0}", i);
                    });
                }
                currentBus.setServicingValue();
                currentBus.setBusState();
            });
            thread.Start();
        }
    }
}
