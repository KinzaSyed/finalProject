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
    public class BookpublisherController : Controller
    {
        private lastDbEntities db = new lastDbEntities();

        // GET: Bookpublisher
        public ActionResult Index()
        {
            return View(db.Book_publisher.ToList());
        }

        // GET: Bookpublisher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_publisher book_publisher = db.Book_publisher.Find(id);
            if (book_publisher == null)
            {
                return HttpNotFound();
            }
            return View(book_publisher);
        }

        // GET: Bookpublisher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookpublisher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pub_id,pub_name,pub_email,pub_contact")] Book_publisher book_publisher)
        {
            if (ModelState.IsValid)
            {
                db.Book_publisher.Add(book_publisher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book_publisher);
        }

        // GET: Bookpublisher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_publisher book_publisher = db.Book_publisher.Find(id);
            if (book_publisher == null)
            {
                return HttpNotFound();
            }
            return View(book_publisher);
        }

        // POST: Bookpublisher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pub_id,pub_name,pub_email,pub_contact")] Book_publisher book_publisher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_publisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book_publisher);
        }

        // GET: Bookpublisher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_publisher book_publisher = db.Book_publisher.Find(id);
            if (book_publisher == null)
            {
                return HttpNotFound();
            }
            return View(book_publisher);
        }

        // POST: Bookpublisher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book_publisher book_publisher = db.Book_publisher.Find(id);
            db.Book_publisher.Remove(book_publisher);
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
