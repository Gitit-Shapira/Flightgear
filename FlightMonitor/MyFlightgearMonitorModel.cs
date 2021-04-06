﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Xml.Linq;
using System.Diagnostics;

namespace FlightMonitor
{
    public class MyFlightgearMonitorModel : IFlightgearMonitorModel
    {
        //fields
        System.IO.StreamReader file;
        string line;
        TimeSeries timeS;
        private volatile int lineCSV;
        private int speed;


        //INotifyPropertyChanged implementation:
        public event PropertyChangedEventHandler PropertyChanged;

        // the properties implementation
        public int LineCSV
        {
            get { return lineCSV; }
            set
            {
                this.lineCSV = value;
                NotifyPropertyChanged("LineCSV");
            }

        }
        public int Speed
        {
            get { return speed; }
            set
            {
                this.speed = value;
                NotifyPropertyChanged("Speed");
            }

        }

        public Boolean Stop
        {
            get { return stop; }
            set
            {
                this.stop = value;
                NotifyPropertyChanged("Stop");
            }
        }

        public int LengthCSV
        {
            get { return timeS.NumOfRows; }
        }
        //the methods

        ITelnetClient telnetClient;
        volatile Boolean stop;

        //constractor
        public MyFlightgearMonitorModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.lineCSV = 1;
            stop = false;
            this.speed = 30;
            
        }
        public void connect(string ip, int port)
        {
            telnetClient.connect(ip, port);
            string path = @"C:\Users\Roey\Documents\GitHub\Flightgear\reg_flight.csv";
            this.file = new System.IO.StreamReader(path);

            XElement Xelement = XElement.Load(@"C:\Program Files\FlightGear 2020.3.6\data\Protocol\playback_small.xml");
            XDocument xDoc = XDocument.Load(@"C:\Program Files\FlightGear 2020.3.6\data\Protocol\playback_small.xml");
            IEnumerable<XElement> query = Xelement.Descendants("output").Descendants("name");
            List<string> names = new List<string>();
            foreach (var name in query)
            {
                names.Add(name.Value);
            }

            this.timeS = new TimeSeries(names);

            while ((this.line = this.file.ReadLine()) != null)
            {
                timeS.AddRow(line);
            }
            start();
        }
        public void disconnect()
        {
            stop = true;
            file.Close();
            telnetClient.disconnect();
        }

        public void start()
        {
            Thread t = new Thread(run);
            t.Start();
        }

        public void run()
        {
            while (!stop)
            {

                if (lineCSV >= timeS.NumOfRows)
                {
                    this.stop = true;
                }
                else
                {
                    //Debug.WriteLine(lineCSV);
                    telnetClient.write(string.Join(",", timeS.GetRow(lineCSV).ToArray()));
                    LineCSV++;
                    Thread.Sleep(1000 / this.speed);
                }
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
