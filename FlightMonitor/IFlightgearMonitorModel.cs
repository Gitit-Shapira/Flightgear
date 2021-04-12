using OxyPlot;
using System;
using System.Collections.Generic;
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
        string XMLPath { get; set; }
        string Path { get; set; }
        Boolean FilesInput { get; }
        List<string> ColumnNames { get; }
        float Altitude_ft { get; }
        float Airspeed_kt { get; }
        float Heading { get; }
        float Pitch { get; }
        float Roll { get; }
        float Yaw { get; }
        double Rudder { get; set; }
        double Aileron { get; set; }
        double Elevator { get; set; }
        double Throttle { get; set; }
        double Y { get; set; }
        double X { get; set; }
        string Selection { set; }
        List<DataPoint> SelFeatDataPoints { get; }
        List<DataPoint> CorFeatDataPoints { get; set; }
        string CorFeat { get; set; }
        DataPoint[] LinRegDataPoints { get; set; }
        List<DataPoint> CombinedDataPoints { get; set; }
    }
}
