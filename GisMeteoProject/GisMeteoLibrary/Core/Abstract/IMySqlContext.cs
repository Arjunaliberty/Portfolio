using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core.Abstract
{
    public interface IMySqlContextd<T>
    {
        T GetItem(int id);
        List<T> GetItems();
        void Insert(List<T> param);
        void Update(List<T> param);
        void Delete(T param);
    }
}
