using GestioneIndirizzi.Controllers;
using GestioneIndirizzi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class BillingAddressTest
    {
        private GestioneIndirizziContext context;
        private BillingAddressesController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<GestioneIndirizziContext>()
                .UseInMemoryDatabase(databaseName: "Context")
                .Options;

            context = new GestioneIndirizziContext(options, null);
            setTenantsContext();
            setBillingAddressesContext();
        }


        [TestMethod]
        public void GetBillingAddressesTestMethod()
        {
            controller = new BillingAddressesController(context);
            IActionResult actionResult = controller.BillingAddresses(0, 1, "Gerardo");
            OkObjectResult result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreNotEqual(((List<BillingAddress>)result.Value).Count, 0);
            controller.Dispose();
        }

        [TestMethod]
        public void AddBillingAddressesTestMethod()
        {
            controller = new BillingAddressesController(context);
            BillingAddress billingAddress = new BillingAddress()
            {
                address = "via pioppi",
                city = "battipaglia",
                first_name = "Gerardo",
                last_name = "Longo",
                country = "Italy",
                company = "",
                postal_code = "84091",
                phone = "33365987",
                province = "SA",
                email = "gerry@gmail.com",
                fiscal_code = "LNGGRD89A17A717B",
                vat_code = "LNGGRD89A17A717B",
            };

            IActionResult actionResult = controller.BillingAddresses(billingAddress);
            OkObjectResult result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);

            string name = "Gerardo";
            BillingAddress billingAddressAdded = result.Value as BillingAddress;

            Assert.AreEqual(billingAddressAdded.first_name, name);
            controller.Dispose();
        }

        [TestMethod]
        public void PatchBillingAddressesTestMethod()
        {
            controller = new BillingAddressesController(context);
            BillingAddress billingAddress = new BillingAddress()
            {
                country = "Germany",
            };


            IActionResult actionResult = controller.BillingAddresses(5, billingAddress);
            OkObjectResult result = actionResult as OkObjectResult;
            BillingAddress billingAddressUpdated = result.Value as BillingAddress;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(billingAddressUpdated.country, billingAddress.country);
            controller.Dispose();
        }

        [TestMethod]
        public void PatchBillingAddressesNotFoundTestMethod()
        {
            controller = new BillingAddressesController(context);
            BillingAddress shippingAddress = new BillingAddress()
            {
                country = "Germany",
            };


            IActionResult actionResult = controller.BillingAddresses(34, shippingAddress);
            BadRequestObjectResult result = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 400);
            controller.Dispose();
        }

        [TestMethod]
        public void DeleteBillingAddressesTestMethod()
        {
            controller = new BillingAddressesController(context);
            IActionResult actionResult = controller.BillingAddresses(1);
            OkObjectResult result = actionResult as OkObjectResult;
            string resultValue = "operation successful";
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, resultValue);
            controller.Dispose();
        }

        [TestMethod]
        public void DeleteBillingAddressesNotValidIdTestMethod()
        {
            controller = new BillingAddressesController(context);
            IActionResult actionResult = controller.BillingAddresses(-10);
            BadRequestObjectResult result = actionResult as BadRequestObjectResult;
            string resultValue = "Not a valid shipping address id";
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 400);
            Assert.AreEqual(result.Value, resultValue);
            controller.Dispose();
        }

        [TestMethod]
        public void DeleteBillingAddressesNotFoundTestMethod()
        {
            controller = new BillingAddressesController(context);
            IActionResult actionResult = controller.BillingAddresses(10);
            BadRequestObjectResult result = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 400);
            controller.Dispose();
        }


        [TestCleanup]
        public void TestCleanup()
        {
            context.Dispose();
        }

        private void setTenantsContext()
        {
            Tenant tenant1 = new Tenant();
            tenant1.tenant_id = "Test1";
            Tenant tenant2 = new Tenant();
            tenant2.tenant_id = "Pippo-Longo";
            context.Tenant.Add(tenant1);
            context.Tenant.Add(tenant2);
            context.SaveChanges();
        }

        private void setBillingAddressesContext()
        {
            BillingAddress billingAddress = new BillingAddress()
            {
                label = "Gerardo-Longo-1324asdasd654",
                address = "via pioppi",
                city = "battipaglia",
                first_name = "Gerardo",
                last_name = "Longo",
                country = "Italy",
                company = "",
                postal_code = "84091",
                phone = "33365987",
                province = "SA",
                email = "gerry@gmail.com",
                fiscal_code = "LNGGRD89A17A717B",
                vat_code = "LNGGRD89A17A717B",
                tenant_id = 1,
            };

            context.BillingAddress.Add(billingAddress);
            context.SaveChanges();
        }
    }


}
