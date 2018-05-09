using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Models
{
    public class IndividualAddress
    {
        public int Id { get; set; }
        public string Index { get; set; }
        public string Region { get; set; }
        public string Locality { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string RoomNumber { get; set; }
        /// <summary>
        /// Навигационно свойство на таблицу IndividualRequisites
        /// отношение one-to-one, IndividualRequisites главная таблица
        /// </summary>
        public IndividualRequisites IndividualRequisites { get; set; }
    }
}
