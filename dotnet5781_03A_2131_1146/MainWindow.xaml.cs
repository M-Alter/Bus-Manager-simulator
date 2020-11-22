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
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotnet5781_02_2131_1146;

namespace dotnet5781_03A_2131_1146
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static BusCollection Company;
        private BusLine currentDisplayBusLine;
        private static void FillCollection()
        {
            int areaIndex = 0;
            List<BusStop> stops = new List<BusStop>();
            Random r = new Random(DateTime.Today.Millisecond);
            for (int i = 0; i < 40; i++)
                try{stops.Add(new BusStop(i + 10000, (Areas)(i / 8) + 1));}
                catch (BusException e)  {Console.WriteLine(e.Message);}
            Company = new BusCollection();
            for (int i = 1; i < 21; i++)
                try{Company.Add(new BusLine(i + 100, new BusStopRoute(stops[(i * 2) - 2], TimeSpan.Zero, 0), new BusStopRoute(stops[(i * 2) - 1], TimeSpan.FromMinutes((2 * i) + 6 % 5), 2 * i / 5), (Areas)(areaIndex++ / 4) + 1));}
                catch (BusException e){Console.WriteLine(e.Message);}
            try{
                Company.Add(new BusLine(150, new BusStopRoute(stops[0], TimeSpan.Zero, 00), new BusStopRoute(stops[39], TimeSpan.FromMinutes(4), 3.3)));
                for (int i = 2; i < 40; i++)
                    try{Company[150].AddStops(new BusStopRoute(stops[i - 1], TimeSpan.FromMinutes((i % 10) + 1), i * 1.333), i);}
                    catch (BusException e){Console.WriteLine(e.Message);}
            }
            catch (BusException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public MainWindow()
        {
            FillCollection();
            InitializeComponent();
            cbBuslines.ItemsSource = Company;
            cbBuslines.DisplayMemberPath = "LineNumber";
            cbBuslines.SelectedIndex = 0;
            showBusLine(((BusLine)cbBuslines.SelectedValue).LineNumber);
        }


        private void cbBuslines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showBusLine((cbBuslines.SelectedValue as BusLine).LineNumber);
        }

        private void showBusLine(int lineNumber)
        {
            currentDisplayBusLine = Company[lineNumber];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }
    }
}
