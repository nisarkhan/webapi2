using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace atpac.webapi.Models
{
    public class Scaffold
    {
        public Int64 Tag { get; set; }
        public Int32 ContractId { get; set; }
        public Int32 OrganizationIdInitiator { get; set; }
        public string Status { get; set; }
        public Int32 ScaffoldTypeId { get; set; } 
    }
}