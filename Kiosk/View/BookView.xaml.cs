using Kiosk.Model;
using Kiosk.ViewModel;
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
using System.Windows.Threading;

namespace Kiosk.View
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
            var idleTime = IdleTimeDetector.GetIdleTimeInfo();
            if (idleTime.IdleTime.TotalSeconds >= 10) {
                this.Close();
                DispatcherTimer timer = (DispatcherTimer)sender;
                timer.Stop();
            }
        }

        private void Done_Searching(object sender, RoutedEventArgs e) => this.Close();
    }
}
