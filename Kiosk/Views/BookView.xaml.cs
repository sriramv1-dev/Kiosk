using Kiosk.Helpers;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Kiosk.Views
{
    /// <summary>
    /// Interaction logic for BookView.xaml
    /// </summary>
    public partial class BookView : Window
    {
        public BookView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e) {
            var inactiveTime = InactivityHelper.GetInactiveTimeInfo();
            if (inactiveTime.InactiveTime.TotalSeconds >= 60) {                
                this.Close();
                Window mainWindow = Application.Current.MainWindow;
                mainWindow.Show();
                DispatcherTimer timer = (DispatcherTimer)sender;
                timer.Stop();
            }
        }

        private void Done_Searching(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Show();                        
        }
            
    }
}
