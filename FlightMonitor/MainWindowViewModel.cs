using System;
using System.ComponentModel;


namespace FlightMonitor
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private IFlightgearMonitorModel model;
        public MainWindowViewModel(IFlightgearMonitorModel model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

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
    }
}