namespace Domain.Models
{
    public class PhysicalAddress
    {
        public int Id { get; set; }
        public string Index { get; set; }
        public string Region { get; set; }
        public string Locality { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string RoomNumber { get; set; }
        /// <summary>
        /// Навигационно свойство на таблицу PhysicalRequisites
        /// отношение one-to-one, PhysicalRequisites главная таблица
        /// </summary>
        public PhysicalRequisites PhysicalRequisites { get; set; }
    }
}
