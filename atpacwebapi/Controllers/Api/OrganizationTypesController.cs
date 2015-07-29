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
    public class OrganizationTypesController : BaseApiController
    {
        OrganizationTypeHelper helper = new OrganizationTypeHelper();
        // GET api/customers
        public IQueryable<OrganizationType> GetOrganizationTypes()
        {
            List<OrganizationType> orgList = helper.QueryAllOnOrganizationType();
            return orgList.AsQueryable();
        }

        // GET api/customers/5
        [Route("api/OrganizationType/{id}")]
        [ResponseType(typeof(OrganizationType))]
        public IHttpActionResult GetOrganizationType(string id)
        {
            OrganizationType org = helper.QueryOnOrganizationTypeBy(id);
            if (org == null)
            {
                return NotFound();
            }
            return Ok(org);
        }

        [Route("api/CreateOrganizationType")]
        [ResponseType(typeof(OrganizationType))]
        public IHttpActionResult PostOrganization(OrganizationType organizationType)
        {
            if (!ModelState.IsValid || organizationType == null)
            {
                return BadRequest(ModelState);
            }

            helper.CreateOrganizationType(organizationType);

            return CreatedAtRoute("DefaultApi", new { controller = "OrganizationType", name = organizationType.Name }, organizationType);
        }

        // PUT api/customers/5
        [ResponseType(typeof(void))]
        [Route("api/UpdateOrganizationType/{id}")]
        public IHttpActionResult PutOrganization(string id, OrganizationType organizationType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organizationType.Name)
            {
                return BadRequest();
            }

            try
            {
                object obj = helper.UpdateOrganizationType(id, organizationType);
                organizationType.Name = obj.ToString();
                return CreatedAtRoute("DefaultApi", new { controller = "Organization", id = obj }, organizationType);
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
        [Route("api/DeleteOrganizationTypeReturnList/{id}")]
        public IHttpActionResult DeleteOrganizationReturnList(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<OrganizationType> org = helper.DeleteOrganizationTypeReturnList(name);
            if (org == null)
            {
                return NotFound();
            }
            return Ok(org);
        }

        // DELETE api/customers/5
        [ResponseType(typeof(Organization))]
        [Route("api/DeleteOrganizationType/{id}")]
        public IHttpActionResult DeleteOrganizationType(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool success = helper.DeleteOrganizationType(name);
            return Ok(success);
        } 
    }
}