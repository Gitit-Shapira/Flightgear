using System.Diagnostics;
using System.Net.Sockets;

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
            Debug.WriteLine("CONNECTED");
        }
        public void write(string lineCSV)
        {
            lineCSV += "\r\n";
            //Debug.Write(lineCSV,"cat");
            ns.Write(System.Text.Encoding.ASCII.GetBytes(lineCSV), 0, lineCSV.Length);
            ns.Flush();
        }
        public void disconnect()
        {
            ns.Close();
            client.Close();
        }
    }
}
