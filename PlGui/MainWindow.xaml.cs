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
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetIBL();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AdminBtn_Click(object sender, RoutedEventArgs e)
        {
            credentialsGrid.Visibility = Visibility.Visible;
            
            //Admin admin = new Admin();
            //admin.Show();
            //Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (bl.ValidateUser(usernameTxtBox.Text, passwordPswrdBox.Password))
            {
                Admin admin = new Admin();
                admin.Show();
            }
            Close();
        }
    }
}
