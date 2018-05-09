using Kernel.Context;
using Kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Core
{
    /// <summary>
    /// Класс для работы с сотрудниками
    /// </summary>
    public class EmployeeManager
    {
        private Employee employee;

        public EmployeeManager(Employee employee)
        {
            this.employee = employee ?? throw new Exception("Параметр не может быть null");
        }

        public static void Add(Employee newEmployee)
        {
            if (newEmployee == null) throw new Exception("Параметр не может быть null");
            if (newEmployee.Id == 0) InsertEntity.Insert(newEmployee);
            else throw new Exception("Ошибка при инициализации типа Employee");
        }
    }
}
