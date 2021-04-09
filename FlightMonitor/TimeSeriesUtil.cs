using OxyPlot;
using System.Collections.Generic;

namespace FlightMonitor
{
    static class TimeSeriesUtil
    {
        static public List<DataPoint> ColumnToDataPoints(List<float> column, int line)
        {
            List<DataPoint> l = new List<DataPoint>();

            for (int i = 0; i < line; i++)
            {
                l.Add(new DataPoint(i, column[i]));
            }
            return l;
        }

        

    }
}
