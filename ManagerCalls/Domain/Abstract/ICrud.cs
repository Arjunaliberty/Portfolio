using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface ICrud<TItem>
    {
        TItem Get(int id);
        IEnumerable<TItem> GetAll();
        void Add(TItem item);
        void Update(TItem item);
        void Delete(TItem item);
    }
}
