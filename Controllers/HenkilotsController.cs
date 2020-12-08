using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB.Models;
using PagedList;

namespace WebAppTilausDB.Controllers
{
    public class HenkilotsController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        // GET: Henkilots
        //BareMinimum Pagedlistille
        public ActionResult Index(int? page, int? pagesize)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                //ViewBag.CurrentSort = sortOrder;
                //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                var henkilot = db.Henkilot;
                //switch (sortOrder)
                //{
                //    case "name_desc":
                //        henkilot = henkilot.OrderByDescending(a => a.Sukunimi);
                //        break;
                //    default:
                //        henkilot = henkilot.OrderBy(a => a.Sukunimi);
                //        break;
                //}
                int pageSize = (pagesize ?? 3); //Tämä palauttaa sivukoon taikka jos pagesize on null, niin palauttaa koon 10 riviä per sivu
                int pageNumber = (page ?? 1); //Tämä palauttaa sivunumeron taikka jos page on null, niin palauttaa numeron yksi
                return View(henkilot.ToPagedList(pageNumber, pageSize));

            }
        }

        // GET: Henkilots/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Henkilot henkilot = db.Henkilot.Find(id);
                if (henkilot == null)
                {
                    return HttpNotFound();
                }
                return View(henkilot);
            }
        }

        // GET: Henkilots/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                return View();
            }
        }

        // POST: Henkilots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Henkilo_id,Etunimi,Sukunimi,Osoite,Esimies,Postinumero,Sahkoposti")] Henkilot henkilot)
        {
            if (ModelState.IsValid)
            {
                db.Henkilot.Add(henkilot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(henkilot);
        }

        // GET: Henkilots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Henkilot henkilot = db.Henkilot.Find(id);
                if (henkilot == null)
                {
                    return HttpNotFound();
                }
                return View(henkilot);
            }
        }

        // POST: Henkilots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Henkilo_id,Etunimi,Sukunimi,Osoite,Esimies,Postinumero,Sahkoposti")] Henkilot henkilot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(henkilot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(henkilot);
        }

        // GET: Henkilots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Henkilot henkilot = db.Henkilot.Find(id);
                if (henkilot == null)
                {
                    return HttpNotFound();
                }
                return View(henkilot);
            }
        }

        // POST: Henkilots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Henkilot henkilot = db.Henkilot.Find(id);
            db.Henkilot.Remove(henkilot);
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
