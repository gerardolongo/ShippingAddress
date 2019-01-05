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
    public class ShippingAddressTest
    {
        private GestioneIndirizziContext context;
        private ShippingAddressesController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<GestioneIndirizziContext>()
                .UseInMemoryDatabase(databaseName: "Context")
                .Options;

            context = new GestioneIndirizziContext(options, null);
            setTenantsContext();
            setShippingAddressesContext();
        }


        [TestMethod]
        public void GetShippingAddressesTestMethod()
        {
            controller = new ShippingAddressesController(context);
            IActionResult actionResult = controller.ShippingAddresses(0, 1, "Gerardo");
            OkObjectResult result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreNotEqual(((List<ShippingAddress>)result.Value).Count, 0);
            controller.Dispose();
        }

        [TestMethod]
        public void AddShippingAddressesTestMethod()
        {
            controller = new ShippingAddressesController(context);
            ShippingAddress shippingAddress = new ShippingAddress()
            {
                address = "via pioppi",
                city = "battipaglia",
                first_name = "Gerardo",
                last_name = "Longo",
                country = "Italy",
                company = "",
                postal_code = "84091",
                phone = "33365987",
                province = "SA"
            };

            IActionResult actionResult = controller.ShippingAddresses(shippingAddress);
            OkObjectResult result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);

            string name = "Gerardo";
            ShippingAddress shippingAddressAdded = result.Value as ShippingAddress;

            Assert.AreEqual(shippingAddressAdded.first_name, name);
            controller.Dispose();
        }

        [TestMethod]
        public void PatchShippingAddressesTestMethod()
        {
            controller = new ShippingAddressesController(context);
            ShippingAddress shippingAddress = new ShippingAddress()
            {
                country = "Germany",
            };


            IActionResult actionResult = controller.ShippingAddresses(1, shippingAddress);
            OkObjectResult result = actionResult as OkObjectResult;
            ShippingAddress shippingAddressUpdated = result.Value as ShippingAddress;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(shippingAddressUpdated.country, shippingAddress.country);
            controller.Dispose();
        }

        [TestMethod]
        public void PatchShippingAddressesNotFoundTestMethod()
        {
            controller = new ShippingAddressesController(context);
            ShippingAddress shippingAddress = new ShippingAddress()
            {
                country = "Germany",
            };


            IActionResult actionResult = controller.ShippingAddresses(34, shippingAddress);
            BadRequestObjectResult result = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 400);
            controller.Dispose();
        }


        [TestMethod]
        public void DeleteShippingAddressesTestMethod()
        {
            controller = new ShippingAddressesController(context);
            IActionResult actionResult = controller.ShippingAddresses(1);
            OkObjectResult result = actionResult as OkObjectResult;
            string resultValue = "operation successful";
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, resultValue);
            controller.Dispose();
        }

        [TestMethod]
        public void DeleteShippingAddressesNotValidIdTestMethod()
        {
            controller = new ShippingAddressesController(context);
            IActionResult actionResult = controller.ShippingAddresses(-10);
            BadRequestObjectResult result = actionResult as BadRequestObjectResult;
            string resultValue = "Not a valid shipping address id";
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 400);
            Assert.AreEqual(result.Value, resultValue);
            controller.Dispose();
        }

        [TestMethod]
        public void DeleteShippingAddressesNotFoundTestMethod()
        {
            controller = new ShippingAddressesController(context);
            IActionResult actionResult = controller.ShippingAddresses(10);
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

        private void setShippingAddressesContext()
        {
            ShippingAddress shippingAddress1 = new ShippingAddress()
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
                tenant_id = 1
            };

            context.ShippingAddress.Add(shippingAddress1);
            context.SaveChanges();
        }
    }


}
