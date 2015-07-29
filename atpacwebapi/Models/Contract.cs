using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace atpac.webapi.Models
{
    public class Contract
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Int64 OrganizationIdPrincipal { get; set; }
        public string Link { get; set; }
    }
}