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
    public class Tbl_memberController : Controller
    {
        private lastDbEntities db = new lastDbEntities();


        public ActionResult Login()
        {
            if (Session["BUser_id"] != null && Session["BUser_email"] != null)
            {
                return RedirectToAction("UserPanel", "Tbl_member");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(String email, String pass)
        {


            tbl_member model = db.tbl_member.Where(x => x.mem_email == email && x.mem_password == pass).SingleOrDefault();

            if (model != null)
            {

                Session["mem_email"] = model.mem_email;
                Session["mem_id"] = model.mem_id;

                Session["mem_password"] = model.mem_password;
                return RedirectToAction("UserPanel", "Tbl_member");

            }
            else
            {

            }
            return View();


        }
        public ActionResult UserPanel()
        {
            tbl_member m = null;
            if (Session["mem_id"] != null)
            {
                int mem_id = Convert.ToInt32(Session["mem_id"].ToString());
                m = db.tbl_member.Where(x => x.mem_id == mem_id).SingleOrDefault();


            }

            return View(m);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Tbl_member");
        }






        // GET: Tbl_member
        public ActionResult Index()
        {
            return View(db.tbl_member.ToList());
        }

        // GET: Tbl_member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_member tbl_member = db.tbl_member.Find(id);
            if (tbl_member == null)
            {
                return HttpNotFound();
            }
            return View(tbl_member);
        }

        // GET: Tbl_member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tbl_member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mem_id,mem_name,mem_email,mem_contact,mem_password")] tbl_member tbl_member)
        {
            if (ModelState.IsValid)
            {
                db.tbl_member.Add(tbl_member);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(tbl_member);
        }

        // GET: Tbl_member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_member tbl_member = db.tbl_member.Find(id);
            if (tbl_member == null)
            {
                return HttpNotFound();
            }
            return View(tbl_member);
        }

        // POST: Tbl_member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mem_id,mem_name,mem_email,mem_contact,mem_password")] tbl_member tbl_member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_member);
        }

        // GET: Tbl_member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_member tbl_member = db.tbl_member.Find(id);
            if (tbl_member == null)
            {
                return HttpNotFound();
            }
            return View(tbl_member);
        }

        // POST: Tbl_member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_member tbl_member = db.tbl_member.Find(id);
            db.tbl_member.Remove(tbl_member);
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
