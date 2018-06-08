using GisMeteoLibrary.Core.Concrete;
using GisMeteoLibrary.Core.DatabaseContext;
using GisMeteoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core
{
    public class ConnectGisServis
    {
        const string pattern1 = @"href=""(/weather.+?[0-9]+?/)"".+? data-name=""(\w+?)""";
        const string pattern2 = @"<a.+?tomorrow.+? data-text=""([А-я, ]+)""(?:.\r?\n?)+?<span class="".+?"">(.+?)</span>(?:.\r?\n?)+?<div class='.+?'[^>]+>([^<]+?)</div><div class='.+?'[^>]+>([^<]+?)</div>(?:.\r?\n?)+?(?:<div class="".+?"">([^<]+?)</div>)?</div></a>";

        private List<Info> startData;
        private List<Weather> itemData;
        
        public ConnectGisServis() {
            this.startData = new List<Info>();
            this.itemData = new List<Weather>();
        }

        private void RunGisStartPage()
        {
            GisResource gisResource = new GisResource(new GisSettings("http://www.gismeteo.ru"));
            PageResultGisStart pageResultGisStart = new PageResultGisStart();

            startData = pageResultGisStart.GetResult(gisResource, pattern1);

            MySqlContextGisStart context = new MySqlContextGisStart(new MySqlGetConnect(new MySqlSettings()));
            context.Insert(startData);
        }

        private void RunGisCityPage()
        {
            foreach (var data in startData)
            {
                GisResource gisResource = new GisResource(new GisSettings("http://www.gismeteo.ru" + data.Link));
                PageResultGisItem pageResultGisItem = new PageResultGisItem();

                Weather info = pageResultGisItem.GetResult(gisResource, pattern2);

                if (!itemData.Contains(info)) itemData.Add(info);

            }

            foreach (var item in itemData)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", item.WeatherCondition, item.Date, item.TemperatureMin, item.TemperatureMax, item.Precipitation);
            }
        }

        public void Run()
        {
            RunGisStartPage();
            RunGisCityPage();

            Console.ReadLine();
        }
    }
}
