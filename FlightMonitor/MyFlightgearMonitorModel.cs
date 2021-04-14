using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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
        string selection,corFeat;
        List<DataPoint> selFeatDataPoints,corFeatDataPoints,combinedDataPoints, recentCombinedDataPoints,dlldisplay,anomalyPoints;
        ITelnetClient telnetClient;
        volatile Boolean stop;
        Dictionary<string,string> Correlations;
        DataPoint[] linRegDataPoints;
        // the properties implementation

        public int LineCSV
        {
            get { return lineCSV; }
            set
            {
                this.lineCSV = value;
                update_data();
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
                CorFeat = Correlations[selection];
                CorFeatDataPoints = TimeSeriesUtil.ColumnToDataPoints(timeS.GetColumn(corFeat), LineCSV);
                LinRegDataPoints = TimeSeriesUtil.LinReg(timeS.GetColumn(selection), timeS.GetColumn(corFeat));
                CombinedDataPoints = TimeSeriesUtil.CombineColumns(timeS.GetColumn(selection), timeS.GetColumn(corFeat));
                
                NotifyPropertyChanged("Selection");
            }
            get
            {
                return selection;
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

        public DataPoint[] LinRegDataPoints
        {
            get
            {
                if(linRegDataPoints != null)
                {
                    return linRegDataPoints;
                } else
                {
                    return new DataPoint[] { new DataPoint(0, 0), new DataPoint(0, 0) };
                }
            } set
            {
                linRegDataPoints = value;
                NotifyPropertyChanged("LinRegDataPoints");
            }
        }

        public List<DataPoint> CorFeatDataPoints
        {
            get
            {
                if (corFeatDataPoints != null)
                    return corFeatDataPoints;
                return new List<DataPoint>();
            }
            set
            {
                corFeatDataPoints = value;
                //Debug.Write("update");
                NotifyPropertyChanged("CorFeatDataPoints");
            }
        }
        public string CorFeat
        {
            get
            {
                if (corFeat != null) {
                    return corFeat;
                } else
                {
                    return "";
                }
            }
            set
            {
                corFeat = value;
                NotifyPropertyChanged("CorFeat");
            }
        }
        public List<DataPoint> CombinedDataPoints
        {
            set
            {
                combinedDataPoints = value;
                NotifyPropertyChanged("CombinedDataPoints");
            }
            get
            {
                if(combinedDataPoints!=null)
                {
                    return combinedDataPoints;
                } else
                {
                    return new List<DataPoint>();
                }
            }
        }

        public List<DataPoint> RecentCombinedDataPoints
        {
            get
            {
                if (recentCombinedDataPoints != null)
                {
                    return recentCombinedDataPoints;
                }
                else
                {
                    return new List<DataPoint>();
                }
            }
            set
            {
                recentCombinedDataPoints = value;
                NotifyPropertyChanged("RecentCombinedDataPoints");
            }
        }

        public List<DataPoint> DLLDisplay
        {
            get
            {
                if (dlldisplay != null)
                {
                    return dlldisplay;
                }
                else
                {
                    return new List<DataPoint>();
                }
            }
            set
            {
                recentCombinedDataPoints = value;
                NotifyPropertyChanged("DLLDisplay");
            }
        }
        public List<DataPoint> AnomalyPoints
        {
            get
            {
                if (anomalyPoints != null)
                {
                    return anomalyPoints;
                }
                else
                {
                    return new List<DataPoint>();
                }
            }
            set
            {
                recentCombinedDataPoints = value;
                NotifyPropertyChanged("AnomalyPoints");
            }
        }
        //the methods
        //constructor
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
                if (names.Find(x => x.Equals(name.Value)) != null)
                {
                    int i = 1;
                    while (names.Find(x => x.Equals(name.Value + "(" + i + ")")) != null)
                    {
                        i++;
                    }
                    names.Add(name.Value + "(" + i + ")");
                }
                else
                {
                    names.Add(name.Value);
                }
            }

            this.timeS = new TimeSeries(names);

            while ((this.line = this.file.ReadLine()) != null)
            {
                timeS.AddRow(line);
            }
            detectCorrelations();
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
            Task t = new Task(run);
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
                    Task thr = new Task(update_data);
                    thr.Start();
                    LineCSV++;
                    Thread.Sleep(1000 / this.speed);
                }
            }
        }

        public void update_data()
        {
            if (timeS != null)
            {
                Rudder = Convert.ToDouble(timeS.FindValue("rudder", lineCSV));
                Throttle = Convert.ToDouble(timeS.FindValue("throttle", lineCSV));
                Aileron = Convert.ToDouble(timeS.FindValue("aileron", lineCSV));
                Elevator = Convert.ToDouble(timeS.FindValue("elevator", lineCSV));
                X = (Aileron * 90);
                Y = (Elevator * 90);
                if (selFeatDataPoints != null)
                {
                    int rangeBegin = Math.Max(LineCSV - 300, 0);
                    Task t1 = new Task(() => SelFeatDataPoints = TimeSeriesUtil.ColumnToDataPoints(timeS.GetColumn(selection), LineCSV));
                    Task t2 = new Task(() => CorFeatDataPoints = TimeSeriesUtil.ColumnToDataPoints(timeS.GetColumn(corFeat), LineCSV));
                    if (CombinedDataPoints.Count != 0)
                    {
                        Task t3 = new Task(() => RecentCombinedDataPoints = CombinedDataPoints.GetRange(rangeBegin, LineCSV - rangeBegin));
                        t3.Start();
                    }
                    t1.Start();
                    t2.Start();

                }
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void detectCorrelations()
        {
            if (Correlations != null) { Correlations.Clear(); } else { Correlations = new Dictionary<string, string>(); }
            timeS.GetColumnNames().ForEach(x => { string s = TimeSeriesUtil.MostCorFeatIndex(x, timeS); Debug.WriteLine(x + " and " + s); Correlations.Add(x, TimeSeriesUtil.MostCorFeatIndex(x, timeS)); }); 
        }
    }
}