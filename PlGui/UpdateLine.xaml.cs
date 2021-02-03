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
            int[] Ind = new int[index];
            for (int i = 1; i < index; i++)
            {
                //Ind[i] = i + 1;
                cbIndex.Items.Add(i);
            }
            
        }

       

        //get only number
        private void validateTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) => e.Handled = e.Text == null || !e.Text.All(char.IsDigit);


        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.Station selected = new BO.Station();
            int selectedIndex = 0;
            if (cbStations.SelectedItem != null)
                selected = cbStations.SelectedItem as BO.Station;
            else
                MessageBox.Show("Choose station to add");
            if (cbIndex.SelectedItem != null)
            {
                selectedIndex = int.Parse(cbIndex.SelectedItem.ToString());
            }
            else
                MessageBox.Show("Choose index");
            //MessageBox.Show($"{int.Parse(cbIndex.SelectedItem.ToString())}   :    {selectedIndex}");
            try
            {
                if (selectedIndex != 0 && cbStations.SelectedItem != null)
                {
                    bl.UpdateLine(line.PersonalId, selected.Code, selectedIndex);
                    Close();
                }
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
