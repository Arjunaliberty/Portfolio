using Kernel.Context;
using Kernel.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class UserWebApi : ApiController
    {
        /// <summary>
        /// Метод для получения из БД пользователя по его id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public string Get(int id)
        {
            User user;

            using (DatabaseContext db = new DatabaseContext())
            {
                ContractType contract = db.Users.Where(u => u.Id == id).Select(u => u.ContractType).FirstOrDefault();

                switch (contract)
                {
                    case ContractType.Legal:
                        user = db.Users.Where(u => u.Id == id).Select(u => new
                        {
                            id = u.Id,
                            email = u.Email,
                            country = u.Country,
                            contractType = u.ContractType,
                            services = u.Services.Select(s => new
                            {
                                serviceType = s.ServiceType,
                                name = s.Name,
                                pendingExDate = s.PendingExDate,
                                serviceState = s.ServiceState,
                                callLog = new
                                {
                                    timeStamp = s.CallLog.TimeStamp,
                                    phone = s.CallLog.Phone,
                                    result = s.CallLog.Result,
                                    comment = s.CallLog.Comment,
                                    employee = new
                                    {
                                        firstName = s.CallLog.Employee.FirstName,
                                        secondName = s.CallLog.Employee.SecondName,
                                        role = s.CallLog.Employee.Role
                                    }
                                }
                            }),
                            legalRequisites = new
                            {
                                nameOrganization = u.LegalRequisites.NameOrganization
                            }
                        })
                        .AsEnumerable()
                        .Select(a=> new User
                        {
                            Id = a.id,
                            Email = a.email,
                            Country = a.country,
                            ContractType = a.contractType,
                            Services = a.services.Select(s=> new Service
                            {
                                ServiceType = s.serviceType,
                                Name = s.name,
                                PendingExDate = s.pendingExDate,
                                ServiceState = s.serviceState,
                                CallLog = new CallLog
                                {
                                    TimeStamp = s.callLog.timeStamp,
                                    Phone = s.callLog.phone,
                                    Result = s.callLog.result,
                                    Comment = s.callLog.comment,
                                    Employee = new Employee
                                    {
                                        FirstName = s.callLog.employee.firstName,
                                        SecondName = s.callLog.employee.secondName,
                                        Role = s.callLog.employee.role
                                    }
                                }

                            }).ToList(),
                            LegalRequisites = new LegalRequisites
                            {
                                NameOrganization = a.legalRequisites.nameOrganization
                            }
                        })
                        .FirstOrDefault();
                        break;
                    case ContractType.Physical:
                        user = db.Users.Where(u => u.Id == id).Select(u => new
                        {
                            id = u.Id,
                            email = u.Email,
                            country = u.Country,
                            contractType = u.ContractType,
                            services = u.Services.Select(s => new
                            {
                                serviceType = s.ServiceType,
                                name = s.Name,
                                pendingExDate = s.PendingExDate,
                                serviceState = s.ServiceState,
                                callLog = new
                                {
                                    timeStamp = s.CallLog.TimeStamp,
                                    phone = s.CallLog.Phone,
                                    result = s.CallLog.Result,
                                    comment = s.CallLog.Comment,
                                    employee = new
                                    {
                                        firstName = s.CallLog.Employee.FirstName,
                                        secondName = s.CallLog.Employee.SecondName,
                                        role = s.CallLog.Employee.Role
                                    }
                                }
                            }),
                            physicalRequisites = new
                            {
                                fio = u.PhysicalRequisites.FIO
                            }
                        })
                        .AsEnumerable()
                        .Select(a => new User
                        {
                            Id = a.id,
                            Email = a.email,
                            Country = a.country,
                            ContractType = a.contractType,
                            Services = a.services.Select(s => new Service
                            {
                                ServiceType = s.serviceType,
                                Name = s.name,
                                PendingExDate = s.pendingExDate,
                                ServiceState = s.serviceState,
                                CallLog = new CallLog
                                {
                                    TimeStamp = s.callLog.timeStamp,
                                    Phone = s.callLog.phone,
                                    Result = s.callLog.result,
                                    Comment = s.callLog.comment,
                                    Employee = new Employee
                                    {
                                        FirstName = s.callLog.employee.firstName,
                                        SecondName = s.callLog.employee.secondName,
                                        Role = s.callLog.employee.role
                                    }
                                }

                            }).ToList(),
                            PhysicalRequisites = new PhysicalRequisites
                            {
                                FIO = a.physicalRequisites.fio
                            }
                        })
                        .FirstOrDefault();
                        break;
                    default:
                        user = db.Users.Where(u => u.Id == id).Select(u => new
                        {
                            id = u.Id,
                            email = u.Email,
                            country = u.Country,
                            contractType = u.ContractType,
                            services = u.Services.Select(s => new
                            {
                                serviceType = s.ServiceType,
                                name = s.Name,
                                pendingExDate = s.PendingExDate,
                                serviceState = s.ServiceState,
                                callLog = new
                                {
                                    timeStamp = s.CallLog.TimeStamp,
                                    phone = s.CallLog.Phone,
                                    result = s.CallLog.Result,
                                    comment = s.CallLog.Comment,
                                    employee = new
                                    {
                                        firstName = s.CallLog.Employee.FirstName,
                                        secondName = s.CallLog.Employee.SecondName,
                                        role = s.CallLog.Employee.Role
                                    }
                                }
                            }),
                            individualRequisites = new
                            {
                                fio = u.IndividualRequisites.FIO
                            }
                        })
                       .AsEnumerable()
                       .Select(a => new User
                       {
                           Id = a.id,
                           Email = a.email,
                           Country = a.country,
                           ContractType = a.contractType,
                           Services = a.services.Select(s => new Service
                           {
                               ServiceType = s.serviceType,
                               Name = s.name,
                               PendingExDate = s.pendingExDate,
                               ServiceState = s.serviceState,
                               CallLog = new CallLog
                               {
                                   TimeStamp = s.callLog.timeStamp,
                                   Phone = s.callLog.phone,
                                   Result = s.callLog.result,
                                   Comment = s.callLog.comment,
                                   Employee = new Employee
                                   {
                                       FirstName = s.callLog.employee.firstName,
                                       SecondName = s.callLog.employee.secondName,
                                       Role = s.callLog.employee.role
                                   }
                               }

                           }).ToList(),
                           IndividualRequisites = new IndividualRequisites
                           {
                               FIO = a.individualRequisites.fio
                           }
                       })
                       .FirstOrDefault();
                       break;
                }

            }

            string result = JsonConvert.SerializeObject(user, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return result;
        }
    }
}