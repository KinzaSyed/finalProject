using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

            int memid = Convert.ToInt32(Session["mem_id"]);
            var tbl_blog = db.tbl_blog.Include(t => t.tbl_group).Include(t => t.tbl_member).Where(x => x.tbl_member.mem_id == memid);
            return View(tbl_blog.ToList());
        }
        public ActionResult Allblogsofgroup(int id)
        {

            var tbl_blog = db.tbl_blog.Include(t => t.tbl_group).Include(t => t.tbl_member).Where(x => x.tbl_group.group_id == id);
            return PartialView(tbl_blog.ToList());
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
            //ViewBag.blog_groupid = new SelectList(db.tbl_group, "group_id", "group_id");
            //ViewBag.blog_createdby = new SelectList(db.tbl_member, "mem_id", "mem_name");
            return View();
        }

        // POST: tbl_blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_blog tbl_Blog , HttpPostedFileBase imagefile,int id)
        {
            string path = uploadingfile(imagefile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                int memid = Convert.ToInt32(Session["mem_id"]);
                tbl_blog blog = new tbl_blog();
                blog.blog_datetime = DateTime.Now;
                blog.blog_groupid = id;
                blog.blog_createdby = memid;
                blog.blog_img = path;
                blog.blog_title = tbl_Blog.blog_title;
                blog.blog_body = tbl_Blog.blog_body;
                db.tbl_blog.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();

        }
        //public ActionResult Create([Bind(Include = "blog_id,blog_datetime,blog_groupid,blog_createdby,blog_img,blog_title,blog_body")] tbl_blog tbl_blog)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tbl_blog.Add(tbl_blog);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.blog_groupid = new SelectList(db.tbl_group, "group_id", "group_id", tbl_blog.blog_groupid);
        //    ViewBag.blog_createdby = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_blog.blog_createdby);
        //    return View(tbl_blog);
        //}
    


    public string uploadingfile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/img/"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/img/" + random + Path.GetFileName(file.FileName);

                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('only jgp,jpeg or png formats are acceptable .... ';</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;
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
