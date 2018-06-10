using GisMeteoLibrary.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core.Abstract
{
    public interface IMySqlContext<T>
    {
        T GetItem(int id, SelectTable selectTable);
        List<T> GetItems(SelectTable selectTable);
        void Insert(T param, SelectTable selectTable);
        void Update(T param, SelectTable selectTable);
        void Delete(T param);
    }
}
