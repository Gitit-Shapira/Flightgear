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
using System.Threading;
namespace FlightMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyFlightgearMonitorModel model;
        public MainWindow()
        {
            InitializeComponent();
            main_window.Show();
            model = new MyFlightgearMonitorModel(new MyTelnetClient());
            Thread thr = new Thread(letsBegin);
            thr.Start();
            
        }

        private void letsBegin()
        {
            model.connect("localhost", 5400);
        }

        private void Fly_default_click(object sender, RoutedEventArgs e)
        {
            //VM.execute();
        }

        private void ControlBar_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}