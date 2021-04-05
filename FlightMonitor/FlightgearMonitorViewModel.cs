using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get { return model.LineCSV; }
            set
            {
                model.LineCSV = value;
                NotifyPropertyChanged("LineCSV");
            }
        }

        public void Play()
        {
            model.Stop = false;
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
    }
}

