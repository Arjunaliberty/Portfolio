namespace GisMeteoLibrary.Core.Abstract
{
    /// <summary>
    /// Интерфейс описывающий загрузку из реурса
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResource
    {
        string Load();
    }
}
