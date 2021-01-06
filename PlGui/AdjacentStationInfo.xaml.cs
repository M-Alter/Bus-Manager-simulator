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
        public AdjacentStationInfo(BO.AdjacentStations[] adjacentStations)
        {
            InitializeComponent();
            foreach (var item in adjacentStations)
            {
                innerGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < adjacentStations.Length; i++)
            {
                WrapPanel panel = new WrapPanel();
                panel.Children.Add(new TextBlock { Text = adjacentStations[i].Station1.ToString() });
                panel.Children.Add(new TextBlock { Text = adjacentStations[i].Station2.ToString() });
                panel.Children.Add(new TextBox { Name = "distanceTbox" + i.ToString() });
                panel.Children.Add(new TextBox { Name = "timeTbox" + i.ToString() });
                Grid.SetRow(panel, i);
                innerGrid.Children.Add(panel);
            }
        }
    }
}
