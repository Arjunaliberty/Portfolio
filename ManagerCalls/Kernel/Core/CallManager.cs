using Kernel.Context;
using Kernel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Core
{
    /// <summary>
    /// Класс для работы с журналом звонков
    /// </summary>
    public class CallManager
    {

        public CallManager()
        {

        }
        /// <summary>
        /// Метод для добавления в записи журнал звонков
        /// </summary>
        /// <param name="call"></param>
        public static void Add(CallLog call)
        {
            if (call == null) throw new Exception("Ссылка на объект не указывает на экземпляр объекта");
            if (call.Id != 0) throw new Exception("Ошибка при инициализации экземпляра класса");
            InsertEntity.Insert(call);
           
        }
        /// <summary>
        /// Метод для обновления записи в журнале звонков
        /// </summary>
        /// <param name="call"></param>
        public static void Update(CallLog call)
        {
            if (call == null) throw new Exception("Ссылка на объект не указывает на экземпляр объекта");
            InsertEntity.Update(call);
        }
    }
}
