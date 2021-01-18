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
using BO;
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for PopUp.xaml
    /// </summary>
    public partial class LinePopUp : Window
    {
        static IBL bl = BLFactory.GetIBL();
        PO.Line line = new PO.Line();
        //public LinePopUp()
        //{
        //    InitializeComponent();
        //}

        public LinePopUp(PO.Line line)
        {
            InitializeComponent();
            Title = string.Format("Line: " + line.LineNumber + " in " + line.Area.ToString().ToLower() + " area | info");
            lineGrid.Visibility = Visibility.Visible;
            this.line = line;
            this.DataContext = line;
        }
        //===============================================================================================================================
        // needs to update the first and last sation in this window and on the main window
        // need to block possability to remove all stations
        // need to update all adjacent stations after removal of a station
        //===============================================================================================================================
        private void btRemove_Click(object sender, RoutedEventArgs e)
        {
            BO.LineStation lineStation = ((sender as Button).DataContext as BO.LineStation);
            ((PO.Line)this.DataContext).Stations.Remove(lineStation);
            line.Stations.Remove(lineStation);
            bl.RemoveStationFromLine(line.PersonalId, lineStation.Station);
            //line.Stations = new ObservableCollection<LineStation>(bl.GetLine(line.PersonalId).Stations);
            line.FirstStation = bl.GetLine(line.PersonalId).FirstStation;
            line.FirstStationName = bl.GetLine(line.PersonalId).FirstStationName;
            line.LastStation = bl.GetLine(line.PersonalId).LastStation;
            line.LastStationName = bl.GetLine(line.PersonalId).LastStationName;
            this.DataContext = line;
            stationDgrid.Items.Refresh();
        }
        //=================================================================================================================================
        private void stationDgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
