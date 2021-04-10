using System;
using System.ComponentModel;
namespace FlightMonitor
{
    public interface IFlightgearMonitorModel : INotifyPropertyChanged
    {
        // connection to the Flightgear
        void connect(string ip, int port);
        void disconnect();
        void start();

        //properties
        int LineCSV { get; set; }
        int Speed { get; set; }
        Boolean Stop { get; set; }
        int LengthCSV { get; }
        string Path { get; set; }
        Boolean IsPathInput { get; }
        double Rudder { get; set; }
        double Aileron { get; set; }
        double Elevator { get; set; }
        double Throttle { get; set; }
        double Y{ get; set; }
        double X{ get; set; }

    }
}
