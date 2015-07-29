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
    public class OrganizationsController : BaseApiController
    {
        OrganizationHelper helper = new OrganizationHelper();
        // GET api/customers
        public IQueryable<Organization> GetOrganizations()
        {
            List<Organization> orgList = helper.QueryAllOnOrganization();
            return orgList.AsQueryable();
        }

        // GET api/customers/5
        [Route("api/Organizations/{id}")]
        [ResponseType(typeof(Organization))]
        public IHttpActionResult GetOrganization(int id)
        {
            Organization org = helper.QueryOnOrganizationBy(id);
            if (org == null)
            {
                return NotFound();
            }
            return Ok(org);
        }

        [Route("api/CreateOrganization")]
        [ResponseType(typeof(Organization))]
        public IHttpActionResult PostOrganization(Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            object obj = helper.CreateOrganization(organization);
            organization.Id = Convert.ToInt32(obj);

            return CreatedAtRoute("DefaultApi", new { controller = "Organization", id = obj }, organization);
        }

        // PUT api/customers/5
        [ResponseType(typeof(void))]
        [Route("api/UpdateOrganization/{id}")]
        public IHttpActionResult PutOrganization(int id, Organization organization)
        {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             if (id != organization.Id)
             {
                 return BadRequest();
             } 

             try
             {
                 object obj = helper.UpdateOrganization(id, organization);
                 organization.Id = Convert.ToInt32(obj);
                 return CreatedAtRoute("DefaultApi", new { controller = "Organization", id = obj }, organization);
             }
             catch (DbUpdateConcurrencyException)
             {
                 //if (!CustomerExists(id))
                 //{
                 //    return NotFound();
                 //}
                 //else
                 //{
                 //    throw;
                 //}
             }

             return StatusCode(HttpStatusCode.NoContent);
         }

        // DELETE api/customers/5
        [ResponseType(typeof(Organization))]
        [Route("api/DeleteOrganizationReturnList/{id}")]
        public IHttpActionResult DeleteOrganizationReturnList(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Organization> org = helper.DeleteOrganizationReturnList(id);
            if (org == null)
            {
                return NotFound();
            }
            return Ok(org); 
        }

        // DELETE api/customers/5
        [ResponseType(typeof(Organization))]
        [Route("api/DeleteOrganization/{id}")]
        public IHttpActionResult DeleteOrganization(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool success = helper.DeleteOrganization(id);
            return Ok(success); 
        } 
    }
}