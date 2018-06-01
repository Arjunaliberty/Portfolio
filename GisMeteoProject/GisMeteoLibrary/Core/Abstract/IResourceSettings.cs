using System.Collections.Generic;

namespace GisMeteoLibrary.Core.Abstract
{
    /// <summary>
    /// Настройки подключения к ресурсу
    /// </summary>
    interface IResourceSettings
    {
        string BaseUrl { get; }
        List<string> Params { get; }
    }
}
