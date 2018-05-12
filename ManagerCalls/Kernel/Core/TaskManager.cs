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
    /// Класс для работы с журналом отложенных задач
    /// </summary>
    public class TaskManager
    {
        //private TaskLog TaskLog;

        public TaskManager(TaskLog taskLog)
        {
           
        }

        /// <summary>
        /// Метод для очистки истекших записей в журнале отложенных задач
        /// </summary>
        public static void Remove()
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                var tasks = context.TaskLogs;

                foreach (var task in tasks)
                {
                    if(DateTime.Now.CompareTo(task.HideExDate) > 0 && task.Count <= 0)
                    {
                        context.TaskLogs.Remove(task);
                    }
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Метод для добавления в записи журнал отложенных задач
        /// </summary>
        /// <param name="task"></param>
        public static void Add(TaskLog task)
        {
            if (task == null) throw new Exception("Ссылка на объект не указывает на экземпляр объекта");
            if (task.Id != 0) throw new Exception("Ошибка при инициализации экземпляра класса");
            InsertEntity.Insert(task);
        }

        /// <summary>
        /// Метод для обновления записи в журнале отложенных задач
        /// </summary>
        /// <param name="task"></param>
        public static void Update(TaskLog task)
        {
            if (task == null) throw new Exception("Ссылка на объект не указывает на экземпляр объекта");
            InsertEntity.Update(task);
        }







    }
}
