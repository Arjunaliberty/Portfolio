using System.ComponentModel.DataAnnotations;

namespace AdoDomain.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Поле не может быть пустым")]
        [Display(Name ="Имя")]
        [RegularExpression(@"^[A-zА-Я]+$", ErrorMessage ="Некорректный ввод")]
        [StringLength(50, MinimumLength = 3, ErrorMessage ="Длина строки должна быть от 3 до 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[A-zА-Я]+$", ErrorMessage = "Некорректный ввод")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Логин")]
        [RegularExpression(@"^[A-zА-я0-9_]+$", ErrorMessage ="Некорректный ввод")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Пароль")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Некорректный ввод")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 20 символов")]
        public int Password { get; set; }

        [Display(Name = "Роль")]
        public int Role { get; set; }
    }
}
