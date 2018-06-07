using GisMeteoLibrary.Core.Abstract;
using GisMeteoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GisMeteoLibrary.Core.Concrete
{
    public class PageResultGisStart : IResult<List<WeatherStartInfo>>
    {
        public PageResultGisStart() {
           
        }

        public List<WeatherStartInfo> GetResult(IResource resource, string pattern)
        {
            List<WeatherStartInfo> result = new List<WeatherStartInfo>();
          
            Regex regex = new Regex(pattern);
            MatchCollection collections = regex.Matches(resource.Load());

            if (collections.Count > 0)
            {
                foreach (Match res in collections)
                {
                    WeatherStartInfo startInfo = new WeatherStartInfo
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
