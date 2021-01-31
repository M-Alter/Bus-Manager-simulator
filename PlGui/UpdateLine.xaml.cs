using System;
using System.Collections.Generic;
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
namespace PlGui
{
    /// <summary>
    /// Interaction logic for UpdateLine.xaml
    /// </summary>
    public partial class UpdateLine : Window
    {
        static IBL bl = BLFactory.GetIBL();
        PO.Line line = new PO.Line();
        List<BO.Station> addStationsList = new List<BO.Station>();
        List<StationClass> stationForListView = new List<StationClass>();

        public UpdateLine(PO.Line line)
        {
            InitializeComponent();
            Title = string.Format("Line: " + line.LineNumber + " in " + line.Area.ToString().ToLower() + " area | info");
            //lineGrid.Visibility = Visibility.Visible;
            this.line = line;
            this.DataContext = line;

            BO.Station firstStation = new BO.Station();
            foreach (var item in bl.GetAllStations())
            {
                if (item.Code == line.FirstStation)
                {
                    firstStation = item as BO.Station;
                }
            }

            BO.Station lastStation = new BO.Station();
            foreach (var item in bl.GetAllStations())
            {
                if (item.Code == line.LastStation)
                {
                    lastStation = item as BO.Station;
                }
            }
            stationsLBox.Items.Add(string.Format($"{firstStation.Code} {firstStation.Name}"));
            stationsLBox.Items.Add(string.Format($"{lastStation.Code} {lastStation.Name}"));
            foreach (var item in bl.GetAllStations(station => (station.Code != firstStation.Code) && (station.Code != lastStation.Code)))
            {
                stationForListView.Add(new StationClass { Name = item.Name, Code = item.Code,/* Checked = false */});
            }
            //foreach (var item in stationForListView)
            //{
            //    foreach (var stop in bl.)
            //    {

            //    }
            //}
            addStopCMBox.ItemsSource = stationForListView;
        }

        class StationClass
        {
            public string Name { get; set; }
            public int Code { get; set; }
            public bool Checked { get; set; }
        }

        private void allCheckedBox(object sender, RoutedEventArgs e)
        {
            BO.Station firstStation = new BO.Station();
            foreach (var item in bl.GetAllStations())
            {
                if (item.Code == line.FirstStation)
                {
                    firstStation = item as BO.Station;
                }
            }

            BO.Station lastStation = new BO.Station();
            foreach (var item in bl.GetAllStations())
            {
                if (item.Code == line.LastStation)
                {
                    lastStation = item as BO.Station;
                }
            }
            stationsLBox.Items.Clear();
            stationsLBox.Items.Add(string.Format($"{firstStation.Code} {firstStation.Name}"));
            foreach (var station in stationForListView)
            {
                if (station.Checked == true)
                    stationsLBox.Items.Add(string.Format($"{station.Code} {station.Name}"));
            }
            stationsLBox.Items.Add(string.Format($"{lastStation.Code} {lastStation.Name}"));
        }



        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
