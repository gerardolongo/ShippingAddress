using GestioneIndirizzi.Controllers;
using GestioneIndirizzi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class TenantTest
    {
        private GestioneIndirizziContext context;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<GestioneIndirizziContext>()
                .UseInMemoryDatabase(databaseName: "Context")
                .Options;

            context = new GestioneIndirizziContext(options, null);
            setTenantContext();
        }


        [TestMethod]
        public void GetTanantsTestMethod()
        {
            TenantsController controller = new TenantsController(context);
            IActionResult actionResult = controller.Tenants();
            OkObjectResult result = actionResult as OkObjectResult;
            List<Tenant> tenants = result.Value as List<Tenant>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreNotEqual(tenants.Count, 0);
        }

        [TestMethod]
        public void AddTenantsTestMethod()
        {
            TenantsController controller = new TenantsController(context);
            Tenant tenant = new Tenant()
            {
                tenant_id = "pluto"
            };

            IActionResult actionResult = controller.Tenants(tenant);
            OkObjectResult result = actionResult as OkObjectResult;
            Tenant tenantResult = result.Value as Tenant;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(tenant.tenant_id, "pluto");
        }

        [TestMethod]
        public void AddTenantsExistsTestMethod()
        {
            TenantsController controller = new TenantsController(context);
            Tenant tenant = new Tenant()
            {
                tenant_id = "Gerardo-Longo"
            };

            IActionResult actionResult = controller.Tenants(tenant);
            BadRequestObjectResult result = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            context.Dispose();
        }

        private void setTenantContext()
        {
            Tenant tenant1 = new Tenant();
            tenant1.tenant_id = "Gerardo-Longo";
            Tenant tenant2 = new Tenant();
            tenant2.tenant_id = "Pippo-Longo";
            context.Tenant.Add(tenant1);
            context.Tenant.Add(tenant2);
            context.SaveChanges();
        }
    }
}
