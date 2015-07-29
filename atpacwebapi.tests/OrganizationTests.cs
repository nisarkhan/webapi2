using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using atpac.webapi.Models;
using System.Net.Http;
using System.Net;

namespace atpacwebapi.tests
{
    [TestClass]
    public class OrganizationTests
    {
        HttpClient client = null;
        string baseAddress = null;

        public OrganizationTests()
        {
            baseAddress = "http://localhost:18965/";

            client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
        }

        [TestMethod]
        public void CreateNewOrganization_UriLinkGeneration_BasedOnAttributeRouteName()
        {
            //Organization org = new Organization();

            //org.Type = "type";
            //org.Name = "test";
            //org.Description = "desc";

            //HttpResponseMessage response = client.PostAsJsonAsync<Organization>("api/Organizations", org).Result;

            //Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            //Product createdProduct = response.Content.ReadAsAsync<Product>().Result;

            //Assert.AreEqual(
            //    expected: string.Format("{0}api/products/{1}", baseAddress, createdProduct.Id),
            //    actual: response.Headers.Location.ToString(),
            //    ignoreCase: true);
        }
    }
}
