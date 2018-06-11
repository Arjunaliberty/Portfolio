namespace GisMeteoLibrary.Core.Abstract
{
    /// <summary>
    /// Интерфейс описывабщий получения данных их ресурса
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResult<T>
    {
       T GetResult(IResource resource, string pattern);    
    }
}
