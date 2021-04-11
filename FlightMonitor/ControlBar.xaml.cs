using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        }

        public void HookVM(IFlightgearMonitorModel model)
        {
            VM = new FlightgearMonitorViewModel(model);
            DataContext = VM;
        }
        private void Button_Play(object sender, RoutedEventArgs e)
        {
            VM.Play();
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            VM.Stop();
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            VM.Pause();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private int playSpeed = 20;
        private void Selected_0_25(object sender, RoutedEventArgs e)
        {
            VM.VM_Speed = (int)(playSpeed * 0.25);
        }

        private void Selected_0_5(object sender, RoutedEventArgs e)
        {
            VM.VM_Speed = (int)(playSpeed * 0.5);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Selected_0_75(object sender, RoutedEventArgs e)
        {
            VM.VM_Speed = (int)(playSpeed * 0.75);
        }

        private void Selected_1_0(object sender, RoutedEventArgs e)
        {
            VM.VM_Speed = (int)(playSpeed * 1.0);
        }

        private void Selected_1_25(object sender, RoutedEventArgs e)
        {
            VM.VM_Speed = (int)(playSpeed * 1.25);
        }

        private void Selected_1_5(object sender, RoutedEventArgs e)
        {
            VM.VM_Speed = (int)(playSpeed * 1.5);
        }

        private void Selected_1_75(object sender, RoutedEventArgs e)
        {
            VM.VM_Speed = (int)(playSpeed * 1.75);
        }

        private void Selected_2_0(object sender, RoutedEventArgs e)
        {
            VM.VM_Speed = (int)(playSpeed * 2.0);
        }

    }
}