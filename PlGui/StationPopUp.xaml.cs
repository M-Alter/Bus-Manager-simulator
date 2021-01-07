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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for StationPopUp.xaml
    /// </summary>
    public partial class StationPopUp : Window
    {
        public StationPopUp()
        {
            InitializeComponent();
        }

        public StationPopUp(Station station)
        {
            InitializeComponent();
            Title = string.Format("Station Number: " + station.Code + " | info");
            //ObservableCollection<BO.StationLine> linesList = new ObservableCollection<int>(station.LinesAtStation);
            //Grid grid = new Grid();
            //grid.ShowGridLines = true;
            //for (int i = 0; i < 3; i++)
            //{
            //    grid.ColumnDefinitions.Add(new ColumnDefinition());
            //}
            //for (int i = 0; i <= linesList.Count()/3; i++)
            //{
            //    grid.RowDefinitions.Add(new RowDefinition());
            //}
            //for (int i = 0; i < linesList.Count(); i++)
            //{
            //    TextBlock txtBlock = new TextBlock();
            //    txtBlock.Text = linesList.GetEnumerator().ToString();
            //    txtBlock.FontSize = 14;
            //    txtBlock.FontWeight = FontWeights.Bold;
            //    txtBlock.Foreground = new SolidColorBrush(Colors.Green);
            //    txtBlock.VerticalAlignment = VerticalAlignment.Top;
            //    Grid.SetRow(txtBlock, i/3);
            //    Grid.SetColumn(txtBlock, i%3);
            //    this.Content = grid;
            //}
            ////linesLbox.DataContext = linesList;
        }
    }
}
