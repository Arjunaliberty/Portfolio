using Kernel.Context;
using Kernel.Core;
using Kernel.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Web.Models;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View(new User());
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(User user)
        {
            // Проверка на существование записи в базе данных
            using (DatabaseContext db = new DatabaseContext())
            {
                User us = db.Users.Where(u => u.Login == user.Login).FirstOrDefault();
                if (us != null) RedirectToAction("Index", "Error", new Error { Message = "Пользователь с таким логином уже существует." });
            }
            // Создаем услугу для пользователя
            if (Request.Form["Service.ServiceType"].Equals("Domain"))
            {
                user.Role = Role.User;
                user.Services.Add(
                    new Service
                    {
                            ServiceType = ServiceType.Domain,
                            ExDate = DateTime.Now,
                            PendingExDate = DateTime.Now.AddYears(1),
                            ServiceState = new ServiceState
                            {
                                // Имитирует оплаченную пользователем услугу
                                Ok = true,
                                PendingDelete = false,
                                Delete = false
                            },
                            CallLog = new CallLog
                            {
                                TimeStamp = DateTime.Now,
                            },
                            TaskLog = new TaskLog
                            {
                                TimeStamp = DateTime.Now,
                                Count = 1
                            }
                    });
            }
            if (Request.Form["Service.ServiceType"].Equals("Host"))
            {
                user.Role = Role.User;
                user.Services.Add(
                    new Service
                    {
                        ServiceType = ServiceType.Host,
                        ExDate = DateTime.Now,
                        PendingExDate = DateTime.Now.AddYears(1),
                        ServiceState = new ServiceState
                        {
                            // Имитирует оплаченную пользователем услугу
                            Ok = true,
                            PendingDelete = false,
                            Delete = false
                        },
                        CallLog = new CallLog
                        {
                            TimeStamp = DateTime.Now
                        },
                        TaskLog = new TaskLog
                        {
                            TimeStamp = DateTime.Now,
                            Count = 1
                        }
                    });
            }
            
            // Записываем нового пользователя в БД 
            UserManager.Add(user);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetUser(int id1, int id2)
        {
            User currentUser = null;
            int? serviseId = null;
            NewCallInfo info = null; 
            using (DatabaseContext db = new DatabaseContext())
            {
                currentUser = db.Users.Include(u => u.LegalRequisites)
                                .Include(u => u.LegalRequisites.LegalAddress)
                                 .Include(u => u.LegalRequisites.LegalContactAddress)
                                  .Include(u => u.PhysicalRequisites)
                                   .Include(u => u.PhysicalRequisites.PhysicalAddress)
                                    .Include(u => u.IndividualRequisites)
                                     .Include(u => u.IndividualRequisites.IndividualAddress)
                                      .Include(u => u.Services)
                                       .Include(u => u.Services.Select(s => s.CallLog))
                                        .Include(u => u.Services.Select(s => s.CallLog.Employee))
                               .Where(u => u.Id == id1).FirstOrDefault();

                serviseId = db.Services.Where(s => s.Id == id2).Select(s => s.Id).FirstOrDefault();
            }

            info = new NewCallInfo
            {
                CurrentService = serviseId,
                User = currentUser
            };

            return JsonConvert.SerializeObject(info, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }
}