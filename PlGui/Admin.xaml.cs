using BLAPI;
using BO;
using PO;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        ObservableCollection<PO.Bus> buses = new ObservableCollection<PO.Bus>();
        ObservableCollection<PO.Station> stations = new ObservableCollection<PO.Station>();
        ObservableCollection<PO.Line> lines = new ObservableCollection<PO.Line>();
        ObservableCollection<AdjacentStations> adjacentStations = new ObservableCollection<AdjacentStations>();

        IBL bl = BLFactory.GetIBL();
        public Admin()
        {
            InitializeComponent();

            foreach (var item in bl.GetAllBuses())
            {
                buses.Add(Tools.POBus(item));
            }
            buseslview.DataContext = buses;
            //buseslview.DataContext = bl.GetAllBuses();

            foreach (var item in bl.GetAllStations())
                stations.Add(Tools.POStation(item));
            stationslview.DataContext = stations;

            foreach (var item in bl.GetAllLines())
            {
                lines.Add(Tools.POLine(item));
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
            var currentLine = lineslview.SelectedItem as PO.Line;
            if (currentLine is PO.Line)
            {
                LinePopUp info = new LinePopUp(currentLine);
                info.DataContext = currentLine;
                info.Show();
            }
        }

        private void buseslview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentBus = buseslview.SelectedItem as PO.Bus;
            if (currentBus is PO.Bus)
            {

                BusPopUp info = new BusPopUp(currentBus);
                info.DataContext = currentBus;
                info.Show();
            }
        }

        private void stationslview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentStation = stationslview.SelectedItem as PO.Station;
            if (currentStation is PO.Station)
            {
                StationPopUp info = new StationPopUp(currentStation);
                info.DataContext = currentStation;
                info.Show();
            }
        }

        private void addBusBtn_Click(object sender, RoutedEventArgs e)
        {
            AddBus addBus = new AddBus();
            addBus.ShowDialog();

            buses.Clear();
            buses = null;
            buses = new ObservableCollection<PO.Bus>();

            foreach (var item in bl.GetAllBuses())
            {
                buses.Add(Tools.POBus(item));
            }
            buseslview.DataContext = buses;
        }

        private void addLineBtn_Click(object sender, RoutedEventArgs e)
        {
            AddLine addLine = new AddLine();
            addLine.ShowDialog();

            lines.Clear();
            lines = null;
            lines = new ObservableCollection<PO.Line>();

            foreach (var item in bl.GetAllLines())
            {
                lines.Add(Tools.POLine(item));
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
", "About", MessageBoxButton.OK, MessageBoxImage.Information);
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
            var currentBus = buseslview.SelectedItem as PO.Bus;
            if (currentBus is PO.Bus)
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
            var currentBus = buseslview.SelectedItem as PO.Bus;
            if (currentBus is PO.Bus)
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

        private void removeBusBtn_Click(object sender, RoutedEventArgs e)
        {
            RemoveBus removeBus = new RemoveBus();
            removeBus.ShowDialog();
            buses.Clear();
            buses = null;
            buses = new ObservableCollection<PO.Bus>();

            foreach (var item in bl.GetAllBuses())
                buses.Add(Tools.POBus(item));
            buseslview.DataContext = buses;
        }

        //===================================================================================================
        private void btnEditLine_Click(object sender, RoutedEventArgs e)
        {

        }
        //===================================================================================================

        //===================================================================================================
        private void removeLineBtn_Click(object sender, RoutedEventArgs e)
        {
            RemoveLine removeLine = new RemoveLine();
            removeLine.ShowDialog();
            lines.Clear();
            lines = null;
            lines = new ObservableCollection<PO.Line>();

            string str = "klum";
            int lineNumber = -9999, lastStation = -9999;
            if (removeLine.lineCBox.SelectedItem != null)
            {
                str = removeLine.lineCBox.SelectedItem.ToString();
                lineNumber = int.Parse(str.Substring(0, 3));
                lastStation = int.Parse(str.Substring(6, 5));
            }
            foreach (var item in bl.GetAllLines())
            {
                if (item.LineNumber != lineNumber && item.LastStation != lastStation)
                {
                    lines.Add(Tools.POLine(item));
                }
            }
            lineslview.DataContext = lines;
        }
        //======================================================================================================

        private void addStationBtn_Click(object sender, RoutedEventArgs e)
        {
            AddStation addStation = new AddStation();
            addStation.ShowDialog();

            stations.Clear();
            stations = null;
            stations = new ObservableCollection<PO.Station>();

            foreach (var item in bl.GetAllStations())
            {
                stations.Add(Tools.POStation(item));
            }
            stationslview.DataContext = stations;

        }
    }
}
