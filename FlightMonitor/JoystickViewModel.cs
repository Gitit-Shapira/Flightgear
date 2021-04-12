using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace FlightMonitor
{
    class JoystickViewModel : ViewModel
    {

        public JoystickViewModel(IFlightgearMonitorModel model) : base(model) { }
        // Properties
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
    }
}

