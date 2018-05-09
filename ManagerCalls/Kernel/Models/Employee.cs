using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Models
{  
    /// <summary>
    /// Структура для хранения таблицы Employees
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public Role Role { get; set; }
        /// <summary>
        /// Навигационное свойство для ссылки на таблицу CallLogs (one to many )
        /// </summary>
        public List<CallLog> CallLogs { get; set; }

        public Employee()
        {
            this.CallLogs = new List<CallLog>();
        }
    }
}
