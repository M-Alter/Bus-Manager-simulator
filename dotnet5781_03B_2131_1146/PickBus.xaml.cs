using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        /// <summary>
        /// event hanlder when a key is pressed down, it checks a if the key pressed is a number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Distance_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //if the key pressed is enter
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (text.Text.Length > 0)
                {
                    this.Close();
                    try
                    {
                        int km = int.Parse(text.Text);
                        //make sure 
                        MyBus.IsReadyToPick(km);
                        MyBus.BusState = State.BUSY;
                        MyBus.setBusStateColor();
                        //thread that puts the bus to sleep whilst the ride is going
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
                            MyBus.setDrivingValues(km);
                            MyBus.setBusState();
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
            //if the pressed key is a function key then ignore the event
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
