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
        public static string hostName;
        public static string name;
        public static int portServerClient = 8080;
        public static int portClientServer = 8888;
        
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
            
            //Читаем ip хоста
            IPAddress[] hostIpList;
            try
            {
                using (var sr = new StreamReader("nameOp.txt", Encoding.UTF8))
                {
                    hostName = sr.ReadToEnd();
                    
                    if (hostName.Length == 0)
                    {
                        Console.WriteLine("Файл пуст");
                        return;
                    }

                    hostIpList = Dns.GetHostAddresses(hostName);
                }

                foreach (var item in hostIpList)
                {
                    if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        host = item.ToString();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл с именем компа оппонента не найден.\n" +
                    "Создайте файл nameOp.txt рядом с программой " +
                    "и впишите в него имя компа оппонента");
                return;
            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("Комп оппонента не в сети");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Консольный чатик");
            Console.ResetColor();

            Console.Write("Ваш ник: ");
            name = Console.ReadLine();
            
            //Запускаем поток принятия сообщений
            listen.Start();

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
