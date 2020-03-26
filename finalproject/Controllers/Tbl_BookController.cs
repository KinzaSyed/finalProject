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
    public class Tbl_BookController : Controller
    {
        // GET: Tbl_Book
        lastDbEntities db = new lastDbEntities();


        public ActionResult Index()
        {
            List<Tbl_Books> Booklist = db.Tbl_Books.ToList();
            List<Tbl_for_books> booklistdis = Booklist.Select(x => new Tbl_for_books
            {
                Book_id = x.Book_id,
                Book_name = x.Book_name,
                Book_Edition = x.Book_Edition,
                Book_price = x.Book_price,
                Book_img = x.Book_img,
                auth_id = Convert.ToInt32(x.auth_id),
                auth_name = x.Book_author.auth_name,
                pub_id = Convert.ToInt32(x.pub_id),
                pub_name = x.Book_publisher.pub_name,
                cat_id = Convert.ToInt32(x.cat_id),
                cat_name = x.Book_categoryy.cat_name,
                Vendor_id = Convert.ToInt32(x.Vendor_id),
                Vendor_name = x.Tbl_Vendorr.Vendor_name



            }).ToList();

            return View(booklistdis);
        }
        [HttpGet]
        public ActionResult Create()
        {

            List<Book_author> li = db.Book_author.ToList();
            ViewBag.authlist = new SelectList(li, "auth_id", "auth_name");

            List<Book_publisher> li2 = db.Book_publisher.ToList();
            ViewBag.publist = new SelectList(li2, "pub_id", "pub_name");



            List<Book_categoryy> li3 = db.Book_categoryy.ToList();
            ViewBag.catlist = new SelectList(li3, "cat_id", "cat_name");



            List<Tbl_Vendorr> li4 = db.Tbl_Vendorr.ToList();
            ViewBag.venlist = new SelectList(li4, "Vendor_id", "Vendor_name");






            return View();
        }
        [HttpPost]
        public ActionResult Create(Tbl_for_books evm, HttpPostedFileBase imagefile)
        {

            string path = uploadingfile(imagefile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }

            else
            {



                List<Book_author> li = db.Book_author.ToList();
                ViewBag.authlist = new SelectList(li, "auth_id", "auth_name");

                List<Book_publisher> li2 = db.Book_publisher.ToList();
                ViewBag.publist = new SelectList(li2, "pub_id", "pub_name");



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
                db.Tbl_Books.Add(bk);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
                return View();

            
        }
        
        public ActionResult Details(int? id)
        {
            Tbl_for_books evn = new Tbl_for_books();
            Tbl_Books bq = db.Tbl_Books.Where(x => x.Book_id == id).SingleOrDefault();
            evn.Book_name = bq.Book_name;
            evn.Book_Edition = bq.Book_Edition;
            evn.Book_price = bq.Book_price;
            evn.Vendor_id = Convert.ToInt32(bq.Vendor_id);
            Tbl_Vendorr ven = db.Tbl_Vendorr.Where(x => x.Vendor_id == id).SingleOrDefault();
            evn.Vendor_name = ven.Vendor_name;
            return View(bq);
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
            return View(Tbl_Books);
        }

        // POST: Bookcategoryy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Book_name,Book_Edition,Book_price,Book_img,auth_id,pub_id,cat_id,Vendor_id")]Tbl_Books Tbl_Books)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Tbl_Books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Tbl_Books);
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