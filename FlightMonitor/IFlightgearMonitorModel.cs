﻿using System;
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
        Boolean IsPathInput { get; }
        float Altitude_ft { get; }
        float Airspeed_kt { get; }
        float Heading { get; }
        float Pitch { get; }
        float Roll { get; }
        float Yaw { get; }
        Boolean FilesInput { get; }
        List<string> ColumnNames { get; }
    }
}
