using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dotnet5781_03B_2131_1146
{
    /// <summary>
    /// Interaction logic for PickBus.xaml
    /// </summary>
    public partial class PickBus : Window
    {
        private Bus MyBus;
        private static Random r = new Random();
        public PickBus(Bus myBus)
        {
            InitializeComponent();
            MyBus = myBus;
        }

        private void Distance_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (text.Text.Length > 0)
                {
                    this.Close();
                    try
                    {
                        //MyBus.Pick(int.Parse(text.Text));
                        int km = int.Parse(text.Text);
                        MyBus.IsReadyToPick(km);


                        MyBus.BusState = State.BUSY;
                        MyBus.setBusStateColor();

                        Thread thread = null;
                        thread = new Thread(() =>
                        {
                            for (int i = (km / r.Next(20, 50) * 6); i > 0; i--)
                            {
                                Thread.Sleep(1000);
                                this.Dispatcher.Invoke(() =>
                                {
                                    MyBus.BusStateString = String.Format("Ready in {0}", i.ToString());
                                });
                            }
                            MyBus.BusState = State.READY;
                            MyBus.setDrivingValues(km);
                            MyBus.setBusStateColor();
                        });
                        thread.Start();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                e.Handled = true;
                return;
            }
            if (e.Key == Key.Escape || e.Key == Key.Tab || e.Key == Key.Back
                || e.Key == Key.Delete || e.Key == Key.CapsLock ||
                e.Key == Key.LeftShift || e.Key == Key.RightShift ||
                e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl ||
                e.Key == Key.LeftAlt || e.Key == Key.RightAlt ||
                e.Key == Key.LWin || e.Key == Key.RWin || e.Key == Key.System ||
                e.Key == Key.Left || e.Key == Key.Down ||
                e.Key == Key.Up || e.Key == Key.Right) return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
            {
                if (!(Keyboard.IsKeyDown(Key.LeftShift)) || (Keyboard.IsKeyDown(Key.RightShift)) ||
                    (Keyboard.IsKeyDown(Key.LeftCtrl)) || (Keyboard.IsKeyDown(Key.RightCtrl)) ||
                    (Keyboard.IsKeyDown(Key.LeftAlt)) || (Keyboard.IsKeyDown(Key.RightAlt))) return;
            }
            e.Handled = true;
            MessageBox.Show("Only number are allowed", "Distance", MessageBoxButton.OKCancel);
        }
    }
}
