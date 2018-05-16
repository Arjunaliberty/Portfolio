using System;

namespace Domain.Models
{
    public class IndividualRequisites
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string AccountNumber { get; set; }
        public string Series { get; set; }
        public string PassportNumber { get; set; }
        public string IssuedBy { get; set; }
        public DateTime? DateIssue { get; set; }
        public string PersonalNumber { get; set; }
        /// <summary>
        /// Навигационно свойство на таблицу IndividualAddress
        /// отношение one-to-one, IndividualAddress зависимая таблица
        /// </summary>
        public IndividualAddress IndividualAddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        // Банковские реквизиты
        public string BankAccount { get; set; }
        public string NameBank { get; set; }
        public string BankCode { get; set; }
        // Сведения из единого гос. реестра
        public string RegistrationNumber { get; set; }
        public string BodyRegistration { get; set; }
        public string NumberRegistration { get; set; }
        public DateTime? DateRegistration { get; set; }

        /// <summary>
        /// Навигационно свойство на таблицу User
        /// отношение one-to-one, UserRequisites главная таблица
        /// </summary>
        public User User { get; set; }
    }
}
