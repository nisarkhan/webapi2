using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using atpac.webapi.Models;
using System.Collections.Generic;
using atpac.webapi.Helper;
using System;

namespace atpac.webapi.Controllers.Api
{
    public class WorkOrderRequestsController : BaseApiController
    {
        WorkOrderRequestHelper helper = new WorkOrderRequestHelper();
        // GET api/WorkOrderRequests
        public IQueryable<WorkOrderRequest> GetWorkOrderRequests()
        {
            List<WorkOrderRequest> worList = helper.QueryAllOnWorkOrderRequest();
            return worList.AsQueryable();
        }

        // GET api/WorkOrderRequest/5
        [Route("api/WorkOrderRequest/{id}")]
        [ResponseType(typeof(Organization))]
        public IHttpActionResult GetWorkOrderRequest(int id)
        {
            string SQL = "SELECT * FROM Work_Order_Request where id = " + id;
            List<WorkOrderRequest> worList = helper.WorkOrderRequestById(SQL);
            if (worList == null)
            {
                return NotFound();
            }
            return Ok(worList);
        } 
    }
}