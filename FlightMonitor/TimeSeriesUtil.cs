using OxyPlot;
using System.Collections.Generic;
using System.Linq;

namespace FlightMonitor
{
    class TimeSeriesUtil
    {
        private TimeSeriesUtil() { }
        static public List<DataPoint> ColumnToDataPoints(List<float> column, int line)
        {
            List<DataPoint> l = new List<DataPoint>();

            for (int i = 0; i < line; i++)
            {
                l.Add(new DataPoint(i, column[i]));
            }
            return l;
        }
        
        static public float AVG(List<float> clm)
        {
            return clm.Sum() / clm.Count;
        }
        static public float COAVG(List<float> c1, List<float> c2)
        {
            float coavg = 0;
            for(int i = 0;i<c1.Count; i++)
            {
                coavg += c1[i] * c2[i];
            }
            coavg /= c1.Count;
            return coavg;
        }
        static public float VAR(List<float> clm)
        {
            float var = 0;
            float avg = AVG(clm);
            for(int i = 0; i<clm.Count; i++)
            {
                var += (clm[i] - avg) * (clm[i] - avg);
            }
            var /= clm.Count;
            return var;
        }
        static public float COV(List<float> c1, List<float> c2)
        {
            float cov = COAVG(c1, c2) - AVG(c1) * AVG(c2);
            return cov;
        }
        static public float Pearson(List<float> c1, List<float> c2)
        {
            double p = COV(c1, c2) / (System.Math.Sqrt(VAR(c1)) * (System.Math.Sqrt(VAR(c2))));
            return (float)p;
        }
        static public string MostCorFeatIndex(string feat, TimeSeries ts)
        {
            float maxcor = 0;
            string currMax = feat;
            float curr;
            for(int i = 0; i<ts.NumOfColumns; i++)
            {
                if (!ts.GetColumnName(i).Equals(feat))
                {
                    curr = Pearson(ts.GetColumn(currMax), ts.GetColumn(i));
                    if (maxcor < curr)
                    {
                        maxcor = curr;
                        currMax = ts.GetColumnName(i);
                    }
                }
            }
            return currMax;
        }
    }
}
