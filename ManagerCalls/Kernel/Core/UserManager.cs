using Kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Core
{
    /// <summary>
    /// Класс ждя работы с пользователями
    /// </summary>
    public class UserManager
    {
        private User user;

        public UserManager(User user)
        {
            this.user = user ?? throw new Exception("Параметр не может быть null");
        }

        public static void Add(User user)
        {
            if (user == null) throw new Exception("Параметр не может быть null");
            if (user.Id == 0) InsertEntity.Insert(user);
            else throw new Exception("Ошибка при инициализации типа Employee");
        }
    }
}
