using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppTilausDB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Tietoja Sovelluksesta.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Täältä löydät yhteystietoni.";

            return View();
        }
    }
}