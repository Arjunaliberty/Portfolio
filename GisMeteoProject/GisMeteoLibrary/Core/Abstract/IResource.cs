namespace GisMeteoLibrary.Core.Abstract
{
    /// <summary>
    /// Работа с ресурсом
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IResource
    {
        string Load();
    }
}
