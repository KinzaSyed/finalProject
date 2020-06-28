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
    public class tbl_groupController : Controller
    {
        private lastDbEntities db = new lastDbEntities();

        

        public ActionResult FindGroups()
        {
            int userid = Convert.ToInt32(Session["mem_id"].ToString());
            var follower = db.tbl_groupmem.Where(x => x.group_mem_memid== userid);


            var rightjoin = (from right in db.tbl_group
                             join left in follower
                            on right.group_id equals left.group_mem_groupid into temp
                             from left in temp.DefaultIfEmpty()
                             where left.group_mem_id == null
                             select new { memid = right.group_id }).ToList();


            List<tbl_group> memberlist = new List<tbl_group>();
            foreach (var member in rightjoin)
            {
                var mem = db.tbl_group.Where(x => x.group_id == member.memid).First();
                memberlist.Add(mem);
            }


            return View(memberlist);


        }


        public ActionResult GroupHome()
        {

            return View();
        }

        public ActionResult JoinGroup(tbl_groupmem groupmem, int id)
        {
            int memid = Convert.ToInt32(Session["mem_id"]);

            groupmem.group_mem_memid = memid;
            groupmem.group_mem_groupid = id;
            groupmem.group_mem_joindate = DateTime.Now;
            db.tbl_groupmem.Add(groupmem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: tbl_group
        public ActionResult Index()
        {


            int memid = Convert.ToInt32(Session["mem_id"]);

            var tbl_group = db.tbl_group.Include(t => t.tbl_member).Where(x => x.tbl_member.mem_id == memid);
            return View(tbl_group.ToList());



        }
        public ActionResult GroupUrIn()
        {
            int memid = Convert.ToInt32(Session["mem_id"]);

            var tbl_groupmem = db.tbl_groupmem.Include(t => t.tbl_group).Include(t => t.tbl_member).Where(x => x.tbl_member.mem_id== memid);
            var a = 12 + 2;
            var c = a;
            return View(tbl_groupmem.ToList());
        }

        // GET: tbl_group/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_group tbl_group = db.tbl_group.Find(id);
            if (tbl_group == null)
            {
                return HttpNotFound();
            }
            return View(tbl_group);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: tbl_group/Create
        [HttpPost]
        public ActionResult Create(tbl_group group,string group_title)
        {
            int memid = Convert.ToInt32(Session["mem_id"]);
            group.group_createdby = memid;
            group.group_datetime = DateTime.Now;
            group.group_title = group_title;
            db.tbl_group.Add(group);
            db.SaveChanges();

            return View();
        }
        //// GET: tbl_group/Create
        //public ActionResult Create()
        //{
        //    ViewBag.group_createdby = new SelectList(db.tbl_member, "mem_id", "mem_name");
        //    return View();
        //}

        //// POST: tbl_group/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "group_id,group_datetime,group_createdby,group_title")] tbl_group tbl_group)
        //{
        //    int memid = Convert.ToInt32(Session["mem_id"]);
        //    if (ModelState.IsValid)
        //    {
        //        db.tbl_group.Add(tbl_group);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.group_createdby = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_group.group_createdby);
        //    return View(tbl_group);
        //}

        // GET: tbl_group/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_group tbl_group = db.tbl_group.Find(id);
            if (tbl_group == null)
            {
                return HttpNotFound();
            }
            ViewBag.group_createdby = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_group.group_createdby);
            return View(tbl_group);
        }

        // POST: tbl_group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "group_id,group_datetime,group_createdby,group_title")] tbl_group tbl_group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.group_createdby = new SelectList(db.tbl_member, "mem_id", "mem_name", tbl_group.group_createdby);
            return View(tbl_group);
        }

        // GET: tbl_group/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_group tbl_group = db.tbl_group.Find(id);
            if (tbl_group == null)
            {
                return HttpNotFound();
            }
            return View(tbl_group);
        }

        // POST: tbl_group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_group tbl_group = db.tbl_group.Find(id);
            db.tbl_group.Remove(tbl_group);
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
