using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimartApi;
using MinimartApi.Controllers;
using System.Web.Mvc;

namespace MinimartApi.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Disponer
            HomeController controller = new HomeController();

            // Actuar
            ViewResult result = controller.Index() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("Minimart Home Page", result.ViewBag.Title);
        }
    }
}
