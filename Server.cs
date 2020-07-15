using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Messanger_for_console
{
    class Server
    {
        static TcpListener server;
        static NetworkStream stream;

        public static void RecieveMessage()
        {
            server = new TcpListener(IPAddress.Parse(Program.YourIp), 8080);
            server.Start();

            byte[] data = new byte[256];
            int bytes = 0;
            string respString;
            StringBuilder response = new StringBuilder();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                stream = client.GetStream();

                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                } 
                while (stream.DataAvailable);

                respString = response.ToString();

                if (respString.Length != 0)
                {
                    Console.WriteLine("\n" + respString);
                    response.Remove(0, response.Length);
                }
                stream.Close();
            }
        }
    }
}
