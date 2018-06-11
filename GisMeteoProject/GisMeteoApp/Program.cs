using System;
using System.Threading;
using GisMeteoLibrary.Core;

namespace GisMeteoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сервис GisMeteo запущен");

            Timer timer = new Timer(Callback, null, 0, 600000);

            Console.ReadLine();
            timer.Dispose();
        }

        private static void Callback(Object obj)
        {
            RunServis connect = new RunServis();
            connect.Run();
            Console.WriteLine("Сервис обновлен {0}", DateTime.Now.ToString());
        }
    }
}
