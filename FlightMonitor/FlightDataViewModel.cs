using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightMonitor
{
    class FlightDataViewModel : ViewModel
    {
        public FlightDataViewModel(IFlightgearMonitorModel model) : base(model) { }
        public float VM_Altitude_ft
        {
            get { return model.Altitude_ft; }
        }

        public float VM_Airspeed_kt
        {
            get { return model.Airspeed_kt; }
        }

        public float VM_Heading
        {
            get { return model.Heading; }
        }

        public float VM_Pitch
        {
            get { return model.Pitch; }
        }

        public float VM_Roll
        {
            get { return model.Roll; }
        }

        public float VM_Yaw
        {
            get { return model.Yaw; }
        }
    }
}