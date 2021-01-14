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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        private IBL bl = BLFactory.GetIBL();
        public AddStation()
        {
            InitializeComponent();
        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
            tb.Foreground = Brushes.Black;
        }

        private void numberTBox_PreviewKeyDown(object sender, KeyEventArgs e)
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
                e.Key == Key.Down || e.Key == Key.Up) return;
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

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(numberTBox.Text == "5 digits" || numberTBox.Text == "")
            {
                MessageBox.Show("Station number can't be empty", "Wrong number", MessageBoxButton.OK);
                return;
            }
            if (nameTBox.Text == "string" || nameTBox.Text == "")
            {
                MessageBox.Show("Station name can't be empty", "Wrong number", MessageBoxButton.OK);
                return;
            }
            BO.Station station = new BO.Station
            {
                Code = int.Parse(numberTBox.Text),
                Name = nameTBox.Text
            };
            bool flag = true;
            try
            {
                bl.AddStation(station);
                flag = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                flag = false;
            }
            if (flag)
            {
                MessageBox.Show("Station added succefully");
                this.Close();
            }
        }
    }
}
