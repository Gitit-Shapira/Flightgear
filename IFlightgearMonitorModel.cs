using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace FlightMonitor
{
    interface IFlightgearMonitorModel : INotifyPropertyChanged
    {
        // connection to the Flightgear
        void connect(string ip, int port);
        void disconnect();
        void start();

        //properties
        int LineCSV { get; set; }
        int Speed { get; set; }
        Boolean Stop { get; set; }
    }
}
