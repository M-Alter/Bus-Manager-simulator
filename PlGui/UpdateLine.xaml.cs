using BLAPI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for UpdateLine.xaml
    /// </summary>
    public partial class UpdateLine : Window
    {
        static IBL bl = BLFactory.GetIBL();
        BO.Line line = new BO.Line();
        public int StationNumber;
        List<BO.Station> addStationsList = new List<BO.Station>();
        ObservableCollection<StationClass> stationsExisted = new ObservableCollection<StationClass>();
        ObservableCollection<StationClass> stationsToAdd = new ObservableCollection<StationClass>();
        public UpdateLine(BO.Line line)
        {
            InitializeComponent();
            //Title
            Title = string.Format("Line: " + line.LineNumber + " in " + line.Area.ToString().ToLower() + " area | info");
            //lineGrid.Visibility = Visibility.Visible;
            this.line = line;
            this.DataContext = line;

            var existedStations = line.Stations.ToList();

            foreach (var item in bl.GetAllStations())
            {
                // line.Stations.ToList().FirstOrDefault(st => st.Station == item.Code) == null
                if (existedStations.FirstOrDefault(st => st.Station == item.Code) == null)
                {
                    stationsToAdd.Add(new StationClass { Name = item.Name, Code = item.Code, Checked = false });
                }
                else
                {
                    stationsExisted.Add(new StationClass { Name = item.Name, Code = item.Code, Checked = true });
                }
            }
            
            foreach (var station in stationsToAdd)
            {
                stationsLBox.Items.Add(string.Format($"{station.Code} {station.Name}"));
            }
            int index = 1;
            foreach (var station in stationsExisted)
            {
                existStationsLBox.Items.Add(string.Format($"{index}: {station.Code} {station.Name}"));
                index++;
            }
            //StationNumber = 
        }

        class StationClass
        {
            public string Name { get; set; }
            public int Code { get; set; }
            public bool Checked { get; set; }
        }

        private void allCheckedBox(object sender, RoutedEventArgs e)
        {
            //BO.Station firstStation = new BO.Station();
            //foreach (var item in bl.GetAllStations())
            //{
            //    if (item.Code == line.FirstStation)
            //    {
            //        firstStation = item as BO.Station;
            //    }
            //}

            //BO.Station lastStation = new BO.Station();
            //foreach (var item in bl.GetAllStations())
            //{
            //    if (item.Code == line.LastStation)
            //    {
            //        lastStation = item as BO.Station;
            //    }
            //}
            //stationsLBox.Items.Clear();
            //stationsLBox.Items.Add(string.Format($"{firstStation.Code} {firstStation.Name}"));
            foreach (var station in stationsExisted)
            {
                if (station.Checked == true)
                    stationsLBox.Items.Add(string.Format($"{station.Code} {station.Name}"));
            }
            //stationsLBox.Items.Add(string.Format($"{lastStation.Code} {lastStation.Name}"));
        }

        //get only number
        private void validateTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) => e.Handled = e.Text == null || !e.Text.All(char.IsDigit);


        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateLine(line, int.Parse(lineNumberTB.Text),1);
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
