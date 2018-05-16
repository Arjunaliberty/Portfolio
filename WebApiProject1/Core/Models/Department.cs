namespace Core.Models
{
    /// <summary>
    /// Модель таблицы Departments
    /// </summary>
    public class Department
    {
        public int Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// Нав.св-во на таблицу Users (one-to-many)
        /// </summary>
        public User User { get; set; }
    }
}
