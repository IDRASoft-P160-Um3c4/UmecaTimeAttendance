using System;
using System.Net;
using System.Net.Sockets;

namespace BiometricService
{
    public class Device
    {
        public long id { get; set; }
        public string name { get; set; }
        public string ip { get; set; }
        public int port { get; set; }

        public bool IsAlive()
        {
            try
            {
                var host = new IPEndPoint(IPAddress.Parse(ip), port);

                using (var s = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp))
                {
                    s.Connect(host);
                    s.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}