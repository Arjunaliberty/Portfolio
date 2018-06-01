using System;
using System.Collections.Generic;
using GisMeteoLibrary.Core.Concrete;

namespace GisMeteoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> coll = new List<string>();

            GetGlobalResource resource = new GetGlobalResource(new ResourceSettings("http://www.gismeteo.ru/"));
            GetGlobalResult result = new GetGlobalResult(resource, @"/weather-[a-z]+-[0-9]+/");

            coll = result.GetResult();

            foreach (var item in coll)
            {
                Console.WriteLine(item);
            }


            Console.ReadLine();
        }
    }
}
