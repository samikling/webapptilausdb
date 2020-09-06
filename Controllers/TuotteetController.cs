using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB.Models;

namespace WebAppTilausDB.Controllers
{
    public class TuotteetController : Controller
    {
        // GET: Tuotteet
        public ActionResult Index()
        {
            TilausDBEntities db = new TilausDBEntities(); //Uusi ilmentymä tietokantamallista
            List<Tuotteet> malli = db.Tuotteet.ToList();  //Luodaan uusi lista johon listataan kaikki tuotteet taulun osat
            db.Dispose(); //Vapautetaan muistia
            
            return View(malli); //Tulostetaan lista näytölle.
        }
    }
}