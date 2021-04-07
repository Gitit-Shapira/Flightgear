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
            vm = new MainWindowViewModel(model);
        }

        private void Fly_default_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Debug.WriteLine(openFileDialog.FileName);
                vm.VM_Path = openFileDialog.FileName;
            }

        }

        private void ControlBar_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}