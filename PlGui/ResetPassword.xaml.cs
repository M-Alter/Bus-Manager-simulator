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
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        IBL bl = BLFactory.GetIBL();
        public ResetPassword(string username)
        {
            InitializeComponent();
            usernameLbl.Content = username;
        }

        private void resetbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.ResendPassword(usernameLbl.Content.ToString(), emailTxtbox.Text);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
