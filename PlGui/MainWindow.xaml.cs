using BLAPI;
using System.Windows;

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

            usernameCmbBox.ItemsSource = bl.GetAllUserNames(true);
            //Admin admin = new Admin();
            //admin.Show();
            //Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            if (bl.ValidatePassword(usernameCmbBox.Text, passwordPswrdBox.Password))
            {
                Admin admin = new Admin();
                admin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("User name or passward is not valid");
            }

        }

        private void resetbtn_Click(object sender, RoutedEventArgs e)
        {
            bl.ResendPassword(usernameCmbBox.Text, emailTxtbox.Text);
        }
    }
}
