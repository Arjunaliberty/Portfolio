using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Models
{
    /// <summary>
    /// Структура таблицы ServiceState
    /// </summary>
    public class ServiceState
    {
        public int Id { get; set; }
        public bool Ok { get; set; }
        public bool PendingDelete { get; set; }
        public bool Delete { get; set; }
        /// <summary>
        /// Навигационное свойство на Service (one to one) 
        /// </summary>
        public Service Service { get; set; }
    }
}
