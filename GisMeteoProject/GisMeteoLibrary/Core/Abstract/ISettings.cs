namespace GisMeteoLibrary.Core.Abstract
{
    /// <summary>
    /// Описывает настройки для подключения к ресурсу
    /// </summary>
    public interface ISettings
    {
        string Url { get; }
    }
}
