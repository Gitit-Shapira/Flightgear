using System;
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

        public double VM_Y
        {
            get { return model.Y; }
            set
            {
                model.Y = value;
                NotifyPropertyChanged("Y");
            }
        }

        public double VM_X
        {
            get { return model.X; }
            set
            {
                model.X = value;
                NotifyPropertyChanged("X");
            }
        }

        public int VM_LengthCSV
        {
            get { return model.LengthCSV; }
        }

        public Boolean VM_IsPathInput
        {
            get
            {
                return model.IsPathInput;
            }
        }

        public double VM_Rudder
        {
            get
            {
                return model.Rudder;
            }
            set
            {
                model.Rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public double VM_Aileron
        {
            get
            {
                return model.Aileron;
            }
            set
            {
                model.Aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }
        public double VM_Elevator
        {
            get
            {
                return model.Elevator;
            }
            set
            {
                model.Elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }
        public double VM_Throttle
        {
            get
            {
                return model.Throttle;
            }
            set
            {
                model.Throttle = value;
                NotifyPropertyChanged("Throttle");
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
    }
}

