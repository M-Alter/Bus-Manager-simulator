using BLAPI;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        private IBL bl = BLFactory.GetIBL();
        List<BO.Station> addStationsList = new List<BO.Station>();
        List<StationStruct> stationStruct = new List<StationStruct>();
        public AddLine()
        {
            InitializeComponent();
            areaCMBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Areas));
        }

        struct StationStruct
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
            lastStopCMBox.IsEnabled = true;
            lastStopCMBox.ItemsSource = bl.GetAllStations(station => station.Code != firstStation.Code);
        }

        private void lastStopCMBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Station firstStation = (BO.Station)firstStopCMBox.SelectedItem as BO.Station;
            BO.Station lastStation = (BO.Station)lastStopCMBox.SelectedItem as BO.Station;
            addStopCMBox.IsEnabled = true;
            foreach (var item in bl.GetAllStations(station => (station.Code != firstStation.Code) || (station.Code != lastStation.Code)))
            {
                stationStruct.Add(new StationStruct { Name = item.Name, Code = item.Code, Checked = false });
            }
            addStopCMBox.ItemsSource = stationStruct;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            int lineNumber = int.Parse(lineTBox.Text);
            BO.Enums.Areas area = (BO.Enums.Areas)areaCMBox.SelectedItem;
            BO.Station firstStation = (BO.Station)firstStopCMBox.SelectedItem as BO.Station;
            BO.Station lastStation = (BO.Station)lastStopCMBox.SelectedItem as BO.Station;
            BO.Line line = new BO.Line
            {
                Area = area,
                Code = lineNumber,
                FirstStation = firstStation.Code,
                LastStation = lastStation.Code,
            };
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
            foreach (var station in stationStruct)
            {
                if(station.Checked)
                    stationsLBox.Items.Add(string.Format($"{station.Code} {station.Name}"));
            }
            //foreach (var station in addStationsList)
            //{
            //    stationsLBox.Items.Add(string.Format($"{station.Code} {station.Name}"));
            //}
            stationsLBox.Items.Add(string.Format($"{lastStation.Code} {lastStation.Name}"));
        }



       
    }
}
