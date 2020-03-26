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
    public class BookcategoryyController : Controller
    {
        private lastDbEntities db = new lastDbEntities();

        // GET: Bookcategoryy
        public ActionResult Index()
        {
            return View(db.Book_categoryy.ToList());
        }

        // GET: Bookcategoryy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_categoryy book_categoryy = db.Book_categoryy.Find(id);
            if (book_categoryy == null)
            {
                return HttpNotFound();
            }
            return View(book_categoryy);
        }

        // GET: Bookcategoryy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookcategoryy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cat_id,cat_name,cat_status")] Book_categoryy book_categoryy)
        {
            if (ModelState.IsValid)
            {
                db.Book_categoryy.Add(book_categoryy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book_categoryy);
        }

        // GET: Bookcategoryy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_categoryy book_categoryy = db.Book_categoryy.Find(id);
            if (book_categoryy == null)
            {
                return HttpNotFound();
            }
            return View(book_categoryy);
        }

        // POST: Bookcategoryy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cat_id,cat_name,cat_status")] Book_categoryy book_categoryy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_categoryy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book_categoryy);
        }

        // GET: Bookcategoryy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_categoryy book_categoryy = db.Book_categoryy.Find(id);
            if (book_categoryy == null)
            {
                return HttpNotFound();
            }
            return View(book_categoryy);
        }

        // POST: Bookcategoryy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book_categoryy book_categoryy = db.Book_categoryy.Find(id);
            db.Book_categoryy.Remove(book_categoryy);
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
