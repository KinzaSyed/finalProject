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
    public class tbl_groupmemController : Controller
    {
        private lastDbEntities db = new lastDbEntities();

        // GET: tbl_groupmem
        public ActionResult Index()
        {
            var tbl_groupmem = db.tbl_groupmem.Include(t => t.tbl_group).Include(t => t.tbl_member);
            return View(tbl_groupmem.ToList());
        }

        // GET: tbl_groupmem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_groupmem tbl_groupmem = db.tbl_groupmem.Find(id);
            if (tbl_groupmem == null)
            {
                return HttpNotFound();
            }
            return View(tbl_groupmem);
        }

        // GET: tbl_groupmem/Create
        public ActionResult Create()
        {
            ViewBag.group_mem_groupid = new SelectList(db.tbl_group, "group_id", "group_title");
            ViewBag.group_mem_memid = new SelectList(db.tbl_member, "mem_id", "mem_name");
            return View();
        }

        // POST: tbl_groupmem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "group_mem_id,group_mem_groupid,group_mem_memid,group_mem_joindate")] tbl_groupmem tbl_groupmem)
        {
            if (ModelState.IsValid)
            {
                db.tbl_groupmem.Add(tbl_groupmem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.group_mem_groupid = new SelectList(db.tbl_group, "group_id", "group_title", tbl_groupmem.group_mem_groupid);
            ViewBag.group_mem_memid = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_groupmem.group_mem_memid);
            return View(tbl_groupmem);
        }

        // GET: tbl_groupmem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_groupmem tbl_groupmem = db.tbl_groupmem.Find(id);
            if (tbl_groupmem == null)
            {
                return HttpNotFound();
            }
            ViewBag.group_mem_groupid = new SelectList(db.tbl_group, "group_id", "group_title", tbl_groupmem.group_mem_groupid);
            ViewBag.group_mem_memid = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_groupmem.group_mem_memid);
            return View(tbl_groupmem);
        }

        // POST: tbl_groupmem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "group_mem_id,group_mem_groupid,group_mem_memid,group_mem_joindate")] tbl_groupmem tbl_groupmem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_groupmem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.group_mem_groupid = new SelectList(db.tbl_group, "group_id", "group_title", tbl_groupmem.group_mem_groupid);
            ViewBag.group_mem_memid = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_groupmem.group_mem_memid);
            return View(tbl_groupmem);
        }

        // GET: tbl_groupmem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_groupmem tbl_groupmem = db.tbl_groupmem.Find(id);
            if (tbl_groupmem == null)
            {
                return HttpNotFound();
            }
            return View(tbl_groupmem);
        }

        // POST: tbl_groupmem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_groupmem tbl_groupmem = db.tbl_groupmem.Find(id);
            db.tbl_groupmem.Remove(tbl_groupmem);
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
