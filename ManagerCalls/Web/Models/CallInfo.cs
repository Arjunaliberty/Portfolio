using Kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    /// <summary>
    /// Модель для журнала звонков
    /// </summary>
    public class CallInfo
    {
        public List<CallLog> Calls { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}