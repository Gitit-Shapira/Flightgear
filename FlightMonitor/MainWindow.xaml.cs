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
        public MainWindow()
        {
            InitializeComponent();
            main_window.Show();
            ControlBar control_bar = new ControlBar();
            Thread thr = new Thread(letsBegin);
            thr.Start();
            
        }

        private void letsBegin()
        {
            MyFlightgearMonitorModel letsStart;
            letsStart = new MyFlightgearMonitorModel(new MyTelnetClient());
            letsStart.connect("localhost", 5400);
            letsStart.start();
            letsStart.disconnect();
        }

        private void Fly_default_click(object sender, RoutedEventArgs e)
        {
            //VM.execute();
        }

        private void ControlBar_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    internal class PrintWriter
    {
        public PrintWriter(object outputStream)
        {
        }

        internal void close()
        {
            throw new NotImplementedException();
        }

        internal void flush()
        {
            throw new NotImplementedException();
        }

        internal void println(string line)
        {
            throw new NotImplementedException();
        }
    }
}