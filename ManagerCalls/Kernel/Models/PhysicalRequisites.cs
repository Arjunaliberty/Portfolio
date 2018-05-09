using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Models
{
    /// <summary>
    /// Класс для хранения иформации от физическом лице
    /// </summary>
    public class PhysicalRequisites
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Series { get; set; }
        public string PassportNumber { get; set; }
        public string IssuedBy { get; set; }
        public DateTime? DateIssue { get; set; }
        public string PersonalNumber { get; set; }
        /// <summary>
        /// Навигационно свойство на таблицу  PhysicalAddress
        /// отношение one-to-one, PhysicalAddress зависимая таблица
        /// </summary>
        public PhysicalAddress PhysicalAddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// Навигационно свойство на таблицу User
        /// отношение one-to-one, UserRequisites главная таблица
        /// </summary>
        public User User { get; set; }
    }
}
