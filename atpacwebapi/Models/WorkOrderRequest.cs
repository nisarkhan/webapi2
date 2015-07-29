using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace atpac.webapi.Models
{
    public class WorkOrderRequest
    {
        public WorkOrderRequest()
        {
            this.WorkPackage = new List<WorkPackage>();
            this.Area = new List<Area>();
            this.ProcessChainType = new List<ProcessChainType>();
            this.Contract = new List<Contract>();
            this.EstimateOrganization = new List<Organization>();
            this.ScaffoldType = new List<ScaffoldType>(); 
            this.Scaffold = new List<Scaffold>();
        } 

        public Int64 Id { get; set; }
        public string Type { get; set; }
        

        public string RequestedBy { get; set; }
        public DateTime RevisionDate { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Trade { get; set; }
        public string PointOfContact { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime RequiredUntil { get; set; }
        
        public virtual ICollection<WorkPackage> WorkPackage { get; set; }
        public Int64 WorkPackageId { get; set; }

        public virtual ICollection<Area> Area { get; set; }
        public Int64 AreaId { get; set; }

        public virtual ICollection<ProcessChainType> ProcessChainType { get; set; }
        public Int64 ProcessSeqId { get; set; }

        public bool Finalized { get; set; }

        public virtual ICollection<Contract> Contract { get; set; }
        public int ContractId { get; set; }

        public virtual ICollection<Organization> EstimateOrganization { get; set; }
        public Int64 EstimateOrganizationId { get; set; }

        public virtual ICollection<ScaffoldType> ScaffoldType { get; set; }
        public Int64 ScaffoldTypeId { get; set; }

        public virtual ICollection<Scaffold> Scaffold { get; set; }
        public Int64 ScaffoldId { get; set; }

    }
}