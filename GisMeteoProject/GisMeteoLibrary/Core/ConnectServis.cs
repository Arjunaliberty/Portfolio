using GisMeteoLibrary.Core.Concrete;
using GisMeteoLibrary.Core.DatabaseContext;
using GisMeteoLibrary.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace GisMeteoLibrary.Core
{
    public class ConnectServis
    {
        const string pattern1 = @"href=""(/weather.+?[0-9]+?/)"".+? data-name=""(\w+?)""";
        const string pattern2 = @"<a.+?tomorrow.+? data-text=""([А-я, ]+)""(?:.\r?\n?)+?<span class="".+?"">(.+?)</span>(?:.\r?\n?)+?<div class='.+?'[^>]+>([^<]+?)</div><div class='.+?'[^>]+>([^<]+?)</div>(?:.\r?\n?)+?(?:<div class="".+?"">([^<]+?)</div>)?</div></a>";

        private List<Info> infoData;
        private List<Weather> weatherData;
        
        public ConnectServis() {
            this.infoData = new List<Info>();
            this.weatherData = new List<Weather>();
        }

        private void RunInfo()
        {
            GisResource gisResource = new GisResource(new GisSettings("http://www.gismeteo.ru"));
            ResultGisInfo pageResultGisStart = new ResultGisInfo();
            MySqlGetConnect connect = new MySqlGetConnect(new MySqlSettings());
            MySqlConnection connections = connect.GetConnection();
            string sqlChek = "SELECT id FROM gis_database.info WHERE `city`=@city";

            infoData = pageResultGisStart.GetResult(gisResource, pattern1);

            try
            {
                connections.Open();

                foreach (var item in infoData)
                {
                    int id = 0;
                    MySqlCommand commandCheck = new MySqlCommand(sqlChek, connections);
                    MySqlParameter parameterCity = new MySqlParameter("@city", MySqlDbType.String);
                    parameterCity.Value = item.City;
                    commandCheck.Parameters.Add(parameterCity);
                    
                    using(DbDataReader reader = commandCheck.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {
                               id = (int)reader.GetValue(0);
                            }
                        }
                    }

                    if(id != 0)
                    {
                        MySqlContextGisInfo context = new MySqlContextGisInfo(connect);
                        context.Update(item);
                    }
                    else
                    {
                        MySqlContextGisInfo context = new MySqlContextGisInfo(connect);
                        context.Insert(item);
                    }
                }
            }
            finally
            {
                connections.Close();
                connections.Dispose();
                connections = null;
            }
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

            foreach (var item in weatherData)
            {
                Console.WriteLine("{0} {1} {2} {3} {4}", item.WeatherCondition, item.Date, item.TemperatureMin, item.TemperatureMax, item.Precipitation);
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
