using System;

namespace Domain.Models
{   
    /// <summary>
    /// Класс для хранения юридических ревизитов
    /// </summary>
    public class LegalRequisites
    {
        public int Id { get; set; }
        // Юридические реквизиты
        public string NameOrganization { get; set; }
        public string NameHead { get; set; }
        /// <summary>
        /// Навигационно свойство на таблицу LegalAddress
        /// отношение one-to-one, LegalAddress зависимая таблица
        /// </summary>
        public LegalAddress LegalAddress { get; set; }
        public string AccountNumber { get; set; }
        // Контактные реквизиты
        /// <summary>
        /// Навигационно свойство на таблицу
        /// отношение one-to-one, LegalContactAddress зависимая таблица
        /// </summary>
        public LegalContactAddress LegalContactAddress { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        
        // Банковские реквизиты
        public string BankAccount { get; set; }
        public string NameBank { get; set; }
        public string BankCode { get; set; }
        
        // Сведения о государственной регистрации
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
