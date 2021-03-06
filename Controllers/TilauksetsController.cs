﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB.Models;
using WebAppTilausDB.ViewModels;
using PagedList;
namespace WebAppTilausDB.Controllers
{
    public class TilauksetsController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        // GET: Tilauksets
        //BareMinimum Pagedlistille
        public ActionResult Index(string sortOrder, int? page, int? pagesize)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                var tilaukset = db.Tilaukset.Include(t => t.Asiakkaat).Include(t => t.Postitoimipaikat);
                switch (sortOrder)
                {
                    case "name_desc":
                        tilaukset = tilaukset.OrderByDescending(a => a.Asiakkaat.Nimi);
                        break;
                    default:
                        tilaukset = tilaukset.OrderBy(a => a.Asiakkaat.Nimi);
                        break;
                }
                int pageSize = (pagesize ?? 10); //Tämä palauttaa sivukoon taikka jos pagesize on null, niin palauttaa koon 10 riviä per sivu
                int pageNumber = (page ?? 1); //Tämä palauttaa sivunumeron taikka jos page on null, niin palauttaa numeron yksi
                return View(tilaukset.ToPagedList(pageNumber, pageSize));
               
            }
        }
        public ActionResult _TilausRivit(int? tilausID)
        {
            if ((tilausID == null) || (tilausID == 0) )
            {
                return HttpNotFound();
            }
            else
            {
            var tilausRiviLista = from tr in db.Tilausrivit
                                  join t in db.Tilaukset on tr.TilausID equals t.TilausID
                                  where t.TilausID == tilausID
                                  join tu in db.Tuotteet on tr.TuoteID equals tu.TuoteID
                                  select new TilausData
                                  {
                                      AsiakasID = (int)t.AsiakasID,
                                      TilausID = (int)t.TilausID,
                                      TilausriviID = tr.TilausriviID,
                                      TuoteID = (int)tr.TuoteID,
                                      Maara = (int)tr.Maara,
                                      Ahinta = (float)tr.Ahinta,
                                      Postinumero = t.Postinumero,
                                      Nimi = tu.Nimi

                                  };
            return PartialView(tilausRiviLista);

            }
        }

        // GET: Tilauksets/Details/5
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
                Tilaukset tilaukset = db.Tilaukset.Find(id);
                if (tilaukset == null)
                {
                    return HttpNotFound();
                }
                return View(tilaukset);
            }
        }

        // GET: Tilauksets/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi");
                ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka");
                return View();
            }
        }

        // POST: Tilauksets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                db.Tilaukset.Add(tilaukset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // GET: Tilauksets/Edit/5
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
                Tilaukset tilaukset = db.Tilaukset.Find(id);
                if (tilaukset == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
                ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
                return View(tilaukset);
            }
        }

        // POST: Tilauksets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilaukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", tilaukset.Postinumero);
            return View(tilaukset);
        }

        // GET: Tilauksets/Delete/5
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
                Tilaukset tilaukset = db.Tilaukset.Find(id);
                if (tilaukset == null)
                {
                    return HttpNotFound();
                }
                return View(tilaukset);
            }
        }

        // POST: Tilauksets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(tilaukset);
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

        public ActionResult Paivittaismyynti()
        {
            string paivatLista;
            string myynnitLista;
            List<myynnit_per_paiva> MyynnitLista = new List<myynnit_per_paiva>();

            var paivittaisMyyntiData = from m in db.myynnit_per_paiva
                                       select m;

            foreach (myynnit_per_paiva myynnit in paivittaisMyyntiData)
            {
                myynnit_per_paiva yksiMyyntiRivi = new myynnit_per_paiva();
                yksiMyyntiRivi.Viikonpäivä = myynnit.Viikonpäivä;
                yksiMyyntiRivi.Myynti = (int)myynnit.Myynti;
                //OneSalesRow.CategorySales = 11;
                MyynnitLista.Add(yksiMyyntiRivi);

            }
            paivatLista = "'" + string.Join("','", MyynnitLista.Select(n => n.Viikonpäivä).ToList()) + "'";
            myynnitLista = string.Join(",", MyynnitLista.Select(n => n.Myynti).ToList());



            ViewBag.paivaTieto = paivatLista;
            ViewBag.myyntiTieto = myynnitLista;

            string tuoteLista;
            string salesLista;
            List<kokonaismyynti_apu_taulu> TopSalesLista = new List<kokonaismyynti_apu_taulu>();

            var topSalesData = (from t in db.kokonaismyynti_apu_taulu orderby t.DailySales descending
                               select t).Take(10);
            foreach (kokonaismyynti_apu_taulu sales in topSalesData)
            {
                kokonaismyynti_apu_taulu yksTopSales = new kokonaismyynti_apu_taulu();
                yksTopSales.Nimi = sales.Nimi;
                yksTopSales.DailySales = (int)sales.DailySales;
                TopSalesLista.Add(yksTopSales);
            }

            tuoteLista = "'" + string.Join("','", TopSalesLista.Select(n => n.Nimi).ToList()) + "'";
            salesLista = string.Join(",", TopSalesLista.Select(n => n.DailySales).ToList());

            ViewBag.tuoteTieto = tuoteLista;
            ViewBag.salesTieto = salesLista;

            return View();
        }

    }
}
