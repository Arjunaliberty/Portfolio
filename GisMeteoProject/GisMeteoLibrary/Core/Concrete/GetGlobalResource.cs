using System;
using GisMeteoLibrary.Core.Abstract;
using xNet;

namespace GisMeteoLibrary.Core.Concrete
{
    public class GetGlobalResource : IResource
    {
        private IResourceSettings settings;
        public string FullUrl { get; }

        public GetGlobalResource(ResourceSettings settings)
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
                    var response = request.Get(settings.BaseUrl);
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
