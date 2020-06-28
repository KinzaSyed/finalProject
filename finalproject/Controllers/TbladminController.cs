using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using finalproject.Models;

namespace finalproject.Controllers
{
    public class TbladminController : Controller
    {
        private lastDbEntities db = new lastDbEntities();

        public ActionResult TotalOfEveryThing()
        {
            var totalbooks = (from books in db.Tbl_Books
                              select books).Count();
            ViewBag.totalbooks = totalbooks;

            var totalebooks = (from ebooks in db.Ebooks_db
                              select ebooks).Count();
            ViewBag.totalebooks = totalebooks;
            var totalmembers = (from member in db.tbl_member
                              select member).Count();
            ViewBag.totalmembers = totalmembers;
            var totalvendors = (from vendor in db.Tbl_Vendorr
                              select vendor).Count();
            ViewBag.totalvendors = totalvendors;




            return View();
        }



        public ActionResult PersonalInfoAdmin()
        {
            Tbl_admin m = null;
            if (Session["Admin_id"] != null)
            {
                int Admin_id = Convert.ToInt32(Session["Admin_id"].ToString());
                m = db.Tbl_admin.Where(x => x.Admin_id == Admin_id).SingleOrDefault();


            }

            return View(m);
        }


        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Admin_id"] != null && Session["Admin_email"] != null)
            {
                return RedirectToAction("AdminPanel", "Tbladmin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(String email, String pass)
        {
            //int id = db.Tbl_admin.Count();
            //id += 1;

            Tbl_admin model = db.Tbl_admin.Where(x => x.Admin_email == email && x.Admin_password == pass).SingleOrDefault();

            if (model != null)
            {
                Session["Admin_email"] = model.Admin_email;
                Session["Admin_id"] = model.Admin_id;
                return RedirectToAction("PersonalInfoAdmin", "Tbladmin");

            }
           
            return View("Invalid Password");


        }
        public ActionResult AdminPanel()
        {
            Tbl_admin m = null;
            if (Session["Admin_id"] != null)
            {
                int Admin_id = Convert.ToInt32(Session["Admin_id"].ToString());
                m = db.Tbl_admin.Where(x => x.Admin_id == Admin_id).SingleOrDefault();
                var totalbooks = (from books in db.Tbl_Books
                                  select books).Count();
                ViewBag.totalbooks = totalbooks;

                var totalebooks = (from ebooks in db.Ebooks_db
                                   select ebooks).Count();
                ViewBag.totalebooks = totalebooks;
                var totalmembers = (from member in db.tbl_member
                                    select member).Count();
                ViewBag.totalmembers = totalmembers;
                var totalvendors = (from vendor in db.Tbl_Vendorr
                                    select vendor).Count();
                ViewBag.totalvendors = totalvendors;


            }
            else
            {
                return RedirectToAction("Login");
            }

            return View(m);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Tbladmin");
        }
        
        // GET: Tbladmin
        public ActionResult Index()
        {
            return View(db.Tbl_admin.ToList());
        }

        // GET: Tbladmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_admin tbl_admin = db.Tbl_admin.Find(id);
            if (tbl_admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_admin);
        }

        // GET: Tbladmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tbladmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Admin_id,Admin_name,Admin_email,Admin_password,Admin_contact,Admin_status")] Tbl_admin tbl_admin)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_admin.Add(tbl_admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_admin);
        }

        // GET: Tbladmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_admin tbl_admin = db.Tbl_admin.Find(id);
            if (tbl_admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_admin);
        }

        // POST: Tbladmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Admin_id,Admin_name,Admin_email,Admin_password,Admin_contact,Admin_status")] Tbl_admin tbl_admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_admin).State = EntityState.Modified;
                db.SaveChanges();
                if (Session["Admin_id"] != null && Session["Admin_email"] != null)
                {
                    return RedirectToAction("AdminPanel", "Tbladmin");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(tbl_admin);
        }

        // GET: Tbladmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_admin tbl_admin = db.Tbl_admin.Find(id);
            if (tbl_admin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_admin);
        }

        // POST: Tbladmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_admin tbl_admin = db.Tbl_admin.Find(id);
            db.Tbl_admin.Remove(tbl_admin);
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
