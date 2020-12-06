using System;
using System.Collections.Generic;
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
        List<Bus> buses;
        public MainWindow()
        {
            InitializeComponent();
            initBuses();
            lvBuses.ItemsSource = buses;
        }
        private void initBuses()
        {
            buses = new List<Bus>();
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
                    buses.Add(new Bus(getReg(newDate), newDate, gas: 10));
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
            //var context = item.DataContext as Bus;
            //MessageBox.Show(String.Format("{0}, {1}",currrentBus.MileageSinceService, currrentBus.Gas));            
            Popup info = new Popup(currrentBus);
            info.Show();
        }
    }
}
