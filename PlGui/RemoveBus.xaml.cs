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
        List<BO.Bus> allBuses = new List<BO.Bus>();
        public RemoveBus()
        {
            InitializeComponent();
            foreach (var item in bl.GetAllBuses())
                allBuses.Add(item);

            busCBox.ItemsSource = allBuses;

        }

        private void busCMBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.Bus bus = (BO.Bus)busCBox.SelectedItem as BO.Bus;

            if (busCBox.SelectedItem != null)
            {
                try
                {
                    bl.DeleteBus(bus.LicenseNum);
                    MessageBox.Show("The bus has delete","",MessageBoxButton.OK);
                    this.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
