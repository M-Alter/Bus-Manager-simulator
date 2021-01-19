using BLAPI;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        ObservableCollection<PO.Bus> buses = new ObservableCollection<PO.Bus>();
        ObservableCollection<PO.Station> stations = new ObservableCollection<PO.Station>();
        ObservableCollection<PO.Line> lines = new ObservableCollection<PO.Line>();
        ObservableCollection<PO.AdjacentStations> adjacentStations = new ObservableCollection<PO.AdjacentStations>();

        IBL bl = BLFactory.GetIBL();
        BO.Station busStation;
        // ***********************************************************************************************88
        //public Admin()
        //{
        //    InitializeComponent();

        //    foreach (var item in bl.GetAllBuses())
        //    {
        //        buses.Add(Tools.POBus(item));
        //    }
        //    buseslview.DataContext = buses;
        //    //buseslview.DataContext = bl.GetAllBuses();

        //    foreach (var item in bl.GetAllStations())
        //        stations.Add(Tools.POStation(item));
        //    stationslview.DataContext = stations;

        //    foreach (var item in bl.GetAllLines())
        //    {
        //        lines.Add(Tools.POLine(item));
        //    }
        //    lineslview.DataContext = lines;

        //    foreach (var item in bl.GetAllAdjacentStations())
        //    {
        //        adjacentStations.Add(Tools.POAdjacentStations(item));
        //    }
        //    adjStationsLview.DataContext = adjacentStations;
        //}
        // ***********************************************************************

        public Admin()
        {
            SimulatorInactive = true;
            timerWorker = new BackgroundWorker();
            InitializeComponent();
            timerWorker.DoWork += (s, e) =>
            {
                workerThread = Thread.CurrentThread;
                bl.StartSimulator(startTime, rate, (time) => timerWorker.ReportProgress(0, time));
                while (!timerWorker.CancellationPending) try { Thread.Sleep(1000000); } catch (Exception ex) { }
            };
            timerWorker.ProgressChanged += timer_ProgressChanged;
            timerWorker.RunWorkerCompleted += (s, e) =>
            {
                SimulatorInactive = true;
                simulatorBtn.Content = "Start";
                simulatorBtn.Background = Brushes.LightGreen;
                bl.StopSimulator();
            };

            timerWorker.WorkerReportsProgress = true;
            timerWorker.WorkerSupportsCancellation = true;
            //---------------------------------------------------------------------------------------
            panelWorker = new BackgroundWorker();
            panelWorker.DoWork += (s, e) =>
            {
                bl.SetStationPanel((int)e.Argument, stationObserver);
                while (!panelWorker.CancellationPending) try { Thread.Sleep(1000); } catch (Exception ex) { }

            };
            panelWorker.ProgressChanged += panel_ProgressChanged;
            panelWorker.WorkerReportsProgress = true;
            panelWorker.WorkerSupportsCancellation = true;
            panelWorker.RunWorkerCompleted += (s, e) =>
            {
                if (busStation != null)
                {
                    //busLineListView.ItemsSource = bl.GetBusLineNumbers(busStation.Code).OrderBy(n => n);
                    panelLines = new List<LineTiming>();
                    lineTimingListView.ItemsSource = null;
                    panelWorker.RunWorkerAsync(busStation.Code);
                }
            };
            //-------------------------------------------------------------------------------------------

            foreach (var item in bl.GetAllBuses())
            {
                buses.Add(Tools.POBus(item));
            }
            buseslview.DataContext = buses;
            //buseslview.DataContext = bl.GetAllBuses();

            foreach (var item in bl.GetAllStations())
                stations.Add(Tools.POStation(item));
            stationslview.DataContext = stations;

            foreach (var item in bl.GetAllLines())
            {
                lines.Add(Tools.POLine(item));
            }
            lineslview.DataContext = lines;

            foreach (var item in bl.GetAllAdjacentStations())
            {
                adjacentStations.Add(Tools.POAdjacentStations(item));
            }
            adjStationsLview.DataContext = adjacentStations;
        }


        private void lineslview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentLine = lineslview.SelectedItem as PO.Line;
            if (currentLine is PO.Line)
            {
                LinePopUp info = new LinePopUp(currentLine);
                info.DataContext = currentLine;
                info.Show();
            }
        }

        private void buseslview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentBus = buseslview.SelectedItem as PO.Bus;
            if (currentBus is PO.Bus)
            {

                BusPopUp info = new BusPopUp(currentBus);
                info.DataContext = currentBus;
                info.Show();
            }
        }

        private void stationslview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentStation = stationslview.SelectedItem as PO.Station;
            if (currentStation is PO.Station)
            {
                lvYellowPanel.DataContext = currentStation.LinesAtStation;
                //StationPopUp info = new StationPopUp(currentStation);
                //info.DataContext = currentStation;
                //info.Show();
                if (panelWorker.IsBusy)
                    panelWorker.CancelAsync();
                else
                {
                    //lvYellowPanel.ItemsSource = bl.GetBusLineNumbers(busStation.Code).OrderBy(n => n);
                    panelLines = new List<LineTiming>();
                    lineTimingListView.ItemsSource = null;
                    panelWorker.RunWorkerAsync(currentStation.Code);
                }
            }
        }

        private void addBusBtn_Click(object sender, RoutedEventArgs e)
        {
            AddBus addBus = new AddBus();
            addBus.ShowDialog();

            buses.Clear();
            buses = null;
            buses = new ObservableCollection<PO.Bus>();

            foreach (var item in bl.GetAllBuses())
            {
                buses.Add(Tools.POBus(item));
            }
            buseslview.DataContext = buses;
        }

        private void addLineBtn_Click(object sender, RoutedEventArgs e)
        {
            AddLine addLine = new AddLine();
            addLine.ShowDialog();

            lines.Clear();
            lines = null;
            lines = new ObservableCollection<PO.Line>();

            foreach (var item in bl.GetAllLines())
            {
                lines.Add(Tools.POLine(item));
            }

            lineslview.DataContext = lines;

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(-1);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"Version 1.0.0.0
© 2021 Menachem Alter & Inon Bezalel
", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuBusesItem_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 0;
        }

        private void MenuLinesItem_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 1;
        }

        private void MenuStationsItem_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 2;
        }

        private void buseslview_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var currentBus = buseslview.SelectedItem as PO.Bus;
            if (currentBus is PO.Bus)
            {
                ContextMenu contextMenu = new ContextMenu();
                //contextMenu.
                //BusPopUp info = new BusPopUp(currentBus);
                //info.DataContext = currentBus;
                //info.Show();
            }
        }

        private void BusesMenuInfoItem_Click(object sender, RoutedEventArgs e)
        {
            var currentBus = buseslview.SelectedItem as PO.Bus;
            if (currentBus is PO.Bus)
            {

                BusPopUp info = new BusPopUp(currentBus);
                info.DataContext = currentBus;
                info.Show();
            }
        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void removeBusBtn_Click(object sender, RoutedEventArgs e)
        {
            RemoveBus removeBus = new RemoveBus();
            removeBus.ShowDialog();
            buses.Clear();
            buses = null;
            buses = new ObservableCollection<PO.Bus>();

            foreach (var item in bl.GetAllBuses())
                buses.Add(Tools.POBus(item));
            buseslview.DataContext = buses;
        }

        //===================================================================================================
        private void btnEditLine_Click(object sender, RoutedEventArgs e)
        {

        }
        //===================================================================================================

        //===================================================================================================
        private void removeLineBtn_Click(object sender, RoutedEventArgs e)
        {
            RemoveLine removeLine = new RemoveLine();
            removeLine.ShowDialog();
            lines.Clear();
            lines = null;
            lines = new ObservableCollection<PO.Line>();

            string str = "klum";
            int lineNumber = -9999, lastStation = -9999;
            if (removeLine.lineCBox.SelectedItem != null)
            {
                str = removeLine.lineCBox.SelectedItem.ToString();
                lineNumber = int.Parse(str.Substring(0, 3));
                lastStation = int.Parse(str.Substring(6, 5));
            }
            foreach (var item in bl.GetAllLines())
            {
                if (item.LineNumber != lineNumber && item.LastStation != lastStation)
                {
                    lines.Add(Tools.POLine(item));
                }
            }
            lineslview.DataContext = lines;
        }
        //======================================================================================================

        private void addStationBtn_Click(object sender, RoutedEventArgs e)
        {
            AddStation addStation = new AddStation();
            addStation.ShowDialog();

            stations.Clear();
            stations = null;
            stations = new ObservableCollection<PO.Station>();

            foreach (var item in bl.GetAllStations())
            {
                stations.Add(Tools.POStation(item));
            }
            stationslview.DataContext = stations;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (timerWorker.IsBusy)
            {
                timerWorker.CancelAsync();
                workerThread.Interrupt();
            }
            if (panelWorker.IsBusy)
                panelWorker.CancelAsync();
            Environment.Exit(-2);
        }


        #region Simulator

        TimeSpan startTime;
        BackgroundWorker timerWorker;
        Thread workerThread;
        int rate;

        BackgroundWorker panelWorker; // bkg worker for updating arriving lines
        List<LineTiming> panelLines = new List<LineTiming>(); // collection of arriving lines

        public static readonly DependencyProperty SimulatorInactiveProperty = DependencyProperty.Register("SimulatorInactive", typeof(Boolean), typeof(Admin));
        private bool SimulatorInactive
        {
            get => (bool)GetValue(SimulatorInactiveProperty);
            set => SetValue(SimulatorInactiveProperty, value);
        }

        private void timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan time = (TimeSpan)e.UserState;
            hourTb.Text = String.Format($"{time.Hours:D2}");
            minutesTb.Text = String.Format($"{time.Minutes:D2}");
            secoundsTb.Text = String.Format($"{time.Seconds:D2}");
        }

        private void validateTb_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = e.Text == null || !e.Text.All(char.IsDigit);

        private void simulatorBtn_Click(object sender, RoutedEventArgs e)
        {
            int hh, mm, ss;
            if (SimulatorInactive)
            {
                if (!(int.TryParse(hourTb.Text, out hh) && int.TryParse(minutesTb.Text, out mm)
                && int.TryParse(secoundsTb.Text, out ss) && int.TryParse(rateTb.Text, out rate)))
                {
                    MessageBox.Show("Wrong timer format");
                    return;
                }
                if (hh > 23 || mm > 59 || ss > 59)
                {
                    MessageBox.Show("Invalid time value");
                    return;
                }
                startTime = new TimeSpan(hh, mm, ss);
                timerWorker.RunWorkerAsync();
                SimulatorInactive = false;
                simulatorBtn.Content = "Stop";
                simulatorBtn.Background = Brushes.Red;
            }
            else
            {
                timerWorker.CancelAsync();
                workerThread.Interrupt();
            }
        }

        private void panel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LineTiming lineTiming = (LineTiming)e.UserState;
            int index = panelLines.IndexOf(lineTiming);
            if (index == -1)
            { // It's a new line bus coming soon here
                if (lineTiming.Timing == TimeSpan.Zero) return;
                panelLines.Add(lineTiming);
                panelLines.Sort((lnTime1, lnTime2) => (int)(lnTime1.Timing - lnTime2.Timing).TotalMilliseconds);
            }
            else
            {
                if (lineTiming.Timing == TimeSpan.Zero)
                    panelLines.Remove(lineTiming);
                else
                    panelLines.Sort((lt1, lt2) => (int)(lt1.Timing - lt2.Timing).TotalMilliseconds);
            }
            lineTimingListView.ItemsSource = null;
            int count = (panelLines.Count < 5) ? panelLines.Count : 5;
            lineTimingListView.ItemsSource = panelLines.GetRange(0, count);
        }

        private void stationObserver(LineTiming lineTiming)
            => panelWorker.ReportProgress(0, lineTiming);

        #endregion
    }
}
