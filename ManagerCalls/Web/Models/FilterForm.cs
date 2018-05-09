using Kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class FilterForm
    {
        public ServiceType Domain { get; set; }
        public ServiceType Host { get; set; }
        public ContractType Legal { get; set; }
        public ContractType Physical { get; set; }
        public ContractType Individual { get; set; }
        public bool PendingDelete { get; set; }
        public bool Ok { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int Range {
            get { return To - From; }
        }
    }
}