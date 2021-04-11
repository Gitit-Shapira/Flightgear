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

        public List<string> VM_ColumnNames
        {
            get
            {
                return model.ColumnNames;
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

        private float altitude_ft;

        public float Altitude_ft
        {
            get { return altitude_ft; }
            set
            {
                altitude_ft = value;
                NotifyPropertyChanged("Altitude_ft");
            }
        }


        /*   public float Altitude_ft
           {
               get { return timeS.FindValue("altimeter_indicated-altitude-ft", LineCSV); }
           }*/

        private float airspeed_kt;

        public float Airspeed_kt
        {
            get { return airspeed_kt; }
            set
            {
                airspeed_kt = value;
                NotifyPropertyChanged("Airspeed_kt");
            }
        }


        /*public float Airspeed_kt
        {
            get { return timeS.FindValue("airspeed-kt", LineCSV); }
        }*/

        private float heading;

        public float Heading
        {
            get { return heading; }
            set
            {
                heading = value;
                NotifyPropertyChanged("Heading");
            }
        }


        /* public float Heading
         {
             get { return timeS.FindValue("heading-deg", LineCSV); }
         }*/

        private float pitch;

        public float Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }


        /* public float Pitch
         {
             get { return timeS.FindValue("pitch-deg", LineCSV); }
         }*/

        private float roll;

        public float Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
        }


        /* public float Roll
         {
             get { return timeS.FindValue("roll-deg", LineCSV); }
         }*/

        private float yaw;

        public float Yaw
        {
            get { return yaw; }
            set
            {
                yaw = value;
                NotifyPropertyChanged("Yaw");
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

