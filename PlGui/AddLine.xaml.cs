using BLAPI;
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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        private IBL bl = BLFactory.GetIBL(); 
        public AddLine()
        {
            InitializeComponent();
            areaCMBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Areas)); 
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
            addStopCMBox.ItemsSource = bl.GetAllStations(station => (station.Code != firstStation.Code)|| (station.Code != lastStation.Code));
        }
    }
}
