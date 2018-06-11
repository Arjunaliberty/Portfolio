using GisMeteoLibrary.Core.Abstract;
using GisMeteoLibrary.Models;
using System;
using System.Text.RegularExpressions;

namespace GisMeteoLibrary.Core.Concrete
{
    /// <summary>
    /// Класс реализующий интерфейс IResult для получения данных конкретного города
    /// </summary>
    public class ResultGisWeather : IResult<Weather>
    {
        public ResultGisWeather() {
            
        }

        public Weather GetResult(IResource resource, string pattern)
        {
            Weather result = null;
          
            Regex regex = new Regex(pattern);
            Match match = regex.Match(resource.Load());

            if (match.Groups.Count > 0)
            {
                result = new Weather
                {
                    WeatherCondition = match.Groups[1].Value,
                    Date = match.Groups[2].Value,
                    TemperatureMin = match.Groups[3].Value,
                    TemperatureMax = match.Groups[4].Value,
                    Precipitation = match.Groups[5].Value
                };
            } else
            {
               throw new Exception("Совпадений не найдено");
            }
            
            return result;
        }
    }
}
