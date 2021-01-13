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
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        private IBL bl = BLFactory.GetIBL();
        public AddBus()
        {
            InitializeComponent();
            startDateDPicker.DisplayDateEnd = DateTime.Today;
            startDateDPicker.SelectedDate = DateTime.Today;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (licenseTBox.Text == "" || licenseTBox.Text == "7 or 8 digits")
            {
                MessageBox.Show("License number can't be empty", "Wrong License", MessageBoxButton.OK);
                return;
            }
            int licenseNumber = int.Parse(licenseTBox.Text);
            int fuel = 1200;
            int milege = 0;
            bool flag = true;
            if (gasTBox.Text != "0 - 1200" && gasTBox.Text != "")
            {
                fuel = int.Parse(gasTBox.Text);
            }
            if (milegeTBox.Text != "digits only" && gasTBox.Text != "")
            {
                milege = int.Parse(gasTBox.Text);
            }
            BO.Bus bus = new BO.Bus
            {
                LicenseNum = licenseNumber,
                FromDate = DateTime.Parse(startDateDPicker.Text),
                FuelRemain = fuel,
                TotalTrip = 0,//int.Parse(milegeTBox.Text),
                Status = BO.Enums.BusStatus.READY
                
            };
            try
            {
                bl.AddBus(bus);
                flag = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                flag = false;
            }
            if (flag)
                MessageBox.Show("Bus added succefully");
                this.Close();
        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
            tb.Foreground = Brushes.Black ;
        }

        private void licenseTBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            
            if (e == null) return;
            //if the key pressed is enter
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (text.Text.Length > 0)
                {



                }
                e.Handled = true;
                return;
            }
            //if the pressed key is a function key then ignore the event
            if (e.Key == Key.Escape || e.Key == Key.Tab || e.Key == Key.Back
                || e.Key == Key.Delete || e.Key == Key.CapsLock ||
                e.Key == Key.LeftShift || e.Key == Key.RightShift ||
                e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl ||
                e.Key == Key.LeftAlt || e.Key == Key.RightAlt ||
                e.Key == Key.LWin || e.Key == Key.RWin || e.Key == Key.System ||
                e.Key == Key.Down || e.Key == Key.Up ) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
            {
                if (!(Keyboard.IsKeyDown(Key.LeftShift)) || (Keyboard.IsKeyDown(Key.RightShift)) ||
                    (Keyboard.IsKeyDown(Key.LeftCtrl)) || (Keyboard.IsKeyDown(Key.RightCtrl)) ||
                    (Keyboard.IsKeyDown(Key.LeftAlt)) || (Keyboard.IsKeyDown(Key.RightAlt))) return;
            }
            e.Handled = true;
            //MessageBox.Show("Only number are allowed", "Validation", MessageBoxButton.OKCancel);
        }
    }
}
