using OxyPlot;
using System;
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

            for (int i = 0; i < Math.Min(column.Count,line); i++)
            {
                l.Add(new DataPoint(i, column[i]));
            }
            return l;
        }
        static public List<DataPoint> CombineColumns(List<float> c1, List<float> c2)
        {
            List<DataPoint> l = new List<DataPoint>();
            for(int i = 0; i < c1.Count; i++)
            {
                l.Add(new DataPoint(c1[i], c2[i]));
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
        static public double Pearson(List<float> c1, List<float> c2)
        {
            double p = COV(c1, c2) / (Math.Sqrt(VAR(c1)) * Math.Sqrt(VAR(c2)));
            return p;
        }
        static public string MostCorFeatIndex(string feat, TimeSeries ts)
        {
            double maxcor = 0;
            string currMax = feat;
            double curr;
            for(int i = 0; i<ts.NumOfColumns; i++)
            {
                if (!ts.GetColumnName(i).Equals(feat))
                {
                    curr = Pearson(ts.GetColumn(feat), ts.GetColumn(i));
                    if (maxcor < curr)
                    {
                        maxcor = curr;
                        currMax = ts.GetColumnName(i);
                    }
                }
            }
            return currMax;
        }
        static public DataPoint[] LinReg(List<float>c1,List<float>c2)
        {
            DataPoint[] dp = new DataPoint[2];
            float a = COV(c1, c2) / VAR(c1);
            float b = AVG(c2) - a * AVG(c1);
            dp[0] = new DataPoint(c1.Min(), c1.Min()*a+ b);
            dp[1] = new DataPoint(c1.Max(), a * c1.Max() + b);
            return dp;
        }
    }
}
