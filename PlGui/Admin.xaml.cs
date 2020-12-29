﻿using System;
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
using BLAPI;
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        ObservableCollection<Bus> buses = new ObservableCollection<Bus>();
        ObservableCollection<Station> stations = new ObservableCollection<Station>();
        ObservableCollection<BO.Line> lines = new ObservableCollection<BO.Line>();
        IBL bl = BLFactory.GetIBL(); 
        public Admin()
        {
            InitializeComponent();

            foreach (var item in bl.GetAllBuses())
            {
                buses.Add(item);
            }
            buseslview.DataContext = buses;

            foreach (var item in bl.GetAllStations())
                stations.Add(item);
            stationslview.DataContext = stations;

            foreach (var item in bl.GetAllLines())
            {
                lines.Add(item);
            }
            lineslview.DataContext = lines;
        }

        private void lineslview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currrentLine = (BO.Line)lineslview.SelectedItem as BO.Line;
            PopUp info = new PopUp(currrentLine);
            info.DataContext = currrentLine;
            info.Show();
        }

        private void buseslview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var currentBus = buseslview.SelectedItem as Bus;
            PopUp info = new PopUp(currentBus);
            info.DataContext = currentBus;
            info.Show();
        }
    }
}
