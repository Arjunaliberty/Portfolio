using GisMeteoLibrary.Core.Abstract;

namespace GisMeteoLibrary.Core.Concrete
{
    /// <summary>
    /// Реализует интерфейс ISettings для поключения к http://www.gismeteo.ru/ 
    /// </summary>
    public class GisSettings : ISettings
    {
        private readonly string url;
        public GisSettings(string baseUrl)
        {
            this.url = baseUrl;      
        }
        public GisSettings(string baseUrl, string parmUrl )
        {
            this.url = baseUrl + parmUrl;            
        }

        public string Url
        {
            get
            {
                return this.url;
            }
        }
    }
}
