using System;
using System.Net;
using System.Threading;
using System.IO;
using System.Text;

namespace Messanger_for_console
{
    /// <summary>
    /// Подключение можно использовать 1 раз
    /// Вычислять ip лучше в функции
    /// </summary>

    class Program
    {
        public static string YourIp;
        public static string host;
        public static string name;
        public static int port = 8080;
        
        static void Main(string[] args)
        {
            Thread listen = new Thread(new ThreadStart(Server.RecieveMessage));
            
            //Читаем наш ip
            IPAddress[] yourIpList = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var item in yourIpList)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    YourIp = item.ToString();
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Консольный чатик");
            Console.ResetColor();

            Console.Write($"Ваш ip: {YourIp}\nIP оппонента: ");
            host = Console.ReadLine();

            //Запускаем поток принятия сообщений
            listen.Start();

            Console.Write("Ваш ник: ");
            name = Console.ReadLine();
            
            string nameMess = "подключился";
            Client.SendMessage(nameMess);

            //Цикл с отправкой сообщений
            while (true)
            {
                Console.Write("Введите сообщение: ");
                string message = Console.ReadLine();
                Client.SendMessage(message);
            }
        }
    }
}
