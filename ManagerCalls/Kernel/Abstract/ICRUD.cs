using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Abstract
{
    /// <summary>
    /// Интерфейс для реализации CRUD к БД
    /// </summary>
    public interface ICRUD
    {
        TEntity Get<TEntity>(int id);
        TEntity GetAll<TEntity>();
        void Update<TEntity>(TEntity entity);
        void Delete<TEntity>(TEntity entity);
    }
}
