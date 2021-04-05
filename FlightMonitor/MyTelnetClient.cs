using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Linq;
namespace FlightMonitor
{
    class MyTelnetClient : ITelnetClient
    {
        private NetworkStream ns;

        TcpClient client;

        public void connect(string ip, int port)
        {
            client = new TcpClient(ip, port);
            ns = client.GetStream();

        }
        public void write(string lineCSV)
        {
            lineCSV += "\r\n";
            Debug.Write(lineCSV,"cat");
            ns.Write(System.Text.Encoding.ASCII.GetBytes(lineCSV),0,lineCSV.Length);
            ns.Flush();
        }

        public void disconnect()
        {
            ns.Close();
            client.Close();
        }
    }
}
