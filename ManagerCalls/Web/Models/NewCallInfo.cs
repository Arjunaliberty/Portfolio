using Kernel.Models;

namespace Web.Models
{
    /// <summary>
    /// Модель для предачи в форму "Новый звонок"
    /// </summary>
    public class NewCallInfo
    {
        public int? CurrentService { get; set; }
        public User User { get; set; }
    }
}