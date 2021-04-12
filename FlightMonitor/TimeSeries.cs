using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FlightMonitor
{
    class TimeSeries
    {
        private List<KeyValuePair<string, List<float>>> table;
        public int NumOfColumns => table.Count;
        public int NumOfRows => table[0].Value.Count;
        public TimeSeries(List<string> columnNames)
        {
            table = new List<KeyValuePair<string, List<float>>>();
            foreach (string s in columnNames)
            {
                table.Add(new KeyValuePair<string, List<float>>(s, new List<float>()));
            }
        }
        public List<string> GetColumnNames()
        {
            List<string> columns = new List<string>();
            foreach (var curr in table)
            {
                columns.Add(curr.Key);
            }
            return columns;
        }
        public void AddRow(string line)
        {
            string[] sVals = line.Split(',');
            Debug.Assert(sVals.Length == NumOfColumns, "TimeSeries AddRow: Expected " + NumOfColumns + ", got " + sVals.Length);
            for (int i = 0; i < NumOfColumns; i++)
            {
                {
                    table[i].Value.Add(float.Parse(sVals[i]));
                }
            }
        }
        public float FindValue(string name, int row)
        {
            return GetColumn(name)[row<NumOfRows?row:NumOfRows-1];
        }
        public List<float> GetColumn(string name)
        {
            return table.Find(x => x.Key.Equals(name)).Value;
        }
        public List<float> GetColumn(int index)
        {
            return table[index].Value;
        }

        public string GetColumnName(int index)
        {
            return table[index].Key;
        }
        public List<float> GetRow(int i)
        {
            List<float> l = new List<float>();
            foreach (var kvp in table)
            {
                l.Add(kvp.Value[i]);
            }
            return l;
        }
    }
}
