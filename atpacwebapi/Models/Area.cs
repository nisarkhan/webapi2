using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace atpac.webapi.Models
{
    public class Area
    {
        public Int64 Id { get; set; }
        public int ContractId { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public bool Inactive { get; set; }
        
    }
}