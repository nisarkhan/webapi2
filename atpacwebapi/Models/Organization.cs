using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace atpac.webapi.Models
{
    public class Organization
    {
        public Int64 Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}