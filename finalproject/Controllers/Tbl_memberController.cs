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


       
            public ActionResult FollowPeople(tbl_follow follow, int followed_id)
        {
            string mem_email = Session["mem_email"].ToString();
            follow.follow_time = DateTime.Now;
            follow.followedby_id = db.tbl_member.Single(a => a.mem_email.Equals(mem_email)).mem_id;
            follow.followed_id = followed_id;
            db.tbl_follow.Add(follow);
            db.SaveChanges();
            return RedirectToAction("PublicProfile", "Tbl_member");

        }

        public ActionResult DetailsPublic(int? id)
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
        //public ActionResult PublicProfile()
        //{
        //    var members = from A in db.tbl_follow
        //                  join Bi in db.tbl_member
        //                  on A.followed_id
        //                  equals Bi.mem_id
                          
        //                  /*where A.follow_id*/ into fol
        //                  from tbl_follow in fol.Distinct()
                          
        //                  select new { memid = tbl_follow.mem_id };
        //    //ye error ha
        //    //is null
        //    //select B.mem_id;
        //    List<tbl_member> memberList = new List<tbl_member>();
        //    foreach (var member in members)
        //    {
        //        var mem = db.tbl_member.Where(x => x.mem_id == member.memid).First();
        //        memberList.Add(mem);
        //    }
               

        //    return View(memberList);
          
        //}


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

        public ActionResult BestSeller()
        {
            var BestsellingBooks =(from item in db.BTransactions
             group item.Transaction_qty by item.Book_id into g
            orderby g.Sum() descending
            select g.Key).Take(2);
            List<Tbl_Books> books = new List<Tbl_Books>();
            foreach(var bookid in BestsellingBooks)
            {
                var book = db.Tbl_Books.Single(x => x.Book_id == bookid);
                books.Add(book);

            }
            return PartialView(books);
        }


        public ActionResult UserPanel()
        {
            List<Reading_History> m = null;
            if (Session["mem_id"] != null)
            {
                int mem_id = Convert.ToInt32(Session["mem_id"].ToString());
                m = db.Reading_History.Where(x => x.memID == mem_id).ToList();


            }

            return View(m);
        }
        public ActionResult PersonalInfo()
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


        public ActionResult Totalmem()
        {
            return View(db.tbl_member.ToList());
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
