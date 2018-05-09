using Kernel.Context;
using Kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Метод для точки входа
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Employee> employee = null;

            using(DatabaseContext db = new DatabaseContext())
            {
                employee = db.Employees.ToList();
                if (employee.Count < 1) return RedirectToAction("Index", "Employee");
            }

            return View(employee);
        }
        /// <summary>
        /// Метод для установки текущего сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetEmployee()
        {
            int id = Int32.Parse(Request.Form["employee"]);

            using(DatabaseContext db = new DatabaseContext())
            {
                Employee employee = db.Employees.Where(e => e.Id == id).FirstOrDefault();
                Session["EmployeeId"] = employee.Id;
                Session["EmployeeName"] = employee.FirstName + " " + employee.SecondName;
            }

            return View("Index");
        }
    }
}