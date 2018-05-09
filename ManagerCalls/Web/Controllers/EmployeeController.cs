using Kernel.Context;
using Kernel.Core;
using Kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с сотрудником
    /// </summary>
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Action для формы добавления нового сутрудника
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(new Employee());
        }

        /// <summary>
        /// Action для сохранения нового сотрудника в БД 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            if (employee == null) return RedirectToAction("Index","Error", new Error { Message = "Ошибка при инициализации пользователя" });
            using(DatabaseContext db = new DatabaseContext())
            {
                Employee emp = db.Employees.Where(e =>e.FirstName == employee.FirstName && e.SecondName == employee.SecondName).FirstOrDefault();
                if (emp != null) RedirectToAction("Index", "Error", new Error { Message = "Сотрудник с указанными имем и фаилией существует." });
            }
            EmployeeManager.Add(employee);

            return RedirectToAction("Index", "Home");
        }
    }
}