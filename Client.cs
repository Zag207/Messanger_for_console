using System;
using System.Net.Sockets;
using System.Text;

namespace Messanger_for_console
{
    class Client
    {
        static NetworkStream stream;
        static TcpClient client;
        
        public static void SendMessage(string mess)
        {
            try
            {
                client = new TcpClient(Program.host, Program.port);
                
                string message = Program.name + ": " + mess;
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream = client.GetStream();
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }
    }
}
