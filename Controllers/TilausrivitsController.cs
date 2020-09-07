using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB.Models;

namespace WebAppTilausDB.Controllers
{
    public class TilausrivitsController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        // GET: Tilausrivits
        public ActionResult Index()
        {
            var tilausrivit = db.Tilausrivit.Include(t => t.Tilaukset).Include(t => t.Tuotteet);
            return View(tilausrivit.ToList());
        }

        // GET: Tilausrivits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null)
            {
                return HttpNotFound();
            }
            return View(tilausrivit);
        }

        // GET: Tilausrivits/Create
        public ActionResult Create()
        {
            ViewBag.TilausID = new SelectList(db.Tilaukset, "TilausID", "Toimitusosoite");
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi");
            return View();
        }

        // POST: Tilausrivits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Ahinta")] Tilausrivit tilausrivit)
        {
            if (ModelState.IsValid)
            {
                db.Tilausrivit.Add(tilausrivit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TilausID = new SelectList(db.Tilaukset, "TilausID", "Toimitusosoite", tilausrivit.TilausID);
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", tilausrivit.TuoteID);
            return View(tilausrivit);
        }

        // GET: Tilausrivits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null)
            {
                return HttpNotFound();
            }
            ViewBag.TilausID = new SelectList(db.Tilaukset, "TilausID", "Toimitusosoite", tilausrivit.TilausID);
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", tilausrivit.TuoteID);
            return View(tilausrivit);
        }

        // POST: Tilausrivits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Ahinta")] Tilausrivit tilausrivit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilausrivit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TilausID = new SelectList(db.Tilaukset, "TilausID", "Toimitusosoite", tilausrivit.TilausID);
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", tilausrivit.TuoteID);
            return View(tilausrivit);
        }

        // GET: Tilausrivits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null)
            {
                return HttpNotFound();
            }
            return View(tilausrivit);
        }

        // POST: Tilausrivits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            db.Tilausrivit.Remove(tilausrivit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
