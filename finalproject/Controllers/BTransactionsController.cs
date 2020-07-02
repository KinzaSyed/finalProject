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
    public class BTransactionsController : Controller
    {
        private lastDbEntities db = new lastDbEntities();

        // GET: BTransactions
        public ActionResult Index()
        {
          
            var bTransactions = db.BTransactions
                                .Include(b => b.Tbl_Books)
                                .Include(b => b.Invoice)
                                .Include(b => b.tbl_member);
            return View(bTransactions.ToList());
        }

        public ActionResult showstatus()
        {
            int mem_id = Convert.ToInt32(Session["mem_id"]);
            var bTransactions = db.BTransactions
                                .Include(b => b.Tbl_Books)
                                .Include(b => b.Invoice)
                                .Include(b => b.tbl_member)
                                .Where(x => x.mem_id == mem_id).ToList();
            return View(bTransactions.ToList());

        }


        public ActionResult Order()
        {
            int Vendor_id = Convert.ToInt32(Session["Vendor_id"]);
            var bTransactions = db.BTransactions
                                .Include(b => b.Tbl_Books)
                                .Include(b => b.Invoice)
                                .Include(b => b.tbl_member)
                                .Where(x => x.Tbl_Books.Vendor_id == Vendor_id).ToList();
            return View(bTransactions.ToList());

        }


        // GET: BTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BTransaction bTransaction = db.BTransactions.Find(id);
            if (bTransaction == null)
            {
                return HttpNotFound();
            }
            return View(bTransaction);
        }

        // GET: BTransactions/Create
        public ActionResult Create()
        {
            ViewBag.Book_id = new SelectList(db.Tbl_Books, "Book_id", "Book_name");
            ViewBag.in_id = new SelectList(db.Invoices, "in_id", "in_id");
            ViewBag.mem_id = new SelectList(db.tbl_member, "mem_id", "mem_name");
            return View();
        }

        // POST: BTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Transaction_id,Transaction_bill,Transaction_qty,Transaction_date,Transaction_price,Book_id,in_id,mem_id")] BTransaction bTransaction)
        {
            if (ModelState.IsValid)
            {
                db.BTransactions.Add(bTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Book_id = new SelectList(db.Tbl_Books, "Book_id", "Book_name", bTransaction.Book_id);
            ViewBag.in_id = new SelectList(db.Invoices, "in_id", "in_id", bTransaction.in_id);
            ViewBag.mem_id = new SelectList(db.tbl_member, "mem_id", "mem_name", bTransaction.mem_id);
            return View(bTransaction);
        }

        // GET: BTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BTransaction bTransaction = db.BTransactions.Find(id);
            if (bTransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.Book_id = new SelectList(db.Tbl_Books, "Book_id", "Book_name", bTransaction.Book_id);
            ViewBag.in_id = new SelectList(db.Invoices, "in_id", "in_id", bTransaction.in_id);
            ViewBag.mem_id = new SelectList(db.tbl_member, "mem_id", "mem_name", bTransaction.mem_id);
            return View(bTransaction);
        }

        // POST: BTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BTransaction bTransaction)
        {
            if (ModelState.IsValid)
            {
               var trans =  db.BTransactions.Find(bTransaction.Transaction_id);
                trans.transaction_status = bTransaction.transaction_status;
                db.Entry(trans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Order");
            }
            ViewBag.Book_id = new SelectList(db.Tbl_Books, "Book_id", "Book_name", bTransaction.Book_id);
            ViewBag.in_id = new SelectList(db.Invoices, "in_id", "in_id", bTransaction.in_id);
            ViewBag.mem_id = new SelectList(db.tbl_member, "mem_id", "mem_name", bTransaction.mem_id);
            return View(bTransaction);
        }

        //public ActionResult Edit([Bind(Include = "Transaction_id,Transaction_bill,Transaction_qty,Transaction_date,Transaction_price,Book_id,in_id,mem_id")] BTransaction bTransaction)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bTransaction).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Book_id = new SelectList(db.Tbl_Books, "Book_id", "Book_name", bTransaction.Book_id);
        //    ViewBag.in_id = new SelectList(db.Invoices, "in_id", "in_id", bTransaction.in_id);
        //    ViewBag.mem_id = new SelectList(db.tbl_member, "mem_id", "mem_name", bTransaction.mem_id);
        //    return View(bTransaction);
        //}



        // GET: BTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BTransaction bTransaction = db.BTransactions.Find(id);
            if (bTransaction == null)
            {
                return HttpNotFound();
            }
            return View(bTransaction);
        }

        // POST: BTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BTransaction bTransaction = db.BTransactions.Find(id);
            db.BTransactions.Remove(bTransaction);
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
