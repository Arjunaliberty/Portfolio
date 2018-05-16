namespace Core.Models
{
    /// <summary>
    /// Модель таблицы Users
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// Нав.св-во на таблицу Departments (one-to-one)
        /// </summary>
        public Department Department { get; set; }
    }
}
