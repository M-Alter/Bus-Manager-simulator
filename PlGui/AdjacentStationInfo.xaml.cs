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
    /// Interaction logic for AdjacentStationInfo.xaml
    /// </summary>
    public partial class AdjacentStationInfo : Window
    {
        IBL bL = BLFactory.GetIBL();
        public AdjacentStationInfo(BO.AdjacentStations[] adjacentStations)
        {
            InitializeComponent();
            this.DataContext = adjacentStations;
            //foreach (var item in adjacentStations)
            //{
            //    innerGrid.RowDefinitions.Add(new RowDefinition());
            //    this.Height += 150;
            //}
            //for (int i = 0; i < adjacentStations.Length; i++)
            //{
            //    StackPanel sPanel = new StackPanel();
            //    WrapPanel wPanelFrom = new WrapPanel();
            //    WrapPanel wPanelTo = new WrapPanel();
            //    WrapPanel wPanelInfo = new WrapPanel();
            //    sPanel.Children.Add(wPanelFrom);
            //    sPanel.Children.Add(wPanelTo);
            //    sPanel.Children.Add(wPanelInfo);
            //    wPanelFrom.Children.Add(new TextBlock { Text = string.Format("{0,-6} {1,-7}{2,-25}","From:", adjacentStations[i].Station1.ToString(), adjacentStations[i].Station1Name),  /*Padding = new Thickness(0, 0, 20, 0),*/ FontSize = 20});
            //    wPanelTo.Children.Add(new TextBlock { Text = string.Format("{0,-8} {1,-7}{2,-25}", "To:", adjacentStations[i].Station2.ToString(), adjacentStations[i].Station2Name),  /*Padding = new Thickness(0, 0, 20, 0),*/ FontSize = 20 }); ;
            //    wPanelInfo.Children.Add(new TextBlock { Text = "Distance", Padding = new Thickness(0, 0, 10, 0), FontSize = 20 });
            //    wPanelInfo.Children.Add(new TextBox { Name = "distanceTbox" + i.ToString(), Width = 70, Padding = new Thickness(0, 0, 10, 0) });
            //    wPanelInfo.Children.Add(new TextBlock { Text = "Time", Padding = new Thickness(10, 0, 10, 0), FontSize = 20 });
            //    wPanelInfo.Children.Add(new TextBox { Name = "timeTbox" + i.ToString(), Width = 70, Padding = new Thickness(0, 0, 10, 0) });
            //    Grid.SetRow(sPanel, i);
            //    innerGrid.Children.Add(sPanel);
            //}
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            BO.AdjacentStations adjacentStations = ((sender as Button).DataContext as BO.AdjacentStations);
            bL.UpdateAdjacentStatoins(new BO.AdjacentStations { Station1 = adjacentStations.Station1, Station2 = adjacentStations.Station2, Distance = adjacentStations.Distance, Time = adjacentStations.Time });
        }
    }
}
