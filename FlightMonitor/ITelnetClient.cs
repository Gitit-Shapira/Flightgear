using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightMonitor
{
    interface ITelnetClient
    {
        void connect(string ip, int port);
        void write(string lineCSV);
        void disconnect();
    }
}
