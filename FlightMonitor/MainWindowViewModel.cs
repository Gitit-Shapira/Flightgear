using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

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

        public List<string> VM_AnomalyList
        {
            get
            {
                Debug.Write("AAAAAAAAAAAAAAA");
                return model.AnomalyList;
            }
        }
        public string VM_DLL
        {
            get
            {
                return model.DLL;
            }
            set
            {
                model.DLL = value;
                NotifyPropertyChanged("DLL");
            }
        }
    }
}