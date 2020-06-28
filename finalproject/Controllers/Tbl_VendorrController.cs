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
    public class Tbl_VendorrController : Controller
    {
        private lastDbEntities db = new lastDbEntities();

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Vendor_id"] != null && Session["Vendor_email"] != null)
            {
                return RedirectToAction("VendorPanel", "Tbl_Vendorr");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(String email, String pass)
        {


            Tbl_Vendorr model = db.Tbl_Vendorr.Where(x => x.Vendor_email == email && x.Vendor_password == pass).SingleOrDefault();

            if (model != null)
            {
                Session["Vendor_email"] = model.Vendor_email;
                Session["Vendor_id"] = model.Vendor_id;
                return RedirectToAction("VendorPanel", "Tbl_Vendorr");

            }
            else
            {

            }
            return View();


        }
        public ActionResult VendorPanel()
        {
            Tbl_Vendorr m = null;
            if (Session["Vendor_id"] != null)
            {
                int Vendor_id = Convert.ToInt32(Session["Vendor_id"].ToString());
                m = db.Tbl_Vendorr.Where(x => x.Vendor_id == Vendor_id).SingleOrDefault();


            }

            return View(m);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Tbl_Vendorr");
        }






        public ActionResult Totalven()
        {
            var tbl_Vendorr = db.Tbl_Vendorr.Include(t => t.Tbl_admin);
            return View(tbl_Vendorr.ToList());
        }




        // GET: Tbl_Vendorr
        public ActionResult Index()
        {
            var tbl_Vendorr = db.Tbl_Vendorr.Include(t => t.Tbl_admin);
            return View(tbl_Vendorr.ToList());
        }

        // GET: Tbl_Vendorr/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Vendorr tbl_Vendorr = db.Tbl_Vendorr.Find(id);
            if (tbl_Vendorr == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Vendorr);
        }

        // GET: Tbl_Vendorr/Create
        public ActionResult Create()
        {
            int admin_id = Convert.ToInt32(Session["Admin_id"].ToString());
            ViewBag.Admin_id = new SelectList(db.Tbl_admin, "Admin_id", "Admin_name");
            ViewBag.Admin_id = admin_id;
            var adminname = (from n in db.Tbl_admin
                             where n.Admin_id == admin_id
                             select n).Single().Admin_name;
            ViewBag.adminname = adminname;
            return View();
        }

        // POST: Tbl_Vendorr/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vendor_id,Vendor_name,Vendor_email,Vendor_password,Vendor_contactno,Vendor_status,Vendor_ShopAdr,Admin_id")] Tbl_Vendorr tbl_Vendorr)
        {
            var email = tbl_Vendorr.Vendor_email;
            var emailcheck = db.Tbl_Vendorr.Where(b => b.Vendor_email == email).Any();
            var emailcheck1 = db.tbl_member.Where(b => b.mem_email == email).Any();
            var emailcheck2 = db.Tbl_admin.Where(b => b.Admin_email == email).Any();

            if (emailcheck == false && emailcheck1 == false && emailcheck2 == false)
            {



                if (ModelState.IsValid)
                {

                    int admina_id = Convert.ToInt32(Session["Admin_id"].ToString());
                    tbl_Vendorr.Admin_id = admina_id;
                    db.Tbl_Vendorr.Add(tbl_Vendorr);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                int admin_id = Convert.ToInt32(Session["Admin_id"].ToString());
                //ViewBag.Admin_id = new SelectList(db.Tbl_admin, "Admin_id", "Admin_name", tbl_Vendorr.Admin_id);
                

                return View(tbl_Vendorr);
            }
            else
            {
                Response.Write("<script>alert('Email Already registered';</script>");
            }
            ViewBag.Admin_id = new SelectList(db.Tbl_admin, "Admin_id", "Admin_name", tbl_Vendorr.Admin_id);
            return View(tbl_Vendorr);

        }

        // GET: Tbl_Vendorr/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Vendorr tbl_Vendorr = db.Tbl_Vendorr.Find(id);
            if (tbl_Vendorr == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_id = new SelectList(db.Tbl_admin, "Admin_id", "Admin_name", tbl_Vendorr.Admin_id);
            return View(tbl_Vendorr);
        }

        // POST: Tbl_Vendorr/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vendor_id,Vendor_name,Vendor_email,Vendor_password,Vendor_contactno,Vendor_status,Vendor_ShopAdr,Admin_id")] Tbl_Vendorr tbl_Vendorr)
        {
            if (ModelState.IsValid)
            {
                int admina_id = Convert.ToInt32(Session["Admin_id"].ToString());
                tbl_Vendorr.Admin_id = admina_id;
                db.Entry(tbl_Vendorr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_id = new SelectList(db.Tbl_admin, "Admin_id", "Admin_name", tbl_Vendorr.Admin_id);
            return View(tbl_Vendorr);
        }

        // GET: Tbl_Vendorr/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Vendorr tbl_Vendorr = db.Tbl_Vendorr.Find(id);
            if (tbl_Vendorr == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Vendorr);
        }

        // POST: Tbl_Vendorr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Vendorr tbl_Vendorr = db.Tbl_Vendorr.Find(id);
            db.Tbl_Vendorr.Remove(tbl_Vendorr);
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
