using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinalL2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Dogs()
        {
            ViewBag.Message = "Cachorros";

            return View();
        }

        public ActionResult Dogs2()
        {
            ViewBag.Message = "Cachorros";

            return View();
        }

        public ActionResult Cats()
        {
            ViewBag.Message = "Gatos";

            return View();
        }
    }
}