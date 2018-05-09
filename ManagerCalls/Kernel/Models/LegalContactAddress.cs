using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Models
{
    /// <summary>
    /// Класс для хранения адреса юридического лица
    /// </summary>
    public class LegalContactAddress
    {
        public int Id { get; set; }
        public string Index { get; set; }
        public string Region { get; set; }
        public string Locality { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string RoomNumber { get; set; }
        /// <summary>
        /// Навигационно свойство на таблицу LegalRequisites
        /// отношение one-to-one, LegalRequisites главная таблица
        /// </summary>
        public LegalRequisites LegalRequisites { get; set; }
    }
}
