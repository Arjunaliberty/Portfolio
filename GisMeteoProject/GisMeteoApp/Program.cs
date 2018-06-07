using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using GisMeteoLibrary.Core;
using GisMeteoLibrary.Core.Concrete;
using GisMeteoLibrary.Models;

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
            ConnectGisServis connect = new ConnectGisServis();
            connect.Run();
        }
    }
}
