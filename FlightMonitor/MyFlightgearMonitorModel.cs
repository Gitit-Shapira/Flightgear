﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Xml.Linq;

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
        string path;
        string xmlpath;
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
        public string Path
        {
            get { return path; }
            set
            {
                if (path == null || stop)
                {
                    LineCSV = 0;
                    path = value;
                    NotifyPropertyChanged("Path");
                    if (FilesInput)
                    {
                        connect("localhost", 5400);
                        NotifyPropertyChanged("FilesInput");
                    }
                }
            }
        }

        public string XMLPath
        {
            get { return xmlpath; }
            set
            {
                if (xmlpath == null || stop)
                {
                    LineCSV = 0;
                    xmlpath = value;
                    NotifyPropertyChanged("XMLPath");
                    if (FilesInput)
                    {
                        connect("localhost", 5400);
                        NotifyPropertyChanged("FilesInput");
                    }
                }
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
            get
            {
                if (timeS == null)
                    return 0;
                else return timeS.NumOfRows;
            }
        }

        public Boolean FilesInput
        {
            get
            {
                return (path != null && xmlpath != null);
            }
        }

        public List<string> ColumnNames
        {
            get
            {
                if(timeS != null)
                    return timeS.GetColumnNames();
                return new List<string>();
            }
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
            this.file = new System.IO.StreamReader(path);

            XElement Xelement = XElement.Load(xmlpath);
            XDocument xDoc = XDocument.Load(xmlpath);
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
            NotifyPropertyChanged("LengthCSV");
            NotifyPropertyChanged("IsXMLInput");
            NotifyPropertyChanged("ColumnNames");
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
                    //NotifyPropertyChanged("Altitude_ft");
                    Altitude_ft = timeS.FindValue("altimeter_indicated-altitude-ft", LineCSV);
                    Airspeed_kt = timeS.FindValue("airspeed-indicator_indicated-speed-kt", LineCSV);
                    Heading = timeS.FindValue("heading-deg", LineCSV);
                    Pitch = timeS.FindValue("pitch-deg", LineCSV);
                    Roll = timeS.FindValue("roll-deg", LineCSV);
                    Yaw = timeS.FindValue("side-slip-deg", LineCSV);
                    LineCSV++;
                    Thread.Sleep(1000 / this.speed);
                }
            }
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

/*
        public float Yaw
        {
            get { return timeS.FindValue("", LineCSV); }
        }*/

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
