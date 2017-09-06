using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TRTSekreterya.Models;
using System.DirectoryServices.AccountManagement;

namespace TRTSekreterya.Controllers
{
    public class usersController : Controller
    {
        // GET: users
        private RandevuEntities db = new RandevuEntities();

        // GET: users
        public ActionResult Index()
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                var users = db.users.Include(u => u.birim).Include(u => u.yetkiLogin);
                return View(users.ToList());
            }
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
       
            //Active Directory Bağlantısı
            
    
        [HttpPost]
        public ActionResult Login(user user)
        {

            if (ModelState.IsValid)
            {
                var usr = db.users.Where(u => u.username == user.username && u.password == user.password).FirstOrDefault();
                //using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "dcmerkez1.int.trt.net.tr"))//172.16.0.138
                //{

                //    bool isValid = pc.ValidateCredentials(usr.username, usr.password);
                //    if (isValid)
                //    {

                        if (usr != null)
                        {
                            Session["id"] = usr.userID;
                            Session["Ad"] = usr.userAdSoyad;
                            Session["yetki"] = usr.userYetkiID;
                            Session["birim"] = usr.userBirimID;
                            return RedirectToAction("Liste", "Kisi");
                        }
                        else
                        {
                            ModelState.AddModelError("", "my db Kullanıcı adı veya şifre hatalı.");
                        }                        
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("","Active Directory'e Kayıtlı OLMAYAN Kullanıcı");
                    //}
                //}                
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user user = db.users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            return RedirectToAction("Login");

        }

        // GET: users/Create
        public ActionResult Create()
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                ViewBag.userBirimID = new SelectList(db.birims, "birimID", "birimAdi");
                ViewBag.userYetkiID = new SelectList(db.yetkiLogins, "yetkiID", "yetki");
                return View();
            }
            return RedirectToAction("Login");

        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,username,password,userYetkiID,userAdSoyad,userBirimID")] user user)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (ModelState.IsValid)
                {
                    db.users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.userBirimID = new SelectList(db.birims, "birimID", "birimAdi", user.userBirimID);
                ViewBag.userYetkiID = new SelectList(db.yetkiLogins, "yetkiID", "yetki", user.userYetkiID);
                return View(user);
            }
            return RedirectToAction("Login");

        }
        // GET: users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user user = db.users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.userBirimID = new SelectList(db.birims, "birimID", "birimAdi", user.userBirimID);
                ViewBag.userYetkiID = new SelectList(db.yetkiLogins, "yetkiID", "yetki", user.userYetkiID);
                return View(user);
            }
            return RedirectToAction("Login");

        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,username,password,userYetkiID,userAdSoyad,userBirimID")] user user)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.userBirimID = new SelectList(db.birims, "birimID", "birimAdi", user.userBirimID);
                ViewBag.userYetkiID = new SelectList(db.yetkiLogins, "yetkiID", "yetki", user.userYetkiID);
                return View(user);
            }
            return RedirectToAction("Login");

        }

        // GET: users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user user = db.users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            return RedirectToAction("Login");

        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["yetki"] != null && (Session["yetki"].ToString() == "3" || Session["yetki"].ToString() == "4"))
            {
                user user = db.users.Find(id);
                db.users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");

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