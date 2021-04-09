using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace FlightMonitor
{
    class FlightgearMonitorViewModel : INotifyPropertyChanged
    {
        private IFlightgearMonitorModel model;
        public FlightgearMonitorViewModel(IFlightgearMonitorModel model)
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
        public int VM_Speed
        {
            get { return model.Speed; }
            set
            {
                model.Speed = value;
                NotifyPropertyChanged("Speed");
            }

        }
        public int VM_LineCSV
        {
            get { return model.LineCSV - 1; }
            set
            {
                model.LineCSV = value;
                NotifyPropertyChanged("LineCSV");
            }
        }

        public int VM_LengthCSV
        {
            get { return model.LengthCSV; }
        }

        public Boolean VM_FilesInput
        {
            get
            {
                return model.FilesInput;
            }
        }
        public void Play()
        {
            if (model.Stop)
            {
                model.Stop = false;
                model.start();
            }
        }

        public void Pause()
        {
            model.Stop = true;
        }

        public void Stop()
        {
            model.Stop = true;
            model.LineCSV = 0;
            //Atom
        }

        public List<string> ColumnNames()
        {
            return model.ColumnNames();
        }
    }
}

