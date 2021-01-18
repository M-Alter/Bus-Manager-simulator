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
         public LinePopUp()
        {
            InitializeComponent();
        }

        public LinePopUp(PO.Line line)
        {
            InitializeComponent();
            Title = string.Format("Line: " + line.LineNumber + " in " + line.Area.ToString().ToLower() + " area | info");
            lineGrid.Visibility = Visibility.Visible;
            this.line = line;
        }

        private void btRemove_Click(object sender, RoutedEventArgs e)
        {
            BO.LineStation lineStation = ((sender as Button).DataContext as BO.LineStation);
            bl.RemoveStationFromLine(line.PersonalId , lineStation.Station);
            line.Stations = bl.GetLine(line.PersonalId).Stations;
            this.DataContext = line;
            stationDgrid.Items.Refresh();
        }

        private void stationDgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
