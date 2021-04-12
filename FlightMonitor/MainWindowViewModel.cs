using System;
using System.ComponentModel;


namespace FlightMonitor
{
    class MainWindowViewModel : ViewModel
    {

        public MainWindowViewModel(IFlightgearMonitorModel model) : base(model) { }
        
        // Properties
        public string VM_Path
        {
            get { return model.Path; }
            set
            {
                model.Path = value;
                NotifyPropertyChanged("Path");
            }

        }

        public string VM_XML
        {
            get { return model.XMLPath; }
            set
            {
                model.XMLPath = value;
                NotifyPropertyChanged("XMLPath");
            }
        }
    }
}