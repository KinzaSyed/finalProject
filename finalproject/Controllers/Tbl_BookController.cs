using finalproject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace finalproject.Controllers
{
    public class Tbl_BookController : Controller
    {
        // GET: Tbl_Book
        lastDbEntities db = new lastDbEntities();

        //public ActionResult booklib()
        //{

        //}

        public ActionResult Totalbooks()
        {
            List<Tbl_Books> Booklist = db.Tbl_Books.ToList();
            List<Tbl_for_books> booklistdis = Booklist.Select(x => new Tbl_for_books
            {
                Book_id = x.Book_id,
                Book_name = x.Book_name,
                Book_Edition = x.Book_Edition,
                Book_price = x.Book_price,
                Book_img = x.Book_img,
                auth_id = x.auth_id,
                pub_id = x.pub_id,
                cat_id = Convert.ToInt32(x.cat_id),
                cat_name = x.Book_categoryy.cat_name,
                Vendor_id = Convert.ToInt32(x.Vendor_id),
                Vendor_name = x.Tbl_Vendorr.Vendor_name,
                //   Book_ebook = x.Book_ebook



            }).ToList();

            return View(booklistdis);
        }

        public ActionResult Index()
        {
            
            int Vendor_id = Convert.ToInt32(Session["Vendor_id"]);
            
            List<Tbl_Books> Booklist = db.Tbl_Books.Where(x => x.Vendor_id == Vendor_id).ToList();
            List<Tbl_for_books> booklistdis = Booklist.Select(x => new Tbl_for_books
            {
                Book_id = x.Book_id,
                Book_name = x.Book_name,
                Book_Edition = x.Book_Edition,
                Book_price = x.Book_price,
                Book_img = x.Book_img,
                auth_id = x.auth_id,
              
                pub_id = x.pub_id,
           
                cat_id = Convert.ToInt32(x.cat_id),
                cat_name = x.Book_categoryy.cat_name,
                Vendor_id = Convert.ToInt32(x.Vendor_id),
                Vendor_name = x.Tbl_Vendorr.Vendor_name,
             //   Book_ebook = x.Book_ebook



            }).ToList();

            return View(booklistdis);
        }
        public ActionResult AdminIndex()
        {
            List<Tbl_Books> Booklist = db.Tbl_Books.ToList();
            List<Tbl_for_books> booklistdis = Booklist.Select(x => new Tbl_for_books
            {
                Book_id = x.Book_id,
                Book_name = x.Book_name,
                Book_Edition = x.Book_Edition,
                Book_price = x.Book_price,
                Book_img = x.Book_img,
                auth_id = x.auth_id,

                pub_id = x.pub_id,

                cat_id = Convert.ToInt32(x.cat_id),
                cat_name = x.Book_categoryy.cat_name,
                Vendor_id = Convert.ToInt32(x.Vendor_id),
                Vendor_name = x.Tbl_Vendorr.Vendor_name,
                //   Book_ebook = x.Book_ebook



            }).ToList();

            return View(booklistdis);
        }
        [HttpGet]
        public ActionResult Create()
        {
            //List<Book_author> li = db.Book_author.ToList();
            //ViewBag.authlist = new SelectList(li, "auth_id", "auth_name");
            //List<Book_publisher> li2 = db.Book_publisher.ToList();
            //ViewBag.publist = new SelectList(li2, "pub_id", "pub_name");
            List<Book_categoryy> li3 = db.Book_categoryy.ToList();
            ViewBag.catlist = new SelectList(li3, "cat_id", "cat_name");
            List<Tbl_Vendorr> li4 = db.Tbl_Vendorr.ToList();
            ViewBag.venlist = new SelectList(li4, "Vendor_id", "Vendor_name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Tbl_for_books evm, HttpPostedFileBase imagefile, HttpPostedFileBase pdffile)
        {
        //    string path1 = uploadingpdffile(pdffile);
            string path = uploadingfile(imagefile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }

            else
            {
                //List<Book_author> li = db.Book_author.ToList();
                //ViewBag.authlist = new SelectList(li, "auth_id", "auth_name");

                //List<Book_publisher> li2 = db.Book_publisher.ToList();
                //ViewBag.publist = new SelectList(li2, "pub_id", "pub_name");
                List<Book_categoryy> li3 = db.Book_categoryy.ToList();
                ViewBag.catlist = new SelectList(li3, "cat_id", "cat_name");
                List<Tbl_Vendorr> li4 = db.Tbl_Vendorr.ToList();
                ViewBag.venlist = new SelectList(li4, "Vendor_id", "Vendor_name");
                Tbl_Books bk = new Tbl_Books();
                bk.Book_name = evm.Book_name;
                bk.Book_img = path;
                bk.auth_id = evm.auth_id;
                bk.pub_id = evm.pub_id;
                bk.cat_id = evm.cat_id;
                bk.Book_Edition = evm.Book_Edition;
                bk.Book_price = evm.Book_price;
                bk.Vendor_id = evm.Vendor_id;
              //  bk.Book_ebook = path1;
                db.Tbl_Books.Add(bk);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
                return View();

            
        }
        
        public ActionResult Details(int? id)
        {
            /**  Tbl_for_books evn = new Tbl_for_books();
              Tbl_Books bq = db.Tbl_Books.Where(x => x.Book_id == id).SingleOrDefault();
              evn.Book_name = bq.Book_name;
              evn.Book_Edition = bq.Book_Edition;
              evn.Book_img = bq.Book_img;
              evn.Book_price = bq.Book_price;

              evn.auth_id = Convert.ToInt32(bq.auth_id);
              int AuthorId = evn.auth_id;
              Book_author ben = db.Book_author.Where(x => x.auth_id == AuthorId).SingleOrDefault();
              evn.auth_name = ben.auth_name;


              evn.Vendor_id = Convert.ToInt32(bq.Vendor_id);
              int VendorId = evn.Vendor_id;
              Tbl_Vendorr ven = db.Tbl_Vendorr.Where(x => x.Vendor_id == VendorId).SingleOrDefault();
              evn.Vendor_name = ven.Vendor_name;
              return View(evn); **/
            var book = db.Tbl_Books.Find(id);
            ViewBag.book = book;
            var review = new Book_Review()
            {
                BookId = book.Book_id
            };
            return View("Details", review);

        }

        public string uploadingfile (HttpPostedFileBase file)
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
                    catch(Exception ex)
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


     /*   public string uploadingpdffile(HttpPostedFileBase file)
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

    */

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Tbl_Books Tbl_Books = db.Tbl_Books.Find(id);
          
            if (Tbl_Books == null)
            {
                return HttpNotFound();
            }
           
            List<Book_categoryy> li3 = db.Book_categoryy.ToList();
            ViewBag.catlist = new SelectList(li3, "cat_id", "cat_name");



            List<Tbl_Vendorr> li4 = db.Tbl_Vendorr.ToList();
            ViewBag.venlist = new SelectList(li4, "Vendor_id", "Vendor_name");
            return View(Tbl_Books);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST: Bookcategoryy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        public ActionResult Edit(Tbl_Books Tbl_Books, HttpPostedFileBase imagefile)
        {
            var tbl_Books = db.Tbl_Books.Find(Tbl_Books.Book_id);

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(imagefile != null ? imagefile.FileName : ""))
                {
                    string path = uploadingfile(imagefile);
                    if (path.Equals("-1"))
                    {
                        ViewBag.error = "Image could not be uploaded....";
                    }
                    else
                    {
                        tbl_Books.Book_img = path;

                    }
                }

                tbl_Books.Book_name = Tbl_Books.Book_name;
                tbl_Books.Book_Edition = Tbl_Books.Book_Edition;
                tbl_Books.Book_price = Tbl_Books.Book_price;
                tbl_Books.auth_id = Tbl_Books.auth_id;
                tbl_Books.pub_id = Tbl_Books.pub_id;
                tbl_Books.cat_id = Tbl_Books.cat_id;




                db.Entry(tbl_Books).State = EntityState.Modified;
                db.SaveChanges();


                if (Session["Admin_id"] != null)
                {
                    return RedirectToAction("AdminIndex");

                }
                else
                {

                    return RedirectToAction("Index");
                }
            }
            return View(Tbl_Books);


        }

        //public ActionResult Edit(HttpPostedFileBase file, Tbl_Books bok)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        if (file != null)
        //        {
        //            string path = uploadingfile(file);
        //            if (path.Equals("-1"))
        //            {
        //                ViewBag.error = "Image could not be uploaded....";
        //            }
        //            else
        //            {
        //                db.Entry(bok).State = EntityState.Modified;
        //                if (db.SaveChanges() > 0)
        //                {
        //                    file.SaveAs(path);
        //                    Session["msg"] = "Image Updated";
        //                    return RedirectToAction("Index");
        //                }
        //            }

        //        }
        //        else
        //        {
        //            bok.Book_img = Session["imgPath"].ToString();
        //            db.Entry(bok).State = EntityState.Modified;
        //            if (db.SaveChanges() > 0)
        //            {
        //                Session["msg"] = "Data Updated";
        //                return RedirectToAction("Index");
        //            }
        //        }

        //    }
        //    return View(bok);
        //}














        [HttpPost]
        public ActionResult SendReview(Book_Review review, double rating)
        {
            string mem_email = Session["mem_email"].ToString();
            review.DatePost = DateTime.Now;
            review.memberId = db.tbl_member.Single(a => a.mem_email.Equals(mem_email)).mem_id;
            review.Rating = rating;
            db.Book_Review.Add(review);
            db.SaveChanges();
            return RedirectToAction("Details", "Tbl_Book", new { id = review.BookId });

        }

        public ActionResult Searching(string searchBy, string search , int? page)
        {

            
            if (searchBy == "cat_name")
            {
                return View(db.Tbl_Books.Where(x => x.Book_categoryy.cat_name.StartsWith(search) || search == null)
                    .ToList().ToPagedList(page ?? 1,3));
            }
            if (searchBy == "Book_name")
            {
                return View(db.Tbl_Books.Where(x => x.Book_name.StartsWith(search) || search == null)
                    .ToList().ToPagedList(page ?? 1, 3));
            }
            else
                return View(db.Tbl_Books.Where(x => x.pub_id.StartsWith(search) || search == null)
                    .ToList().ToPagedList(page ?? 1, 3));
        }


        // GET: Bookcategoryy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Books Tbl_Books = db.Tbl_Books.Find(id);
            if (Tbl_Books == null)
            {
                return HttpNotFound();
            }
            return View(Tbl_Books);
        }


        // POST: Bookcategoryy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Books Tbl_Books = db.Tbl_Books.Find(id);
            db.Tbl_Books.Remove(Tbl_Books);
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