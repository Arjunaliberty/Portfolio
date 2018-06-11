using GisMeteoLibrary.Core.Database;
using System.Collections.Generic;

namespace GisMeteoLibrary.Core.Abstract
{
    /// <summary>
    /// Интерфейс описывабщий контест БД
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMySqlContext<T>
    {
        T GetItem(int id, SelectTable selectTable);
        List<T> GetItems(SelectTable selectTable);
        void Insert(T param, SelectTable selectTable);
        void Update(T param, SelectTable selectTable);
        void Delete(T param);
    }
}
