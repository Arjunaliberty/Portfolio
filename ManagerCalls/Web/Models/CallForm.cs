using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CallForm
    {
        public string Result { get; set; }
        public string ServiceName { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}