using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GisMeteoLibrary.Core.Abstract;

namespace GisMeteoLibrary.Core.Concrete
{
    public class GetGlobalResult : IResult<string>
    {
        private GetGlobalResource Resource;
        private string Pattern;

        public GetGlobalResult(GetGlobalResource resource, string pattern )
        {
            this.Resource = resource;
            this.Pattern = pattern;
        }

        public List<string> GetResult()
        {
            List<string> result = null;

            Regex regex = new Regex(Pattern);
            MatchCollection collection = regex.Matches(Resource.Load());

            if (collection.Count > 0)
            {
                result = new List<string>();

                foreach (Match item in collection)
                {
                    if( !result.Contains(item.Value) ) result.Add(item.Value);
                }
            }
            else
            {
                throw new Exception("Данные не найдены");
            }

            return result;
        }
    }
}
