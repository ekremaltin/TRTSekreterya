using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TRTSekreterya.Models;

namespace TRTSekreterya.Controllers
{
    public class kisisController : Controller
    {
        private RandevuEntities db = new RandevuEntities();

        // GET: kisis
        public ActionResult Index(int page = 1, string sort = "kisiAdi", string sortdir = "asc", string search = "")
        {
            

                int pageSize = 10;
                int totalRecord = 0;
                if (page < 1) page = 1;
                int skip = (page * pageSize) - pageSize;
                var data = GetKisiList(search, sort, sortdir, skip, pageSize, out totalRecord);
                ViewBag.TotalRows = totalRecord;
                ViewBag.search = search;
                return View(data);            
           
        }
        public List<kisi> GetKisiList(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            var v = (from a in db.kisis
                     where
                             a.kisiAdi.Contains(search) ||
                             a.kisiSoyadi.Contains(search) ||
                             a.kisiMeslek.Contains(search) ||
                             a.kisiUnvan.Contains(search)
                     select a);
            v = v.OrderBy(sort + " " + sortdir);
            totalRecord = v.Count();
            v = v.OrderBy(sort + " " + sortdir);
            if (pageSize > 0)
            {
                v = v.Skip(skip).Take(pageSize);
            }
            return v.ToList();
        }
        public JsonResult getKisiler()
        {
            var liste = new SelectList(db.kisis, "kisiID", "kisiAdi");
            return new JsonResult { Data = liste, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getBirimKisiler(int? id)
        {
            var liste = new SelectList(db.kisis.Where(m => m.birimID == id), "kisiID", "kisiAdi");
            var trueList = db.kisis.Where(m => m.birimID == id && m.kisiTakvimKilit == true).Select(m => m.kisiID).ToList();
            return new JsonResult { Data = new { liste = liste, trueList = trueList }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ChangeTakvimKey(int[] ids)
        {
            List<int> diziList = ids.ToList();
            var status = false;
            using (RandevuEntities db = new RandevuEntities())
            {
                int firstKisiID = ids[0];
                var birimID = db.kisis.Find(firstKisiID).birimID;
                var birimTrueList = db.kisis.Where(m => m.kisiTakvimKilit == true && m.birimID == birimID).ToList();
                foreach (var item in birimTrueList)
                {
                    if (ids.Contains(item.kisiID) == false)
                    {
                        item.kisiTakvimKilit = false;
                    }
                    else
                    {
                        diziList.Remove(item.kisiID);
                    }
                }
                for (int i = 0; i < diziList.Count(); i++)
                {
                    int id = diziList[i];
                    db.kisis.Where(m => m.kisiID == id).FirstOrDefault().kisiTakvimKilit = true;
                }
                db.SaveChanges();
                status = true;
                return new JsonResult { Data = new { status = status } };
            }
        }

        // GET: kisis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kisi kisi = db.kisis.Find(id);
            if (kisi == null)
            {
                return HttpNotFound();
            }
            return View(kisi);
        }

        // GET: kisis/Create
        public ActionResult Create()
        {
            ViewBag.birimID = new SelectList(db.birims, "birimID", "birimAdi");
            ViewBag.kisiSirketID = new SelectList(db.sirkets, "sirketID", "sirketAdi");
            return View();
        }

        // POST: kisis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kisiID,kisiAdi,kisiSoyadi,kisiMeslek,kisiUnvan,kisiKayitTarihi,kisiEkBilgi,kisiSirketID,kisiTakvimKilit,birimID")] kisi kisi)
        {
            if (ModelState.IsValid)
            {
                db.kisis.Add(kisi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.birimID = new SelectList(db.birims, "birimID", "birimAdi", kisi.birimID);
            ViewBag.kisiSirketID = new SelectList(db.sirkets, "sirketID", "sirketAdi", kisi.kisiSirketID);
            return View(kisi);
        }

        // GET: kisis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kisi kisi = db.kisis.Find(id);
            if (kisi == null)
            {
                return HttpNotFound();
            }
            ViewBag.birimID = new SelectList(db.birims, "birimID", "birimAdi", kisi.birimID);
            ViewBag.kisiSirketID = new SelectList(db.sirkets, "sirketID", "sirketAdi", kisi.kisiSirketID);
            return View(kisi);
        }

        // POST: kisis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kisiID,kisiAdi,kisiSoyadi,kisiMeslek,kisiUnvan,kisiKayitTarihi,kisiEkBilgi,kisiSirketID,kisiTakvimKilit,birimID")] kisi kisi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kisi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.birimID = new SelectList(db.birims, "birimID", "birimAdi", kisi.birimID);
            ViewBag.kisiSirketID = new SelectList(db.sirkets, "sirketID", "sirketAdi", kisi.kisiSirketID);
            return View(kisi);
        }

        // GET: kisis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kisi kisi = db.kisis.Find(id);
            if (kisi == null)
            {
                return HttpNotFound();
            }
            return View(kisi);
        }

        // POST: kisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kisi kisi = db.kisis.Find(id);
            db.kisis.Remove(kisi);
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
