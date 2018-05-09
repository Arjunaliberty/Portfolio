using Kernel.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Core
{
    /// <summary>
    ///  Класс для добавления в базу данных
    /// </summary>
    public class InsertEntity
    {
        public static void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public static void Update<TEntity>(TEntity entity) where TEntity : class
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
