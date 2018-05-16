using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    /// <summary>
    /// Структура для хранения таблицы CallLogs
    /// </summary>
    public class CallLog
    {
        public int Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        /// <summary>
        /// Навигационное свойство для ссылки на таблицу Services (one to one )
        /// </summary>
        public Service Service { get; set; }
        public string Phone { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
        /// <summary>
        /// Навигационное свойство для ссылки на таблицу Employees (one to many )
        /// </summary>
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
