using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BLAPI;
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
        ObservableCollection<Station> stations = new ObservableCollection<Station>();
        ObservableCollection<BO.Line> lines = new ObservableCollection<BO.Line>();
        ObservableCollection<AdjacentStations> adjacentStations = new ObservableCollection<AdjacentStations>();

        IBL bl = BLFactory.GetIBL(); 
        public Admin()
        {
            InitializeComponent();

            foreach (var item in bl.GetAllBuses())
            {
                buses.Add(item);
            }
            buseslview.DataContext = buses;

            foreach (var item in bl.GetAllStations())
                stations.Add(item);
            stationslview.DataContext = stations;

            foreach (var item in bl.GetAllLines())
            {
                lines.Add(item);
            }
            lineslview.DataContext = lines;

            foreach (var item in bl.GetAllAdjacentStations())
            {
                adjacentStations.Add(item);
            }
            adjStationsLview.DataContext = adjacentStations;
        }

        private void 
            lineslview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentLine = lineslview.SelectedItem as BO.Line;
            if (currentLine is BO.Line)
            {
                LinePopUp info = new LinePopUp(currentLine);
                info.DataContext = currentLine;
                info.Show(); 
            }
        }

        private void buseslview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentBus = buseslview.SelectedItem as Bus;
            if (currentBus is Bus) 
            {

            BusPopUp info = new BusPopUp(currentBus);
            info.DataContext = currentBus;
            info.Show();
            }
        }

        private void stationslview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentStation = stationslview.SelectedItem as Station;
            if (currentStation is Station)
            {
                StationPopUp info = new StationPopUp(currentStation);
                info.DataContext = currentStation;
                info.Show(); 
            }
        }

        private void addLineBtn_Click(object sender, RoutedEventArgs e)
        {
            AddLine addLine = new AddLine();
            addLine.ShowDialog();

            lines.Clear();
            foreach (var item in bl.GetAllLines())
            {
                lines.Add(item);
            }
            lineslview.DataContext = lines;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(-1);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"Version 1.0.0.0
© 2021 Menachem Alter & Inon Bezalel
","About",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void MenuBusesItem_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 0;
        }

        private void MenuLinesItem_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 1;
        }

        private void MenuStationsItem_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 2;
        }

        private void buseslview_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var currentBus = buseslview.SelectedItem as Bus;
            if (currentBus is Bus)
            {
                ContextMenu contextMenu = new ContextMenu();
                //contextMenu.
                //BusPopUp info = new BusPopUp(currentBus);
                //info.DataContext = currentBus;
                //info.Show();
            }
        }

        private void BusesMenuInfoItem_Click(object sender, RoutedEventArgs e)
        {
            var currentBus = buseslview.SelectedItem as Bus;
            if (currentBus is Bus)
            {

                BusPopUp info = new BusPopUp(currentBus);
                info.DataContext = currentBus;
                info.Show();
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }
    }
}
