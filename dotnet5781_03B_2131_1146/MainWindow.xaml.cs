using System;
using System.Collections.ObjectModel;

using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace dotnet5781_03B_2131_1146
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Random r = new Random();
        public static ObservableCollection<Bus> buses;

        /// <summary>
        /// c'tor 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            initBuses();
            lvBuses.ItemsSource = buses;
        }

        /// <summary>
        /// creates the instances of the buses and puts the in the collection
        /// </summary>
        private void initBuses()
        {
            buses = new ObservableCollection<Bus>();
            DateTime newDate;
            bool flag = false;

            Func<DateTime> getRandomDate = () => new DateTime(r.Next(2000, 2020), r.Next(1, 12), r.Next(1, 28));
            Func<DateTime, int> getReg = (date) => date.Year >= 2018 ? r.Next(10000000, 99999999) : r.Next(1000000, 9999999);
            #region AddBusses
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    newDate = getRandomDate();
                    buses.Add(new Bus(getReg(newDate), newDate));
                }
                catch (Exception) { i--; }
            }
            while (flag == false)
            {
                try
                {
                    newDate = getRandomDate();
                    buses.Add(new Bus(getReg(newDate), newDate, mileage: 19990));
                    flag = true;
                }
                catch { }
            }
            flag = false;
            while (flag == false)
            {
                try
                {
                    newDate = getRandomDate();
                    buses.Add(new Bus(getReg(newDate), newDate, gas: 30));
                    flag = true;
                }
                catch { }
            }
            flag = false;
            while (flag == false)
            {
                try
                {
                    newDate = getRandomDate();
                    buses.Add(new Bus(getReg(newDate), newDate, dateTime: DateTime.Today.AddYears(-2)));
                    flag = true;
                }
                catch { }
            }
            #endregion
        }

        /// <summary>
        /// event handler for a double click on an item in the list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvBuses_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var currrentBus = (Bus)lvBuses.SelectedItem as Bus;
            Popup info = new Popup(currrentBus);
            info.DataContext = currrentBus;
            info.Show();
        }

        /// <summary>
        /// event handler for a click on Add Bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBusbtn_Click(object sender, RoutedEventArgs e)
        {
            AddBus addbus = new AddBus();
            addbus.ShowDialog();
        }
        
        /// <summary>
        /// event handler for a click on the pick button of a bus
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
        /// event handler for a click on the refuel button of a bus
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
                    currentBus.Gas += (1200-fuel)/12;
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
        /// event handler for a click on the service button of a bus
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

        /// <summary>
        /// event handler when pressing the X to exit the window, closes all the current threads in the background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}


