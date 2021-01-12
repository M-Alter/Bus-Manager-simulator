using BLAPI;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for RemoveBus.xaml
    /// </summary>
    public partial class RemoveBus : Window
    {
        private IBL bl = BLFactory.GetIBL();
        List<int> allBusesNum = new List<int>();
        public RemoveBus()
        {
            InitializeComponent();
            foreach (var item in bl.GetAllBuses())
            {
                allBusesNum.Add(item.LicenseNum);
            }

            busCBox.ItemsSource = allBusesNum;
        }

        private void busCMBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            //BO.Bus bus = (BO.Bus)busCBox.SelectedItem as BO.Bus;
            int busNumber = -1, i = 0;
            foreach (var item in allBusesNum)
            {
                if (i == busCBox.SelectedIndex)
                {
                    busNumber = item;
                    break;
                }
                i++;
            }

            try
            {
                if (busCBox.SelectedItem != null)
                {
                    bl.DeleteBus(busNumber);

                    MessageBox.Show("The bus has delete");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
