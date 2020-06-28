using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using finalproject.Models;
using Newtonsoft.Json;

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
       


        public ActionResult PublicRecentReading(int id)
        {
            List<Reading_History> m = null;
            m = db.Reading_History.Where(x => x.memID == id).ToList();

            return PartialView(m);

        }
        public ActionResult WhoTheyFollow(int? id)
        {

            List<tbl_follow> m = null;
            m = db.tbl_follow.Where(x => x.followedby_id == id).ToList();

            return PartialView(m);

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
        public ActionResult publicprofile()
        {
            //var membersOutterJoin = (from a in db.tbl_follow
            //              join bi in db.tbl_member
            //              on a.followed_id
            //              equals bi.mem_id
            //              into fol
            //              from bi in fol.DefaultIfEmpty()
            //              select new { memid = bi.mem_id }).ToList();
            //var membersInnerJoin =( from b in db.tbl_member
            //                        join a in db.tbl_follow
            //                        on b.mem_id
            //                        equals a.followed_id
            //                        into fol
            //                        from tbl_follow in fol.DefaultIfEmpty()
            //                        select new { memid = tbl_follow.followed_id}).ToList();

            var rightjoin = (from right in db.tbl_follow
                             join left in db.tbl_member
                            on right.followed_id equals left.mem_id into temp
                            from left in temp.DefaultIfEmpty()
                            select new { memid = right.followed_id}).ToList();


            //var fullOuterJoinmembers = membersOutterJoin.Union(membersInnerJoin);
            List<tbl_member> memberlist = new List<tbl_member>();
            foreach (var member in rightjoin)
            {
                var mem = db.tbl_member.Where(x => x.mem_id == member.memid).First();
                memberlist.Add(mem);
            }


            return View(memberlist);

        }


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
            List<Tbl_Books> books = new List<Tbl_Books>();
            var BestsellingBooks =(from item in db.BTransactions
             group item.Transaction_qty by item.Book_id into g
            orderby g.Sum() descending
            select g.Key).Take(3);

           

            foreach(var bookid in BestsellingBooks)
            {
                var book = db.Tbl_Books.Single(x => x.Book_id == bookid);
                books.Add(book);

            }
            return PartialView(books);
        }

        public ActionResult RecommandBook()
        {
            //save file in csv
            string cs = ConfigurationManager.ConnectionStrings["lastDbEntities"].ConnectionString;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            string bookQuery = "select Ebook_id,Ebook_name from lastDb.dbo.Ebooks_db;";
            string RatingQuery = "select EReview_id,ERating,EbookId,memberId from lastDb.dbo.EBook_Review_db;";
            using (var context = new lastDbEntities())
            {
                var connection = (System.Data.SqlClient.SqlConnection)context.Database.Connection;
                if (connection != null && connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet ds = new DataSet();
                //Ebook save in .csv file 
                using (var da = new System.Data.SqlClient.SqlDataAdapter(bookQuery, connection))
                {
                    da.Fill(ds);
                    ds.Tables[0].TableName = "Ebook";
                    sb.Append("Ebook_Id" + ",");
                    sb.Append("Ebook_name" + ",");
                    sb.Append("\r\n");
                }
                foreach (DataRow b in ds.Tables["Ebook"].Rows)
                {
                    int ebookid = Convert.ToInt32(b["Ebook_id"]);
                    sb.Append(ebookid.ToString() + ',');
                    sb.Append(b["Ebook_name"].ToString());
                    sb.Append("\r\n");
                }
                string book = "Ebook.csv";
                StreamWriter file = new StreamWriter(@"C:\Users\Kinza Syed\source\repos\finalproject\Knn_Recommandation\" + book);
                file.WriteLine(sb.ToString());
                file.Close();
                //Review save in .csv file 
                DataSet ds1 = new DataSet();
                using (var da = new System.Data.SqlClient.SqlDataAdapter(RatingQuery, connection))
                {
                    da.Fill(ds1);
                    ds1.Tables[0].TableName = "Rating";
                    sb1.Append("EReview_id" + ",");
                    sb1.Append("memberId" + ",");
                    sb1.Append("Ebook_Id" + ",");
                    sb1.Append("ERating" + ",");
                    sb1.Append("\r\n");
                }
                foreach (DataRow b in ds1.Tables["Rating"].Rows)
                {
                    int ReviewId = Convert.ToInt32(b["EReview_id"]);
                    sb1.Append(ReviewId.ToString() + ',');
                    int memid = Convert.ToInt32(b["memberId"]);
                    sb1.Append(memid.ToString() + ',');
                    int EbookId = Convert.ToInt32(b["EbookId"]);
                    sb1.Append(EbookId.ToString() + ',');
                    int Rate = Convert.ToInt32(b["ERating"]);
                    sb1.Append(Rate.ToString() + ',');

                    sb1.Append("\r\n");

                }
                string rating = "Rating.csv";
                StreamWriter file1 = new StreamWriter(@"C:\Users\Kinza Syed\source\repos\finalproject\Knn_Recommandation\" + rating);
                file1.WriteLine(sb1.ToString());
                file1.Close();

            }
            
            //API CALL
            List<string> Recommand = new List<string>(6);
            List<Ebooks_db> Recommandbooks = new List<Ebooks_db>();
            string book_name = null;
            string postdata = null;
            string result = null;
            var ids = (from item in db.Reading_History
                         select item.ebook_id).ToList<int>();
            Random random = new Random();
            int index = random.Next(ids.Count);
            int book_id = ids[index];
            if (book_id == 0)
            {
                book_id = (from item in db.Reading_History
                               select item.ebook_id).FirstOrDefault();
            }

            book_name = (from a in db.Ebooks_db
                         where a.Ebook_id == book_id
                         select a).Single().Ebook_name;

            string url = string.Format("http://localhost:5000/recommand");
            WebRequest requestobj = WebRequest.Create(url);
            requestobj.Method = "POST";
            requestobj.ContentType = "application/json";
            if (!string.IsNullOrEmpty(book_name)) { 
                postdata = "{\"parameter\":\"" + book_name + "\"}";
            }
            else{
                postdata = "{\"parameter\":\"A new journey\"}";}
            
            using (var streamWriter = new StreamWriter(requestobj.GetRequestStream()))
            {
                streamWriter.Write(postdata);
                streamWriter.Flush();
                streamWriter.Close();
                var httpResponse = (HttpWebResponse)requestobj.GetResponse();
                using (var streamreader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamreader.ReadToEnd();
            }}
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(result);
            foreach (string i in obj.book)
            {
                Recommand.Add(i);
            }
            foreach (var bookname in Recommand)
            {
                int id = db.Ebooks_db.Where(x => x.Ebook_name == bookname).SingleOrDefault().Ebook_id;
                var book = db.Ebooks_db.Single(x => x.Ebook_id == id);
                Recommandbooks.Add(book);
            }




            return PartialView(Recommandbooks);
        }


        public ActionResult UserPanel()
        {
            List<Reading_History> m = null;
            if (Session["mem_id"] != null)
            {
                int mem_idd = Convert.ToInt32(Session["mem_id"].ToString());
                var selectname = (from n in db.tbl_member
                                 where n.mem_id == mem_idd
                                 select n).Single().mem_name;
                ViewBag.membername = selectname;
                m = db.Reading_History.Where(x => x.memID == mem_idd).ToList();
                
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
        public ActionResult Create([Bind(Include = "mem_id,mem_name,mem_email,mem_contact,mem_password,mem_address,mem_dob")] tbl_member tbl_member)
        {

            //var readings = db.Reading_History.Where(b => b.ebook_id == id).Any();
            //if (readings == false)
            //{
            var email = tbl_member.mem_email;
            var emailcheck = db.tbl_member.Where(b => b.mem_email == email).Any();
            var emailcheck1 = db.Tbl_Vendorr.Where(b => b.Vendor_email == email).Any();
            var emailcheck2 = db.Tbl_admin.Where(b => b.Admin_email == email).Any();

            if (emailcheck == false && emailcheck1 == false && emailcheck2 == false)
            {

                if (ModelState.IsValid)
                {
                    db.tbl_member.Add(tbl_member);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }

                return View(tbl_member);
            }
            else
            {
                Response.Write("<script>alert('Email Already registered';</script>");
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
        public ActionResult Edit([Bind(Include = "mem_id,mem_name,mem_email,mem_contact,mem_password,mem_address,mem_dob")] tbl_member tbl_member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserPanel", "Tbl_member");
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
