using System;

namespace Domain.Models
{
    /// <summary>
    /// Перечесления для хранения типа сервиса
    /// </summary>
    public enum ServiceType
    {
        Domain = 1,
        Host
    }
    /// <summary>
    /// Структура таблицы Services
    /// </summary>
    public class Service
    {
        public int Id { get; set; }
        public ServiceType ServiceType { get; set; }
        public string Name { get; set; }
        public int Term { get; set; }
        /// <summary>
        /// Навигационное свойство на таблицу ServiceState (one to one, Service - главная таблица)
        /// </summary>
        public ServiceState ServiceState { get; set; }
        public DateTime? ExDate { get; set; }
        public DateTime? PendingExDate { get; set; }
        /// <summary>
        /// Навигационное свойство на таблицу CallLogs (one to one)
        /// </summary>
        public CallLog CallLog { get; set; }
        /// <summary>
        /// Навигационное свойство на таблицу CallLogs (one to one)
        /// </summary>
        public TaskLog TaskLog { get; set; }

        /// <summary>
        /// Навигационно свойство на таблицу User
        /// отношение one-to-many
        /// </summary>
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
