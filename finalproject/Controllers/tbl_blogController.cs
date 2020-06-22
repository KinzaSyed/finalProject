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
    public class tbl_blogController : Controller
    {
        private lastDbEntities db = new lastDbEntities();

        // GET: tbl_blog
        public ActionResult Index()
        {
            var tbl_blog = db.tbl_blog.Include(t => t.tbl_group).Include(t => t.tbl_member);
            return View(tbl_blog.ToList());
        }

        // GET: tbl_blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_blog tbl_blog = db.tbl_blog.Find(id);
            if (tbl_blog == null)
            {
                return HttpNotFound();
            }
            return View(tbl_blog);
        }

        // GET: tbl_blog/Create
        public ActionResult Create()
        {
            ViewBag.blog_groupid = new SelectList(db.tbl_group, "group_id", "group_id");
            ViewBag.blog_createdby = new SelectList(db.tbl_member, "mem_id", "mem_name");
            return View();
        }

        // POST: tbl_blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "blog_id,blog_datetime,blog_groupid,blog_createdby,blog_img,blog_title,blog_body")] tbl_blog tbl_blog)
        {
            if (ModelState.IsValid)
            {
                db.tbl_blog.Add(tbl_blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.blog_groupid = new SelectList(db.tbl_group, "group_id", "group_id", tbl_blog.blog_groupid);
            ViewBag.blog_createdby = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_blog.blog_createdby);
            return View(tbl_blog);
        }

        // GET: tbl_blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_blog tbl_blog = db.tbl_blog.Find(id);
            if (tbl_blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.blog_groupid = new SelectList(db.tbl_group, "group_id", "group_id", tbl_blog.blog_groupid);
            ViewBag.blog_createdby = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_blog.blog_createdby);
            return View(tbl_blog);
        }

        // POST: tbl_blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "blog_id,blog_datetime,blog_groupid,blog_createdby,blog_img,blog_title,blog_body")] tbl_blog tbl_blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.blog_groupid = new SelectList(db.tbl_group, "group_id", "group_id", tbl_blog.blog_groupid);
            ViewBag.blog_createdby = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_blog.blog_createdby);
            return View(tbl_blog);
        }

        // GET: tbl_blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_blog tbl_blog = db.tbl_blog.Find(id);
            if (tbl_blog == null)
            {
                return HttpNotFound();
            }
            return View(tbl_blog);
        }

        // POST: tbl_blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_blog tbl_blog = db.tbl_blog.Find(id);
            db.tbl_blog.Remove(tbl_blog);
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
