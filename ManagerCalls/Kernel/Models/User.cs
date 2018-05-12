using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Models
{
    /// <summary>
    /// Перечисление для хранения поля Role
    /// </summary>
    public enum Role
    {
        User = 1,
        Manager,
        Admin
    }

    /// <summary>
    /// Перечисления для хранения типа восстановления пароля
    /// </summary>
    public enum RecoveryType
    {
        Email = 1,
        Statement
    }
    /// <summary>
    /// Перечисление для хранения типа договора
    /// </summary>
    public enum ContractType
    {
        Legal = 1,
        Physical,
        Individual
    }

    /// <summary>
    /// Класс для хранения иформации о пользователе
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Введите имя")]
        [RegularExpression(@"(?!)[A-zА-я0-9]{3,10}", ErrorMessage ="Можно использовать только цифры и буквенные значения")]
        public string Login { get; set; }
        public string Password { get; set; } // На данный момент пароль будет храниться в базе данных ввиде строки
        public string Email { get; set; }
        public Role Role { get; set; }
        public RecoveryType RecoveryType { get; set; }
        public string Country { get; set; }
        public ContractType ContractType { get; set; }

        /// <summary>
        /// Навигационно свойство на таблицу LegalRequisites
        /// отношение one-to-one, LegalRequisites зависимая таблица
        /// </summary>
        public LegalRequisites LegalRequisites { get; set; }
        /// <summary>
        /// Навигационно свойство на таблицу PhysicalRequisites
        /// отношение one-to-one, PhysicalRequisites зависимая таблица
        /// </summary>
        public PhysicalRequisites PhysicalRequisites { get; set; }
        /// <summary>
        /// Навигационно свойство на таблицу IndividualRequisites
        /// отношение one-to-one, IndividualRequisites зависимая таблица
        /// </summary>
        public IndividualRequisites IndividualRequisites { get; set; }

        /// <summary>
        /// Навигационное ствойство на таблицу сервисов
        /// отношение one-to-many
        /// </summary>
        public List<Service> Services { get; set; }

        public User()
        {
            Services = new List<Service>();
        }
    }
}
