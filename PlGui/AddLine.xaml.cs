using BLAPI;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        private IBL bl = BLFactory.GetIBL();
        List<BO.Station> addStationsList = new List<BO.Station>();
        List<StationClass> stationForListView = new List<StationClass>();


        public AddLine()
        {
            InitializeComponent();
            areaCMBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Areas));
        }

        class StationClass
        {
            public string Name { get; set; }
            public int Code { get; set; }
            public bool Checked { get; set; }
        }

        private void areaCMBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lineTBox.IsEnabled = true;
            firstStopCMBox.IsEnabled = true;
            firstStopCMBox.ItemsSource = bl.GetAllStations();
        }

        private void firstStopCMBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Station firstStation = (BO.Station)firstStopCMBox.SelectedItem as BO.Station;
            firstStopCMBox.IsEnabled = false;
            lastStopCMBox.IsEnabled = true;
            stationsLBox.Items.Clear();
            stationsLBox.Items.Add(string.Format($"{firstStation.Code} {firstStation.Name}"));
            lastStopCMBox.ItemsSource = bl.GetAllStations(station => station.Code != firstStation.Code);
        }

        private void lastStopCMBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Station firstStation = (BO.Station)firstStopCMBox.SelectedItem as BO.Station;
            BO.Station lastStation = (BO.Station)lastStopCMBox.SelectedItem as BO.Station;
            lastStopCMBox.IsEnabled = false;
            addStopCMBox.IsEnabled = true;
            stationsLBox.Items.Clear();
            stationsLBox.Items.Add(string.Format($"{firstStation.Code} {firstStation.Name}"));
            stationsLBox.Items.Add(string.Format($"{lastStation.Code} {lastStation.Name}"));
            foreach (var item in bl.GetAllStations(station => (station.Code != firstStation.Code) && (station.Code != lastStation.Code)))
            {
                stationForListView.Add(new StationClass { Name = item.Name, Code = item.Code,/* Checked = false */});
            }
            addStopCMBox.ItemsSource = stationForListView;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = 1;
            int lineNumber = int.Parse(lineTBox.Text);
            BO.Enums.Areas area = (BO.Enums.Areas)areaCMBox.SelectedItem;
            BO.Station firstStation = (BO.Station)firstStopCMBox.SelectedItem as BO.Station;
            BO.Station lastStation = (BO.Station)lastStopCMBox.SelectedItem as BO.Station;
            List<BO.LineStation> stations = new List<BO.LineStation>();
            stations.Add(new BO.LineStation { Station = firstStation.Code, StationName = firstStation.Name, Index = index++ });
            foreach (var item in stationForListView)
                if (item.Checked)
                {
                    stations.Add(new BO.LineStation { Station = item.Code, StationName = item.Name, Index = index++ });
                }
            stations.Add(new BO.LineStation { Station = lastStation.Code, StationName = lastStation.Name, Index = index++ });
            BO.Line line = new BO.Line
            {
                Area = area,
                LineNumber = lineNumber,
                FirstStation = firstStation.Code,
                LastStation = lastStation.Code,
                Stations = stations
            };
            try
            {
                bl.AddLine(line);
            }
            catch (BO.AdjacentStationsExceptions ex)
            {
                BO.AdjacentStations[] adjacentStations = new BO.AdjacentStations[ex.adjacentStationsArray.Length];
                adjacentStations = ex.adjacentStationsArray;
                AdjacentStationInfo adjacentStationInfo = new AdjacentStationInfo(adjacentStations);
                adjacentStationInfo.ShowDialog();
            }
            Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void allCheckedBox(object sender, RoutedEventArgs e)
        {
            BO.Station firstStation = (BO.Station)firstStopCMBox.SelectedItem as BO.Station;
            BO.Station lastStation = (BO.Station)lastStopCMBox.SelectedItem as BO.Station;
            stationsLBox.Items.Clear();
            stationsLBox.Items.Add(string.Format($"{firstStation.Code} {firstStation.Name}"));
            foreach (var station in stationForListView)
            {
                if (station.Checked == true)
                    stationsLBox.Items.Add(string.Format($"{station.Code} {station.Name}"));
            }
            stationsLBox.Items.Add(string.Format($"{lastStation.Code} {lastStation.Name}"));
        }

        private void lineTBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //if the key pressed is enter
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (text.Text.Length > 0)
                {



                }
                e.Handled = true;
                return;
            }
            //if the pressed key is a function key then ignore the event
            if (e.Key == Key.Escape || e.Key == Key.Tab || e.Key == Key.Back
                || e.Key == Key.Delete || e.Key == Key.CapsLock ||
                e.Key == Key.LeftShift || e.Key == Key.RightShift ||
                e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl ||
                e.Key == Key.LeftAlt || e.Key == Key.RightAlt ||
                e.Key == Key.LWin || e.Key == Key.RWin || e.Key == Key.System ||
                e.Key == Key.Left || e.Key == Key.Down ||
                e.Key == Key.Up || e.Key == Key.Right) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
            {
                if (!(Keyboard.IsKeyDown(Key.LeftShift)) || (Keyboard.IsKeyDown(Key.RightShift)) ||
                    (Keyboard.IsKeyDown(Key.LeftCtrl)) || (Keyboard.IsKeyDown(Key.RightCtrl)) ||
                    (Keyboard.IsKeyDown(Key.LeftAlt)) || (Keyboard.IsKeyDown(Key.RightAlt))) return;
            }
            e.Handled = true;

        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {

            //areaCMBox.SelectedItem = null;
            //lineTBox.Clear();
            //lastStopCMBox.SelectedItem = null;
            //addStopCMBox.SelectedItem = null;

        }

        


    }
}
