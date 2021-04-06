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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            ControlBar1.HookVM(model);
            // ControlBar control_bar = new ControlBar();
            //control_bar.HookVM(model);
            Thread thr = new Thread(letsBegin);
            thr.Start();
            
           /* main_window.Show();
            Control control_bar = new Control();*/
        }

        private void letsBegin()
        { 
            model.connect("localhost", 5400);
            model.start();
            model.disconnect();
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