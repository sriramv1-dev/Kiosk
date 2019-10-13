using System;
using System.Windows;

namespace Kiosk.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchForBooks_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                BookView bookView = new BookView();
                bookView.ShowDialog();
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }

        }
    }
}
