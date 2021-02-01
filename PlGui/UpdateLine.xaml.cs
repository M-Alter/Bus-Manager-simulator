using BLAPI;
using System;
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
        PO.Line line = new PO.Line();
        public int StationNumber;
        List<BO.Station> addStationsList = new List<BO.Station>();
        List<BO.Station> cbStationList = new List<BO.Station>();
        public UpdateLine(PO.Line line)
        {
            InitializeComponent();

            Title = string.Format("Line: " + line.LineNumber + " in " + line.Area.ToString().ToLower() + " area | info");
            this.line = line;
            this.DataContext = line;


            var existedStations = line.Stations.ToList();
            foreach (var item in bl.GetAllStations())
            {
                if (existedStations.FirstOrDefault(st => st.Station == item.Code) == null)
                {
                    cbStationList.Add(item);
                }
            }
            cbStations.ItemsSource = cbStationList;

            int index = 1;
            foreach (var station in line.Stations)
            {
                existStationsLBox.Items.Add(string.Format($"{index}: {station.Station} {station.StationName}"));
                index++;
            }
            
        }

       

        //get only number
        private void validateTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) => e.Handled = e.Text == null || !e.Text.All(char.IsDigit);


        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected = cbStations.SelectedItem as BO.Station;
            try
            {
                bl.UpdateLine(line.PersonalId, selected.Code, int.Parse(tbIndex.Text));
                Close();
            }
            catch (BO.AdjacentStationsExceptions ex)
            {
                BO.AdjacentStations[] adjacentStations = new BO.AdjacentStations[ex.adjacentStationsArray.Length];
                adjacentStations = ex.adjacentStationsArray;
                AdjacentStationInfo adjacentStationInfo = new AdjacentStationInfo(adjacentStations);
                adjacentStationInfo.ShowDialog();
            }
            //catch (BO.LineStationException ex)
            //{
            //    MessageBox.Show(ex.line.ToString() + ex.Message, "", MessageBoxButton.OK);
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK);
            }


        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
