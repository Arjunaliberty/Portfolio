using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Models
{
    /// <summary>
    /// Структура для хранения таблицы TaskLogs
    /// </summary>
    public class TaskLog
    {
        public int Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        /// <summary>
        /// Навигационное свойство для ссылки на таблицу Services (one to one )
        /// </summary>
        public Service Service { get; set; }
        public DateTime? HideExDate { get; set; }
        public int Count { get; set; }
    }
}
