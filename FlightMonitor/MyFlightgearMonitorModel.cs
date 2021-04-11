using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private double x, y, aileron, elevator, rudder, throttle;
        //INotifyPropertyChanged implementation:
        public event PropertyChangedEventHandler PropertyChanged;
        string selection;
        List<DataPoint> selFeatDataPoints;
        ITelnetClient telnetClient;
        volatile Boolean stop;

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
                if (timeS != null)
                    return timeS.GetColumnNames();
                return new List<string>();
            }
        }

        public double Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                this.rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public double Aileron
        {
            get
            {
                return aileron;
            }
            set
            {
                this.aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }
        public double Elevator
        {
            get
            {
                return elevator;
            }
            set
            {
                this.elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }
        public double Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                this.throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }

        public double X
        {
            get { return x; }
            set
            {
                this.x = value;
                NotifyPropertyChanged("X");
            }
        }
        public double Y
        {
            get { return y; }
            set
            {
                this.y = value;
                NotifyPropertyChanged("Y");
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

        public string Selection
        {
            set
            {
                selection = value;
                SelFeatDataPoints = TimeSeriesUtil.ColumnToDataPoints(timeS.GetColumn(selection), LineCSV);
            }
        }
    
        public List<DataPoint> SelFeatDataPoints
        {
            set
            {
                selFeatDataPoints = value;
                //Debug.Write("update");
                NotifyPropertyChanged("SelFeatDataPoints");
            }
            get
            {
                if (selFeatDataPoints != null)
                    return selFeatDataPoints;
                return new List<DataPoint>();
            }
        }
        //the methods
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
                    stop = true;
                }
                else
                {
                    //Debug.WriteLine(lineCSV);
                    telnetClient.write(string.Join(",", timeS.GetRow(lineCSV).ToArray()));
                    Altitude_ft = timeS.FindValue("altimeter_indicated-altitude-ft", LineCSV);
                    Airspeed_kt = timeS.FindValue("airspeed-indicator_indicated-speed-kt", LineCSV);
                    Heading = timeS.FindValue("heading-deg", LineCSV);
                    Pitch = timeS.FindValue("pitch-deg", LineCSV);
                    Roll = timeS.FindValue("roll-deg", LineCSV);
                    Yaw = timeS.FindValue("side-slip-deg", LineCSV);
                    if(selFeatDataPoints != null)
                    {
                        SelFeatDataPoints = TimeSeriesUtil.ColumnToDataPoints(timeS.GetColumn(selection), LineCSV);
                     //   selFeatDataPoints.Add(new DataPoint(LineCSV, timeS.FindValue(selection, LineCSV)));
                        //SelFeatDataPoints = selFeatDataPoints;
                    }
                    update_data();
                    LineCSV++;
                    Thread.Sleep(1000 / this.speed);
                }
            }
        }

        public void update_data()
        {
            Rudder = Convert.ToDouble(timeS.FindValue("rudder", lineCSV));
            Throttle = Convert.ToDouble(timeS.FindValue("throttle", lineCSV));
            Aileron = Convert.ToDouble(timeS.FindValue("aileron", lineCSV));
            Elevator = Convert.ToDouble(timeS.FindValue("elevator", lineCSV));
            X = (Aileron * 90);
            Y = (Elevator * 90);
        }

       

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
