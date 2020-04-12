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
    public class tbl_wishlistController : Controller
    {
        private lastDbEntities db = new lastDbEntities();

        // GET: tbl_wishlist

        

             public ActionResult wishlistwindow()
        {
            var tbl_wishlist1 = db.tbl_wishlist.Include(t => t.Tbl_Books).Include(t => t.tbl_member);
            return View(tbl_wishlist1.ToList());
        }

        public ActionResult Index()
        {
            var tbl_wishlist = db.tbl_wishlist.Include(t => t.Tbl_Books).Include(t => t.tbl_member);
            return View(tbl_wishlist.ToList());
        }

        // GET: tbl_wishlist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_wishlist tbl_wishlist = db.tbl_wishlist.Find(id);
            if (tbl_wishlist == null)
            {
                return HttpNotFound();
            }
            return View(tbl_wishlist);
        }

        //// GET: tbl_wishlist/Create
        //public ActionResult Create()
        //{




        //    ViewBag.Book_ID = new SelectList(db.Tbl_Books, "Book_id", "Book_name");
        //    ViewBag.mem_Id = new SelectList(db.tbl_member, "mem_id", "mem_name");
        //    return View();
        //}

        //// POST: tbl_wishlist/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Wish_Id,Book_ID,mem_Id,Wish_date")] tbl_wishlist tbl_wishlist)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tbl_wishlist.Add(tbl_wishlist);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Book_ID = new SelectList(db.Tbl_Books, "Book_id", "Book_name", tbl_wishlist.Book_ID);
        //    ViewBag.mem_Id = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_wishlist.mem_Id);
        //    return View(tbl_wishlist);
        //}

        // GET: tbl_wishlist/Edit/5


        public ActionResult Create(tbl_wishlist wish, int id)
        {

            var book_chk = db.tbl_wishlist.Where(b => b.Book_ID == id).Any();
            if (book_chk == false)
            {
                int memid = Convert.ToInt32(Session["mem_id"]);

               wish.Book_ID = id;


                wish.Wish_date = DateTime.Now;
                wish.mem_Id = memid;


                db.tbl_wishlist.Add(wish);
                db.SaveChanges();
            }


            return RedirectToAction("Index", "AddtoCart");
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_wishlist tbl_wishlist = db.tbl_wishlist.Find(id);
            if (tbl_wishlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.Book_ID = new SelectList(db.Tbl_Books, "Book_id", "Book_name", tbl_wishlist.Book_ID);
            ViewBag.mem_Id = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_wishlist.mem_Id);
            return View(tbl_wishlist);
        }

        // POST: tbl_wishlist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Wish_Id,Book_ID,mem_Id,Wish_date")] tbl_wishlist tbl_wishlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_wishlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Book_ID = new SelectList(db.Tbl_Books, "Book_id", "Book_name", tbl_wishlist.Book_ID);
            ViewBag.mem_Id = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_wishlist.mem_Id);
            return View(tbl_wishlist);
        }

        // GET: tbl_wishlist/Delete/5
        public ActionResult Delete(int? id)
        {
            tbl_wishlist tbl_wishlist = db.tbl_wishlist.Find(id);
            db.tbl_wishlist.Remove(tbl_wishlist);
            db.SaveChanges();
            return RedirectToAction("UserPanel","Tbl_member");


            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //tbl_wishlist tbl_wishlist = db.tbl_wishlist.Find(id);
            //if (tbl_wishlist == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(tbl_wishlist);
        }

        // POST: tbl_wishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_wishlist tbl_wishlist = db.tbl_wishlist.Find(id);
            db.tbl_wishlist.Remove(tbl_wishlist);
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
