using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace atpac.webapi.Models
{
    public class Estimate
    {
        public int Id { get; set; }
        public int OrganizationId  { get; set; }
        public int WorkOrderRequestId { get; set; }
        public double HoursPerMan { get; set; }
        public double NumberOfMen { get; set; }
        public DateTime EstimatedStart { get; set; }
        public string EstimatedBy { get; set; }
        public DateTime MaterialRequiredDate  { get; set; }
        public string Complexity { get; set; }
        public string ComplexityExplanation { get; set; }
        public bool HazardFlag { get; set; }
        public string EstimateComments { get; set; }
        public string SpecialInstructions { get; set; }
        public bool MultiCraftScaffold { get; set; }
        public bool EngineeringRequired { get; set; }
        public int Revision { get; set; }
        public double Rate { get; set; }
        public int HazardTypeId { get; set; }
        public string Trade  { get; set; }
        public bool Finalized { get; set; }
        public string Status { get; set; }
    }
}