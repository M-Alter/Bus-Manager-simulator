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

        public MainWindow()
        {
            InitializeComponent();
            initBuses();
            lvBuses.ItemsSource = buses;
        }
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

        private void lvBuses_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var currrentBus = (Bus)lvBuses.SelectedItem as Bus;
            Popup info = new Popup(currrentBus);
            info.DataContext = currrentBus;
            info.Show();
        }

        private void AddBusbtn_Click(object sender, RoutedEventArgs e)
        {
            AddBus addbus = new AddBus();
            addbus.Show();
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
                for (int i = 144; i > 0; i--)
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}


