using Kernel.Context;
using Kernel.Core;
using Kernel.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Models;

namespace Web.Controllers
{
    public class CallController : Controller
    {
        public ActionResult Index()
        {
            if (Session["EmployeeId"] == null) RedirectToAction("Index", "Home");

            return View();
        }
                
        /// <summary>
        /// Метод для сохранения в журнал звонков и журнал отложенных задач 
        /// </summary>
        /// <param name="id">Идентификатор сервиса</param>
        /// <param name="call">Модель журнала звонков из представления</param>
        /// <returns>Перенаправление на /Task/FilterRequest</returns>
        public ActionResult SaveLog(int id, CallLog callLog)
        {
            FilterForm filter = (FilterForm)Session["Filter"];
            Service service = null;
            TaskLog task = null;

            if (Session["EmployeeId"] == null) RedirectToAction("", "", new Error { Message = "Для работы с журналом выберите служащего." });
            if (id < 1) throw new Exception("Параметр не может быть меньше 1");
            if (callLog == null) throw new Exception("Ссылка на объект не указывает на экземпляр объекта");
            if (callLog.Result == null || callLog.Result == "") return RedirectToAction("Index", "Error", new Error { Message = "Результат зовнка не определен" });
            if (callLog.Phone == null || callLog.Phone == "") return RedirectToAction("Index", "Error", new Error { Message = "Номер зовнка не определен" });

            using (DatabaseContext db = new DatabaseContext())
            {
                service = db.Services.Include(s => s.ServiceState).Include(s=>s.CallLog).Include(s=>s.TaskLog).Where(s => s.Id == id).FirstOrDefault();
                if (service == null) return RedirectToAction("Index", "Error", new Error { Message = "Сервиса в базе данных не существует" });

                // Сохраняем данные в журнал звонков
                if (service.CallLog == null)
                {
                    callLog.TimeStamp = DateTime.Now;
                    callLog.EmployeeId = (int)Session["EmployeeId"];
                    service.CallLog = callLog;
                    db.SaveChanges();
                }
                else
                {
                    callLog.TimeStamp = DateTime.Now;
                    callLog.EmployeeId = (int)Session["EmployeeId"];
                    service.CallLog = callLog;
                    db.SaveChanges();
                }

                // Сохраняем данные в журнал отложенных задач
                if (service.TaskLog == null)
                {
                    switch (callLog.Result)
                    {
                        case "Уведомлен, оплатят":
                        case "Уведомлен, отказ":
                        case "Иное":
                            if (service.ServiceState.Ok)
                            {
                                task = new TaskLog
                                {
                                    TimeStamp = DateTime.Now,
                                    HideExDate = service.ExDate,
                                    Count = 1
                                };
                                service.TaskLog = task;
                                db.SaveChanges();
                            }
                            if (service.ServiceState.PendingDelete)
                            {
                                task = new TaskLog
                                {
                                    TimeStamp = DateTime.Now,
                                    HideExDate = service.PendingExDate,
                                    Count = 1
                                };
                                service.TaskLog = task;
                                db.SaveChanges();
                            }
                            break;
                        case "Контакты неактуальны":
                            task = new TaskLog
                            {
                                TimeStamp = DateTime.Now,
                                HideExDate = service.PendingExDate,
                                Count = 1
                            };
                            service.TaskLog = task;
                            db.SaveChanges();
                            break;
                        case "Не берет трубку":
                        case "Временно не доступен":
                        case "Номер занят":
                            task = new TaskLog
                            {
                                TimeStamp = DateTime.Now,
                                HideExDate = DateTime.Now.Add(new TimeSpan(23, 59, 59)),
                                Count = 1
                            };
                            service.TaskLog = task;
                            db.SaveChanges();
                            break;
                    }
                }
                else
                {
                    switch (callLog.Result)
                    {
                        case "Уведомлен, оплатят":
                        case "Уведомлен, отказ":
                        case "Иное":
                            if (service.ServiceState.Ok)
                            {
                                service.TaskLog.HideExDate = service.ExDate;
                                service.TaskLog.Count--;
                                db.SaveChanges();
                            }
                            if (service.ServiceState.PendingDelete)
                            {
                                service.TaskLog.HideExDate = service.PendingExDate;
                                service.TaskLog.Count--;
                                db.SaveChanges();
                            }
                            break;
                        case "Контакты неактуальны":
                            service.TaskLog.HideExDate = service.PendingExDate;
                            service.TaskLog.Count--;
                            db.SaveChanges();
                            break;
                        case "Не берет трубку":
                        case "Временно не доступен":
                        case "Номер занят":
                            if (service.TaskLog.Count > 0)
                            {
                                service.TaskLog.HideExDate = DateTime.Now.Add(new TimeSpan(23, 59, 59));
                                service.TaskLog.Count--;
                                db.SaveChanges();
                            }
                            if (service.TaskLog.Count < 1)
                            {
                                if (service.ServiceState.Ok)
                                {
                                    service.TaskLog.HideExDate = service.ExDate;
                                    service.TaskLog.Count--;
                                    db.SaveChanges();
                                }
                                if (service.ServiceState.PendingDelete)
                                {
                                    service.TaskLog.HideExDate = service.PendingExDate;
                                    service.TaskLog.Count--;
                                    db.SaveChanges();
                                }
                            }
                            break;
                    }
                }
            }

            return RedirectToAction("FilterRequest", "Task");
        }

        [HttpPost]
        public ActionResult GetCallTable(CallForm callForm)
        {
            if (Session["EmployeeId"] == null) RedirectToAction("Index", "Home");

            int pageSize = 10;
            CallInfo callInfo = null;
            Session["CallForm"] = callForm;
            bool check = false;

            switch (callForm.Result)
            {
                case "Все":
                    using(DatabaseContext db = new DatabaseContext())
                    {
                        callInfo = new CallInfo
                        {
                            PageInfo = new PageInfo
                            {
                                CurrentPage = 1,
                                ItemsPerPage = pageSize
                            }
                        };

                        // Поиск по имени сервиса
                        if(callForm.ServiceName != "")
                        {
                            if(callForm.From != "" && callForm.To != "")
                            {
                                DateTime f = Convert.ToDateTime(callForm.From);
                                DateTime t = Convert.ToDateTime(callForm.To);

                                callInfo.Calls = db.CallLogs
                                                .Include(c=>c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c=>c.Service.User)
                                                .Where(c => c.Service.Name == callForm.ServiceName && (c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1))
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Take(pageSize)
                                                .ToList();
                                callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                                check = true;
                            }
                            else
                            {
                                DateTime f = Convert.ToDateTime(callForm.From);
                                DateTime t = Convert.ToDateTime(callForm.To);

                                callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .Where(c => c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1)
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Take(pageSize)
                                                .ToList();
                                callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                                check = true;
                            }
                        }

                        //Поиск по дате
                        if (callForm.From != "" && callForm.To != "")
                        {
                            DateTime f = Convert.ToDateTime(callForm.From);
                            DateTime t = Convert.ToDateTime(callForm.To);

                            callInfo.Calls = db.CallLogs
                                            .Include(c => c.Service)
                                            .Include(c => c.Service.ServiceState)
                                            .Include(c => c.Service.User)
                                            .Where(c => c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1)
                                            .OrderByDescending(c => c.TimeStamp)
                                            .Take(pageSize)
                                            .ToList();
                            callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                            check = true;
                        }

                        // Если другие фильтры не заданы
                        if(!check)
                        {
                            callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Take(pageSize)
                                                .ToList();
                            callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                        }
                    }
                    break;
                case "Уведомлен, оплатят":
                case "Уведомлен, отказ":
                case "Не берет трубку":
                case "Временно не доступен":
                case "Номер занят":
                case "Контакты неактуальны":
                case "Иное":
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        callInfo = new CallInfo
                        {
                            PageInfo = new PageInfo
                            {
                                CurrentPage = 1,
                                ItemsPerPage = pageSize
                            }
                        };

                        // Поиск по имени сервиса
                        if (callForm.ServiceName != "")
                        {
                            if (callForm.From != "" && callForm.To != "")
                            {
                                DateTime f = Convert.ToDateTime(callForm.From);
                                DateTime t = Convert.ToDateTime(callForm.To);

                                callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .Where(c => c.Result == callForm.Result && c.Service.Name == callForm.ServiceName && (c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1))
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Take(pageSize)
                                                .ToList();
                                callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                                check = true;
                            }
                            else
                            {
                                DateTime f = Convert.ToDateTime(callForm.From);
                                DateTime t = Convert.ToDateTime(callForm.To);

                                callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .Where(c => c.Result == callForm.Result && c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1)
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Take(pageSize)
                                                .ToList();
                                callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                                check = true;
                            }
                        }

                        //Поиск по дате
                        if (callForm.From != "" && callForm.To != "")
                        {
                            DateTime f = Convert.ToDateTime(callForm.From);
                            DateTime t = Convert.ToDateTime(callForm.To);

                            callInfo.Calls = db.CallLogs
                                            .Include(c => c.Service)
                                            .Include(c => c.Service.ServiceState)
                                            .Include(c => c.Service.User)
                                            .Where(c => c.Result == callForm.Result && c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1)
                                            .OrderByDescending(c => c.TimeStamp)
                                            .Take(pageSize)
                                            .ToList();
                            callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                            check = true;
                        }

                        // Если другие фильтры не заданы
                        if (!check)
                        {
                            callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .Where(c=> c.Result == callForm.Result)
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Take(pageSize)
                                                .ToList();
                            callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                        }
                    }
                    break;
            }
                  
            return View(callInfo);
        }

        [HttpGet]
        public ActionResult GetCurrentPageTable(int page = 1)
        {
            if (Session["EmployeeId"] == null) RedirectToAction("Index", "Home");

            CallForm callForm = (CallForm)Session["CallForm"];
            CallInfo callInfo = null;
            int pageSize = 10;
            bool check = false;

            switch (callForm.Result)
            {
                case "Все":
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        callInfo = new CallInfo
                        {
                            PageInfo = new PageInfo
                            {
                                CurrentPage = page,
                                ItemsPerPage = pageSize
                            }
                        };

                        // Поиск по имени сервиса
                        if (callForm.ServiceName != "")
                        {
                            if (callForm.From != "" && callForm.To != "")
                            {
                                DateTime f = Convert.ToDateTime(callForm.From);
                                DateTime t = Convert.ToDateTime(callForm.To);

                                callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .Where(c => c.Service.Name == callForm.ServiceName && (c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1))
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToList();
                                callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                                check = true;
                            }
                            else
                            {
                                DateTime f = Convert.ToDateTime(callForm.From);
                                DateTime t = Convert.ToDateTime(callForm.To);

                                callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .Where(c => c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1)
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToList();
                                callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                                check = true;
                            }
                        }

                        //Поиск по дате
                        if (callForm.From != "" && callForm.To != "")
                        {
                            DateTime f = Convert.ToDateTime(callForm.From);
                            DateTime t = Convert.ToDateTime(callForm.To);

                            callInfo.Calls = db.CallLogs
                                            .Include(c => c.Service)
                                            .Include(c => c.Service.ServiceState)
                                            .Include(c => c.Service.User)
                                            .Where(c => c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1)
                                            .OrderByDescending(c => c.TimeStamp)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();
                            callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                            check = true;
                        }

                        // Если другие фильтры не заданы
                        if (!check)
                        {
                            callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToList();
                            callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                        }
                    }
                    break;
                case "Уведомлен, оплатят":
                case "Уведомлен, отказ":
                case "Не берет трубку":
                case "Временно не доступен":
                case "Номер занят":
                case "Контакты неактуальны":
                case "Иное":
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        callInfo = new CallInfo
                        {
                            PageInfo = new PageInfo
                            {
                                CurrentPage = page,
                                ItemsPerPage = pageSize
                            }
                        };

                        // Поиск по имени сервиса
                        if (callForm.ServiceName != "")
                        {
                            if (callForm.From != "" && callForm.To != "")
                            {
                                DateTime f = Convert.ToDateTime(callForm.From);
                                DateTime t = Convert.ToDateTime(callForm.To);

                                callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .Where(c => c.Result == callForm.Result && c.Service.Name == callForm.ServiceName && (c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1))
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToList();
                                callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                                check = true;
                            }
                            else
                            {
                                DateTime f = Convert.ToDateTime(callForm.From);
                                DateTime t = Convert.ToDateTime(callForm.To);

                                callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .Where(c => c.Result == callForm.Result && c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1)
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToList();
                                callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                                check = true;
                            }
                        }

                        //Поиск по дате
                        if (callForm.From != "" && callForm.To != "")
                        {
                            DateTime f = Convert.ToDateTime(callForm.From);
                            DateTime t = Convert.ToDateTime(callForm.To);

                            callInfo.Calls = db.CallLogs
                                            .Include(c => c.Service)
                                            .Include(c => c.Service.ServiceState)
                                            .Include(c => c.Service.User)
                                            .Where(c => c.Result == callForm.Result && c.TimeStamp.Value.CompareTo(f) > 1 && c.TimeStamp.Value.CompareTo(t) < 1)
                                            .OrderByDescending(c => c.TimeStamp)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();
                            callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                            check = true;
                        }

                        // Если другие фильтры не заданы
                        if (!check)
                        {
                            callInfo.Calls = db.CallLogs
                                                .Include(c => c.Service)
                                                .Include(c => c.Service.ServiceState)
                                                .Include(c => c.Service.User)
                                                .Where(c => c.Result == callForm.Result)
                                                .OrderByDescending(c => c.TimeStamp)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToList();
                            callInfo.PageInfo.TotalItems = callInfo.Calls.Count();
                        }
                    }
                    break;
            }



            return View("GetCallTable", callInfo);
        }
    }
}