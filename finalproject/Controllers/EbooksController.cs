using finalproject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace finalproject.Controllers
{
    public class EbooksController : Controller
    {
        public string ses;

        lastDbEntities db = new lastDbEntities();


        public ActionResult SearchingEbook(string searchBy, string search)
        {


            if (searchBy == "cat_name")
            {
                return View(db.Ebooks_db.Where(x => x.Book_categoryy.cat_name.StartsWith(search) || search == null).ToList());
            }
            if (searchBy == "Book_name")
            {
                return View(db.Ebooks_db.Where(x => x.Ebook_name.StartsWith(search) || search == null).ToList());
            }
            else
                return View(db.Ebooks_db.Where(x => x.Ebook_author.StartsWith(search) || search == null).ToList());
        }
        public ActionResult Totalebooks()
        {
            List<Ebooks_db> Booklist = db.Ebooks_db.ToList();

            List<Tbl_for_Ebooks> booklistdis = Booklist.Select(x => new Tbl_for_Ebooks
            {
                Ebook_id = x.Ebook_id,
                Ebook_name = x.Ebook_name,
                Ebook_publisher = x.Ebook_publisher,
                Ebook_author = x.Ebook_author,
                cat_id = Convert.ToInt32(x.cat_id),
                cat_name = x.Book_categoryy.cat_name,
                Ebook_img = x.Ebook_img,
                Ebook_pdffile = x.Ebook_pdffile,
                Ebook_edition = x.Ebook_edition,
                mem_id = Convert.ToInt32(x.mem_id),
                mem_name = x.tbl_member.mem_name,
            }).ToList();



            return View(booklistdis);
        }

        // GET: Ebooks
        public ActionResult Index()
        { int memid = Convert.ToInt32(Session["mem_id"]);



            List<Ebooks_db> Booklist = db.Ebooks_db.Where(x => x.mem_id == memid).ToList();
            List<Tbl_for_Ebooks> booklistdis = Booklist.Select(x => new Tbl_for_Ebooks
            {
                Ebook_id = x.Ebook_id,
                Ebook_name = x.Ebook_name,
                Ebook_publisher = x.Ebook_publisher,
                Ebook_author = x.Ebook_author,
                cat_id = Convert.ToInt32(x.cat_id),
                cat_name = x.Book_categoryy.cat_name,
                Ebook_img = x.Ebook_img,
                Ebook_pdffile = x.Ebook_pdffile,
                Ebook_edition = x.Ebook_edition,
                mem_id = Convert.ToInt32(x.mem_id),
                mem_name = x.tbl_member.mem_name,
            }).ToList();



            return View(booklistdis);
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<Book_categoryy> li3 = db.Book_categoryy.ToList();
            ViewBag.catlist = new SelectList(li3, "cat_id", "cat_name");
            List<tbl_member> li4 = db.tbl_member.ToList();
            ViewBag.memlist = new SelectList(li4, "mem_id", "mem_name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tbl_for_Ebooks evm, HttpPostedFileBase imagefile, HttpPostedFileBase pdffile)
        {
            string path1 = uploadingpdffile(pdffile);
            string path = uploadingfile(imagefile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }

            else
            {

                List<Book_categoryy> li3 = db.Book_categoryy.ToList();
                ViewBag.catlist = new SelectList(li3, "cat_id", "cat_name");

                if (Session["mem_id"] != null)
                {
                    evm.mem_id = Convert.ToInt32(Session["mem_id"]);

                }

                List<tbl_member> li4 = db.tbl_member.ToList();
                ViewBag.memlist = new SelectList(li4, "mem_id", "mem_name");


                Ebooks_db ebk = new Ebooks_db();
                ebk.Ebook_name = evm.Ebook_name;
                ebk.Ebook_publisher = evm.Ebook_publisher;
                ebk.Ebook_author = evm.Ebook_author;
                ebk.cat_id = evm.cat_id;
                ebk.Ebook_img = path;
                ebk.Ebook_pdffile = path1;
                ebk.Ebook_edition = evm.Ebook_edition;
                ebk.mem_id = evm.mem_id;
                db.Ebooks_db.Add(ebk);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            return View();


        }

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


        public string uploadingpdffile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".pdf"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/pdfbooks/"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/pdfbooks/" + random + Path.GetFileName(file.FileName);

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
        public ActionResult Details(int? id)
        {

            var Ebook = db.Ebooks_db.Find(id);
            ViewBag.Ebook = Ebook;
            var review = new EBook_Review_db()
            {
                EBookId = Ebook.Ebook_id
            };
            return View("Details", review);

        }
        [HttpPost]
        public ActionResult SendReview(EBook_Review_db review, double rating)
        {
            string mem_email = Session["mem_email"].ToString();
            review.DatePost = DateTime.Now;
            review.memberId = db.tbl_member.Single(a => a.mem_email.Equals(mem_email)).mem_id;
            review.ERating = rating;
            db.EBook_Review_db.Add(review);
            db.SaveChanges();
            return RedirectToAction("Details", "EBooks", new { id = review.EBookId });

        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ebooks_db ebooks = db.Ebooks_db.Find(id);
            if (ebooks == null)
            {
                return HttpNotFound();
            }
            List<Book_categoryy> li3 = db.Book_categoryy.ToList();
            ViewBag.catlist = new SelectList(li3, "cat_id", "cat_name");



            List<tbl_member> li4 = db.tbl_member.ToList();
            ViewBag.venlist = new SelectList(li4, "mem_id", "mem_name");
            return View(ebooks);
        }

        // POST: Bookcategoryy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ebook_id,Ebook_name,Ebook_publisher,Ebook_author,cat_id,Ebook_img,Ebook_pdffile,Ebook_edition")]Ebooks_db ebooks)
        {
            var book = db.Ebooks_db.Where(x => x.Ebook_id == ebooks.Ebook_id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                book.Ebook_name = ebooks.Ebook_name;
                book.Ebook_publisher = ebooks.Ebook_publisher;
                book.Ebook_author = ebooks.Ebook_author;
                book.cat_id = ebooks.cat_id;
                book.Ebook_img = ebooks.Ebook_img;
                book.Ebook_pdffile = ebooks.Ebook_pdffile;
                book.Ebook_edition = ebooks.Ebook_edition;

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }


        // GET: Bookcategoryy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ebooks_db ebooks_Db = db.Ebooks_db.Find(id);
            if (ebooks_Db == null)
            {
                return HttpNotFound();
            }
            return View(ebooks_Db);
        }


        // POST: Bookcategoryy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ebooks_db ebooks_Db = db.Ebooks_db.Find(id);
            db.Ebooks_db.Remove(ebooks_Db);
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
    
        public ActionResult addtoReadings(Reading_History RD,int id)
        {
            
            var kinza = db.Reading_History.Where(b => b.ebook_id == id).Any();
             if (kinza ==false)
            {
                int memid = Convert.ToInt32(Session["mem_id"]);

                RD.ebook_id = id;


                RD.Read_Date = DateTime.Now;
                RD.memID = memid;


                db.Reading_History.Add(RD);
                db.SaveChanges();
            }

           
            return RedirectToAction("Details", "EBooks", new { id = id });
        }
       
     

        

        public ActionResult ReadABook()
        {
            int memid = Convert.ToInt32(Session["mem_id"]);



            List<Ebooks_db> Booklist = db.Ebooks_db.Where(x => x.mem_id == memid).ToList();
            List<Tbl_for_Ebooks> booklistdis = Booklist.Select(x => new Tbl_for_Ebooks
            {
                Ebook_id = x.Ebook_id,
                Ebook_name = x.Ebook_name,
                Ebook_publisher = x.Ebook_publisher,
                Ebook_author = x.Ebook_author,
                cat_id = Convert.ToInt32(x.cat_id),
                cat_name = x.Book_categoryy.cat_name,
                Ebook_img = x.Ebook_img,
                Ebook_pdffile = x.Ebook_pdffile,
                Ebook_edition = x.Ebook_edition,
                mem_id = Convert.ToInt32(x.mem_id),
                mem_name = x.tbl_member.mem_name,
            }).ToList();



            return View(booklistdis);

        }

    }

}
