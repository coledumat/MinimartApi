using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimartApi;
using MinimartApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using MinimartApi.Models;

namespace MinimartApi.Tests.Controllers
{
    [TestClass]
    public class MinimartControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Disponer
            MinimartController controller = new MinimartController();

            // Actuar
            //IEnumerable<string> result = controller.Get();
            List<MinimartProduct> result =  controller.GetProducts( categoryName : "SODA" ).ToList();
                    
            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count());
            Assert.AreEqual("Coffee flavoured milk", result.ElementAt(0));
            Assert.AreEqual("Nuke-Cola", result.ElementAt(1));

            //Actuar
            result = controller.GetProducts(productName: "slur").ToList();

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());


            //Actuar
            result = controller.GetProducts(lowStock: true).ToList();

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

        }

        [TestMethod]
        public void GetById()
        {
            // Disponer
            ValuesController controller = new ValuesController();

            // Actuar
            string result = controller.Get(5);

            // Declarar
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Disponer
            ValuesController controller = new ValuesController();

            // Actuar
            controller.Post("value");

            // Declarar
        }

        [TestMethod]
        public void Put()
        {
            // Disponer
            ValuesController controller = new ValuesController();

            // Actuar
            controller.Put(5, "value");

            // Declarar
        }

        [TestMethod]
        public void Delete()
        {
            // Disponer
            ValuesController controller = new ValuesController();

            // Actuar
            controller.Delete(5);

            // Declarar
        }
    }
}
