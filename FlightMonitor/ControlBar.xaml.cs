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
using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for ControlBar.xaml
    /// </summary>
    public partial class ControlBar : UserControl
    {
        private FlightgearMonitorViewModel VM;
        public ControlBar()
        {
            InitializeComponent();
            VM = new FlightgearMonitorViewModel(new MyFlightgearMonitorModel(new MyTelnetClient()));
            DataContext = VM;
        }
        private void Button_Play(object sender, RoutedEventArgs e)
        {
            VM.Play();
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            VM.Pause();
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            VM.Stop();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
