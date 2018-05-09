using Kernel.Context;
using Kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;


namespace Web.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            if (Session["EmployeeId"] == null) RedirectToAction("", "", new Error { Message = "Для работы с журналом выберите служащего." });
            return View();
        }
        /// <summary>
        /// Метод обрабатывает данные из фильтра журнала задачь
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Перенаправляет на /Task/FilterRequest</returns>
        public ActionResult Filter(FilterForm filter)
        {
            if (Session["EmployeeId"] == null) RedirectToAction("Index", "Error", new Error { Message = "Для работы с журналом выберите служащего." });
            // Сохраняем данные из фильтра в сессию и перенаправляем
            Session["Filter"] = filter;

            return RedirectToAction("FilterRequest");
        }
        /// <summary>
        /// Метод запрашивает данные из БД
        /// </summary>
        /// <returns></returns>
        public ActionResult FilterRequest()
        {
            if (Session["EmployeeId"] == null) RedirectToAction("Index", "Error", new Error { Message = "Для работы с журналом выберите служащего." });
            // Получаем данные фильтра из сессии
            FilterForm filter = (FilterForm)Session["Filter"];
            IEnumerable<RequestFilterForm> preparation = null;
            List<RequestFilterForm> result = new List<RequestFilterForm>();

            using (DatabaseContext db = new DatabaseContext())
            {
                // Выборка необходимых данных по основным критериям поиска
                preparation = db.Services.Where(s => (s.User.Country.Equals("BY"))
                                               && (!s.ServiceState.Delete)
                                               && (s.ServiceType == ServiceType.Domain || s.ServiceType == ServiceType.Host))
                                               //&& (!s.TaskLog.TimeStamp))
                                               .Select(s => new
                                               {
                                                   serviceId = s.Id,
                                                   servicesType = s.ServiceType,
                                                   servicesName = s.Name,
                                                   servicesExDate = s.ExDate,
                                                   servicesPendingExDate = s.PendingExDate,
                                                   serviceOk = s.ServiceState.Ok,
                                                   servicePendingDelete = s.ServiceState.PendingDelete,
                                                   userId = s.User.Id,
                                                   userType = s.User.ContractType,
                                                   userLegalName = s.User.LegalRequisites.NameOrganization,
                                                   userPhysicName = s.User.PhysicalRequisites.FIO,
                                                   userIndividualName = s.User.IndividualRequisites.FIO,
                                                   userLegaPhone = s.User.LegalRequisites.Phone,
                                                   userPhysicPhone = s.User.PhysicalRequisites.Phone,
                                                   userIndividualPhone = s.User.IndividualRequisites.Phone,
                                                   userEmail = s.User.Email
                                               })
                                               .AsEnumerable()
                                               .Select(an => new RequestFilterForm
                                               {
                                                   ServiceId = an.serviceId,
                                                   ServicesType = an.servicesType,
                                                   ServicesName = an.servicesName,
                                                   ExDate = an.servicesExDate,
                                                   PendingExDate = an.servicesPendingExDate,
                                                   Ok = an.serviceOk,
                                                   PendingDelete = an.servicePendingDelete,
                                                   UserId = an.userId,
                                                   UserType = an.userType,
                                                   UserLegalName = an.userLegalName,
                                                   UserPhysicName = an.userPhysicName,
                                                   UserIndividualName = an.userIndividualName,
                                                   UserLegaPhone = an.userLegaPhone,
                                                   UserPhysicPhone = an.userPhysicPhone,
                                                   UserIndividualPhone = an.userIndividualPhone,
                                                   UserEmail = an.userEmail

                                               }).ToList();
            }

            // Выборка по параметрам фильтра
            foreach (var serv in preparation)
            {
                if (filter.Domain == ServiceType.Domain)
                {
                    int count = 0;
                    foreach (var res in result)
                    {
                        if (res.Equals(serv)) count++;
                    }
                    if (count == 0 && serv.ServicesType == ServiceType.Domain) result.Add(serv);
                }
                if (filter.Host == ServiceType.Host)
                {
                    int count = 0;
                    foreach (var res in result)
                    {
                        if (res.Equals(serv)) count++;
                    }
                    if (count == 0 && serv.ServicesType == ServiceType.Host) result.Add(serv);
                }
                if (filter.Legal == ContractType.Legal)
                {
                    int count = 0;
                    foreach (var res in result)
                    {
                        if (res.Equals(serv)) count++;
                    }
                    if (count == 0 && serv.UserType == ContractType.Legal) result.Add(serv);
                }
                if (filter.Physical == ContractType.Physical)
                {
                    int count = 0;
                    foreach (var res in result)
                    {
                        if (res.Equals(serv)) count++;
                    }
                    if (count == 0 && serv.UserType == ContractType.Physical) result.Add(serv);
                }
                if (filter.Individual == ContractType.Individual)
                {
                    int count = 0;
                    foreach (var res in result)
                    {
                        if (res.Equals(serv)) count++;
                    }
                    if (count == 0 && serv.UserType == ContractType.Individual) result.Add(serv);
                }
                if (filter.Ok)
                {
                    int count = 0;
                    foreach (var res in result)
                    {
                        if (res.Equals(serv)) count++;
                    }
                    if (count == 0 && serv.Ok) result.Add(serv);
                }
                if (filter.PendingDelete)
                {
                    int count = 0;
                    foreach (var res in result)
                    {
                        if (res.Equals(serv)) count++;
                    }
                    if (count == 0 && serv.PendingDelete) result.Add(serv);
                }
                if (DateTime.Now.CompareTo(serv.PendingExDate.Value) <= 0
                        && DateTime.Now.CompareTo(serv.PendingExDate.Value.Subtract(new TimeSpan(filter.Range, 0, 0, 0))) > 0)
                {
                    int count = 0;
                    foreach (var res in result)
                    {
                        if (res.Equals(serv)) count++;
                    }
                    if (count == 0) result.Add(serv);
                }
            }

            return View(result);
        }
    }
}