using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kernel.Models;

namespace Web.Models
{
    /// <summary>
    /// Класс для передачи данный в таблицу журнала звонков
    /// </summary>
    public class RequestFilterForm
    {
        public int ServiceId { get; set; }
        public ServiceType ServicesType { get; set; }
        public string ServicesName { get; set; }
        public DateTime? ExDate { get; set; }
        public DateTime? PendingExDate { get; set; }
        public bool Ok { get; set; }
        public bool PendingDelete { get; set; }
        public int UserId { get; set; }
        public ContractType UserType { get; set; }
        public string UserLegalName { get; set; }
        public string UserPhysicName { get; set; }
        public string UserIndividualName { get; set; }
        public string UserLegaPhone { get; set; }
        public string UserPhysicPhone { get; set; }
        public string UserIndividualPhone { get; set; }
        public string UserEmail { get; set; }
    }
}