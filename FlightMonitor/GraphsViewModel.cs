using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace FlightMonitor
{
    class GraphsViewModel : INotifyPropertyChanged
    {
        private IFlightgearMonitorModel model;
        public GraphsViewModel(IFlightgearMonitorModel model)
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
       
        public Boolean VM_FilesInput
        {
            get
            {
                return model.FilesInput;
            }
        }

        public List<string> VM_ColumnNames
        {
            get
            {
                return model.ColumnNames;
            }
        }

        public string VM_Selection
        {
            set
            {
                model.Selection = value;
            }
        }
        public List<DataPoint> VM_SelFeatDataPoints
        {
            get
            {
                return model.SelFeatDataPoints;
            }
        }
    }
}

