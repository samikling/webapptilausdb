using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB.Models;

namespace WebAppFirst.Controllers
{
    public class TuotteetController : Controller
    {
        // GET: Shippers
        TilausDBEntities db = new TilausDBEntities(); //Uusi ilmentymä tietokannasta nimellä db
        public ActionResult Index()
        {
            //List<Shippers> model = db.Shippers.ToList();    //Lista nimeltä model, joka ottaa vastaan Products kategoriaan kuuluvia rivejä, jotka sijoitetaan db oliosta listana.
            ////db.Dispose();                                   //Suljetaan tietokanta yhteys

            //return View(model);                             //Palautetaan näkymälle lista model.
            var tuotteet = db.Tuotteet;
            return View(tuotteet.ToList());
        }
        public ActionResult Index2()
        {
            //List<Shippers> model = db.Shippers.ToList();    //Lista nimeltä model, joka ottaa vastaan Products kategoriaan kuuluvia rivejä, jotka sijoitetaan db oliosta listana.
            ////db.Dispose();                                   //Suljetaan tietokanta yhteys

            //return View(model);                             //Palautetaan näkymälle lista model.
            var tuotteet = db.Tuotteet;
            return View(tuotteet.ToList());
        }
        [HttpGet]
        public ActionResult Edit(int? id) //parametri tutkitaan ja etsitään taulusta vastaavaa riviä. Jos vastaavuus löytyy, Näytetään Edit.cshtml sivu.
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null) return HttpNotFound(); //Jos vastaavuutta ei löydy, palautetaan sivua ei löydy virhe.
            //ViewBag.RegionID = new SelectList(db.Region, "RegionID", "RegionDescription", tuotteet.RegionID);
            return View(tuotteet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Katso https://go.microsoft.com/fwlink/?LinkId=317598
        public ActionResult Edit([Bind(Include = "TuoteID,Nimi,Ahinta,Kuva")] Tuotteet tuote) //Tämä metodi tallentaa muutokset kantaan ja paulauttaa sivun Shippers.cshtml
        {
            if (ModelState.IsValid)
            {
                db.Entry(tuote).State = EntityState.Modified;
                db.SaveChanges();
                //ViewBag.RegionID = new SelectList(db.Region, "RegionID", "RegionDescription", tuote.RegionID);
                return RedirectToAction("Index");
            }
            return View(tuote);
        }
        public ActionResult Create() //Tätä metodia kutsutaan listanäkymästä ja tämä metodi näyttää luontinäytön Create.cshtml
        {
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TuoteID,Nimi,Ahinta,Kuva")] Tuotteet tuote) //Tätä metodia kutsutaan luontinäytöltä, kun klikataan "Save".Metodi lisää uuden rivin tiedot kantaan.
        {
            if (ModelState.IsValid)
            {
                db.Tuotteet.Add(tuote);

                db.SaveChanges();
                ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", tuote.TuoteID);

                return RedirectToAction("Index");
            }
            return View(tuote);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tuotteet tuote = db.Tuotteet.Find(id);
            if (tuote == null) return HttpNotFound();
            return View(tuote);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tuotteet tuote = db.Tuotteet.Find(id);
            db.Tuotteet.Remove(tuote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id) //parametri tutkitaan ja etsitään taulusta vastaavaa riviä. Jos vastaavuus löytyy, Näytetään Edit.cshtml sivu.
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tuotteet tuote = db.Tuotteet.Find(id);
            if (tuote == null) return HttpNotFound(); //Jos vastaavuutta ei löydy, palautetaan sivua ei löydy virhe.
            return View(tuote);
        }
    }
}