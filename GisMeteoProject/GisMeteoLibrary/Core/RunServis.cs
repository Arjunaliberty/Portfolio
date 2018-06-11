using GisMeteoLibrary.Core.Concrete;
using GisMeteoLibrary.Core.Database;
using GisMeteoLibrary.Core.DatabaseContext;
using GisMeteoLibrary.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace GisMeteoLibrary.Core
{
    public class RunServis                                                                                                     
    {
        const string pattern1 = @"href=""(/weather.+?[0-9]+?/)"".+? data-name=""(\w+?)""";
        const string pattern2 = @"<a.+?tomorrow.+? data-text=""([А-я, ]+)""(?:.\r?\n?)+?<span class="".+?"">(.+?)</span>(?:.\r?\n?)+?<div class='.+?'[^>]+>([^<]+?)</div><div class='.+?'[^>]+>([^<]+?)</div>(?:.\r?\n?)+?(?:<div class="".+?"">([^<]+?)</div>)?</div></a>";

        private List<Info> infoData;
        private List<Weather> weatherData;
        
        public RunServis() {
            this.infoData = new List<Info>();
            this.weatherData = new List<Weather>();
        }

        private void RunInfo()
        {
            GisResource gisResource = new GisResource(new GisSettings("http://www.gismeteo.ru"));
            ResultGisInfo pageResultGisStart = new ResultGisInfo();


            infoData = pageResultGisStart.GetResult(gisResource, pattern1);

            MySqlConnection connection = new MySqlGetConnection(new MySqlSettings()).GetConnection();
            MySqlContext context = new MySqlContext(connection);

           
        }

        private void RunWeather()
        {
            foreach (var data in infoData)
            {
                GisResource gisResource = new GisResource(new GisSettings("http://www.gismeteo.ru" + data.Link));
                ResultGisWeather pageResultGisItem = new ResultGisWeather();

                Weather info = pageResultGisItem.GetResult(gisResource, pattern2);

                if (!weatherData.Contains(info)) weatherData.Add(info);

            }


        }

        public void Run()
        {
            RunInfo();
            RunWeather();

            Console.ReadLine();
        }
    }
}
