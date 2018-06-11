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
        public RunServis() {}

        public void Run()
        {
            string pattern1 = @"href=""(/weather.+?[0-9]+?/)"".+? data-name=""(\w+?)""";
            string pattern2 = @"<a.+?tomorrow.+? data-text=""([А-я, ]+)""(?:.\r?\n?)+?<span class="".+?"">(.+?)</span>(?:.\r?\n?)+?<div class='.+?'[^>]+>([^<]+?)</div><div class='.+?'[^>]+>([^<]+?)</div>(?:.\r?\n?)+?(?:<div class="".+?"">([^<]+?)</div>)?</div></a>";

            GisResource resourceFirst = new GisResource(new GisSettings("http://www.gismeteo.ru/"));
            ResultGisInfo resultFirst = new ResultGisInfo();
            List<Info> infoResult = resultFirst.GetResult(resourceFirst, pattern1);

            try
            {
                MySqlConnection connection = new MySqlGetConnection(new MySqlSettings()).GetConnection();
                MySqlContext context = new MySqlContext(connection);

                List<MySqlDatabase> dataInfo = context.GetItems(SelectTable.Informations);

                foreach (var result in infoResult)
                {
                    GisResource resourceSecond = new GisResource(new GisSettings("http://www.gismeteo.ru/" + result.Link));
                    ResultGisWeather resultSecond = new ResultGisWeather();
                    Weather weatherResult = resultSecond.GetResult(resourceSecond, pattern2);

                    if(dataInfo != null)
                    {
                        foreach (var data in dataInfo)
                        {
                            if(result.City.Equals(data.Informations.City))
                            {
                                MySqlDatabase row = new MySqlDatabase
                                {
                                    Informations = new Info
                                    {
                                        Id = data.Informations.Id,
                                        City = result.City,
                                        Link = result.Link
                                    },
                                    Weathers = weatherResult
                                };

                                context.Update(row, SelectTable.All);
                            }
                            else
                            {
                                MySqlDatabase row = new MySqlDatabase
                                {
                                    Informations = result,
                                    Weathers = weatherResult
                                };
                            }
                        }
                    }
                    else
                    {
                        MySqlDatabase row = new MySqlDatabase
                        {
                            Informations = result,
                            Weathers = weatherResult
                        };

                        context.Insert(row, SelectTable.All);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                System.Environment.Exit(1);
            }
        }
    }
}
