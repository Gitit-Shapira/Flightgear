using System;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;
namespace FlightMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IFlightgearMonitorModel model;
        MainWindowViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            main_window.Show();
            model = new MyFlightgearMonitorModel(new MyTelnetClient());
            ControlBar1.HookVM(model);
            //FlightDetails1.HookVM(model);
            vm = new MainWindowViewModel(model);
            this.FlightDetails1.HookVM(model);
        }

        private object FlightgearMonitorViewModel(IFlightgearMonitorModel model)
        {
            throw new NotImplementedException();
        }

        private void Fly_default_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV file (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                Debug.WriteLine(openFileDialog.FileName);
                vm.VM_Path = openFileDialog.FileName;
            }

        }

        private void ControlBar_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void FlightDetails1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}