using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

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

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            lvBuses.Items.Refresh();
        }


        private void Pick_Click(object sender, RoutedEventArgs e)
        {
            PickBus pick = new PickBus((Bus)lvBuses.SelectedItem);
            pick.Show();

        }

        private void Refuel_Click(object sender, RoutedEventArgs e)
        {
            Bus currentBus = (Bus)lvBuses.SelectedItem;
            if(currentBus.Gas == 1200)
            {
                MessageBox.Show("This bus already refueled");
                return;
            }
            currentBus.BusState = State.REFUELING;
            Thread thread = null;
            thread = new Thread(() =>
            {
                for (int i = 12; i > 0; i--)
                {
                    Thread.Sleep(1000);
                    this.Dispatcher.Invoke(() =>
                    {
                        currentBus.BusStateString = String.Format("Reday in {0}", i.ToString());
                        //String s = i.ToString();
                    });
                }
            });
            thread.Start();
            currentBus.BusStateString = "Ready";
            currentBus.Gas = 1200;
            currentBus.BusState = State.READY;
        }
    }
}
