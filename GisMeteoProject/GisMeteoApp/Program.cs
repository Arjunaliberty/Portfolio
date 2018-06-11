using System;
using System.Threading;
using GisMeteoLibrary.Core;

namespace GisMeteoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(Callback, null, 0, 600000);

            Console.ReadLine();
            timer.Dispose();
        }

        private static void Callback(Object obj)
        {
            Console.Clear();
            RunServis connect = new RunServis();
            connect.Run();
        }
    }
}
