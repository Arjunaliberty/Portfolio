using GisMeteoLibrary.Core.Abstract;
using GisMeteoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GisMeteoLibrary.Core.Concrete
{
    /// <summary>
    /// Класс реализующий интерфейс IResult для получения данных с главной страницы
    /// </summary>
    public class ResultGisInfo : IResult<List<Info>>
    {
        public ResultGisInfo() {}

        public List<Info> GetResult(IResource resource, string pattern)
        {
            List<Info> result = new List<Info>();
          
            Regex regex = new Regex(pattern);
            MatchCollection collections = regex.Matches(resource.Load());

            if (collections.Count > 0)
            {
                foreach (Match res in collections)
                {
                    Info startInfo = new Info
                    {
                        City = res.Groups[2].Value,
                        Link = res.Groups[1].Value
                    };

                    if (!result.Contains(startInfo)) result.Add(startInfo);
                }
            }
            else
            {
                throw new Exception("Совпадений не найдено");
            }

            return result;
        }
    }
}
