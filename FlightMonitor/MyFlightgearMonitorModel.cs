using System;
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
        private double x, y, aileron, elevator, rudder, throttle;

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
                    connect("localhost", 5400);
                    NotifyPropertyChanged("Path");
                    NotifyPropertyChanged("IsPathInput");
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

        public Boolean IsPathInput
        {
            get
            {
                return path != null;
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

        public static int ConvertRange(int value) 
        {
            double scale = 45;
            return (int)(80 + ((value + 1) * scale));
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
            NotifyPropertyChanged("LengthCSV");
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
                    update_data();
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

        public void update_data()
        { 
            Rudder = Convert.ToDouble(timeS.FindValue("rudder", lineCSV));
            Throttle = Convert.ToDouble(timeS.FindValue("throttle", lineCSV));
            Aileron = Convert.ToDouble(timeS.FindValue("aileron", lineCSV));
            Elevator = Convert.ToDouble(timeS.FindValue("elevator", lineCSV));
            X = (Aileron * 90);
            Y = (Elevator * 90);
            //ConvertRange(lineCSV);

        }
    }
}
