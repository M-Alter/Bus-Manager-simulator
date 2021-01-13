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
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for RemoveLine.xaml
    /// </summary>
    public partial class RemoveLine : Window
    {
        struct LineDetails
        {
            int lineNumber;
            int lastStop;
        }
        private IBL bl = BLFactory.GetIBL();
        //List<LineDetails> lineDetailsList = new List<LineDetails>();
        List<string> linesList = new List<string>();
        public RemoveLine()
        {
            InitializeComponent();
            string str;
            foreach (var item in bl.GetAllLines())
            {
                if (item.LineNumber < 10)
                    str = string.Format("00{0}", item.LineNumber.ToString());
                else if (item.LineNumber < 100)
                    str = string.Format("0{0}", item.LineNumber.ToString());
                else
                    str = item.LineNumber.ToString();
                linesList.Add($"{str}   {item.LastStation}  {item.LastStationName}");
            }
            lineCBox.ItemsSource = linesList;
        }

        private void lineCMBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            BO.Line tempLine = new BO.Line();
            foreach (var item in bl.GetAllLines())
            {
                if (i == lineCBox.SelectedIndex)
                {
                    tempLine = item;
                    break;
                }
                i++;
            }

            try
            {
                if (lineCBox.SelectedItem != null)
                {
                    bl.RemoveLine(tempLine.LineNumber, tempLine.LastStation);

                    MessageBox.Show($"The line {lineCBox.SelectedItem} has delete");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
