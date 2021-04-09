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

namespace FlightMonitor
{
    /// <summary>
    /// Interaction logic for GraphsView.xaml
    /// </summary>
    public partial class GraphsView : UserControl
    {

        private FlightgearMonitorViewModel VM;
        public GraphsView()
        {
            InitializeComponent();

        }

        public void HookVM(IFlightgearMonitorModel model)
        {
            VM = new FlightgearMonitorViewModel(model);
            DataContext = VM;
        }
        private void ComboBox(object sender, RoutedEventArgs e)
        {
            foreach (string s in VM.ColumnNames())
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = s;
                comboBox.Items.Add(newItem);
            }
        }
    }
}
