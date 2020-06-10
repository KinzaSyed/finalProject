//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using finalproject.Models;

//namespace finalproject.Controllers
//{
//    public class BookauthorController : Controller
//    {
//        private lastDbEntities db = new lastDbEntities();

//        // GET: Bookauthor


//        public ActionResult TotalRec()
//        {
//            return View(db.Book_author.ToList());
//        }


//        public ActionResult Index()
//        {
//            return View(db.Book_author.ToList());
//        }

//        // GET: Bookauthor/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Book_author book_author = db.Book_author.Find(id);
//            if (book_author == null)
//            {
//                return HttpNotFound();
//            }
//            return View(book_author);
//        }

//        // GET: Bookauthor/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Bookauthor/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "auth_id,auth_name,auth_email,auth_contact")] Book_author book_author)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Book_author.Add(book_author);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(book_author);
//        }

//        // GET: Bookauthor/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Book_author book_author = db.Book_author.Find(id);
//            if (book_author == null)
//            {
//                return HttpNotFound();
//            }
//            return View(book_author);
//        }

//        // POST: Bookauthor/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "auth_id,auth_name,auth_email,auth_contact")] Book_author book_author)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(book_author).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(book_author);
//        }

//        // GET: Bookauthor/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Book_author book_author = db.Book_author.Find(id);
//            if (book_author == null)
//            {
//                return HttpNotFound();
//            }
//            return View(book_author);
//        }

//        // POST: Bookauthor/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Book_author book_author = db.Book_author.Find(id);
//            db.Book_author.Remove(book_author);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
