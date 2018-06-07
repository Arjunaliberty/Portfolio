using GisMeteoLibrary.Core.Abstract;
using System;
using xNet;

namespace GisMeteoLibrary.Core.Concrete
{
    /// <summary>
    /// Реализует интерфейс IResource<T> для работый с http://www.gismeteo.ru/
    /// </summary>
    public class GisResource : IResource
    {
        private ISettings settings;
        public GisResource(ISettings settings)
        {
            this.settings = settings;
        }

        public string Load()
        {
            string result = null;

            try
            {
                using (var request = new HttpRequest())
                {
                    HttpResponse response = request.Get(settings.Url);
                    result = response.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
