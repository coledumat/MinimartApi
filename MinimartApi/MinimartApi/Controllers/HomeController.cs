using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinimartApi.Controllers
{
    /// <summary>
    /// example Home Controler 
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Minimart Home Page";

            return View();
        }
    }
}
