using System.Collections.Generic;
using GisMeteoLibrary.Core.Abstract;

namespace GisMeteoLibrary.Core.Concrete
{
    /// <summary>
    /// Взаимодействие с ресурсом
    /// </summary>
    public class ResourceSettings : IResourceSettings
    {
        #region Params
        private readonly string baseUrl;
        private readonly List<string> param;
        #endregion
        public ResourceSettings()
        {
            this.baseUrl = null;
            this.param = null;
        }
        public ResourceSettings(string baseUrl)
        {
            this.baseUrl = baseUrl;
            this.param = null;
        }
        public ResourceSettings(string baseUrl, List<string> param)
        {
            this.baseUrl = baseUrl;
            this.param = new List<string>();
            this.param.AddRange(param);
        }

        public string BaseUrl
        {
            get
            {
                return this.baseUrl;
            }
        }
        public List<string> Params
        {
            get
            {
                return this.param;
            }
        }
    }
}
