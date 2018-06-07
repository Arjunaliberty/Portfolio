using GisMeteoLibrary.Core.Concrete;
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

        private List<WeatherStartInfo> startData;
        private List<WeatherItemInfo> itemData;
        
        public ConnectGisServis() {
            this.startData = new List<WeatherStartInfo>();
            this.itemData = new List<WeatherItemInfo>();
        }

        private void RunGisStartPage()
        {
            GisResource gisResource = new GisResource(new GisSettings("http://www.gismeteo.ru"));
            PageResultGisStart pageResultGisStart = new PageResultGisStart();

            startData = pageResultGisStart.GetResult(gisResource, pattern1);

            foreach (var item in startData)
            {
                Console.WriteLine("{0}   {1}", item.City, item.Link);
            }
        }

        private void RunGisCityPage()
        {
            foreach (var data in startData)
            {
                string deb = "http://www.gismeteo.ru" + data.Link;


                GisResource gisResource = new GisResource(new GisSettings("http://www.gismeteo.ru" + data.Link));
                PageResultGisItem pageResultGisItem = new PageResultGisItem();

                WeatherItemInfo info = pageResultGisItem.GetResult(gisResource, pattern2);

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
