using finalproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace finalproject.Controllers
{
    public class HomeController : Controller
    {

        lastDbEntities db = new lastDbEntities();

        //public ActionResult Searching(string searchBy, string search, int? page)
        //{


        //    if (searchBy == "cat_name")
        //    {
        //        return View(db.Tbl_Books.Where(x => x.Book_categoryy.cat_name.StartsWith(search) || search == null)
        //            .ToList().ToPagedList(page ?? 1, 3));
        //    }
        //    if (searchBy == "Book_name")
        //    {
        //        return View(db.Tbl_Books.Where(x => x.Book_name.StartsWith(search) || search == null)
        //            .ToList().ToPagedList(page ?? 1, 3));
        //    }
        //    else
        //        return View(db.Tbl_Books.Where(x => x.pub_id.StartsWith(search) || search == null)
        //            .ToList().ToPagedList(page ?? 1, 3));
        //}

        public ActionResult vendorreaderLogin(String email, String pass,string sel)
        {
            if (sel == "reader")
            {
                tbl_member model = db.tbl_member.Where(x => x.mem_email == email && x.mem_password == pass).SingleOrDefault();

                if (model != null)
                {

                    Session["mem_email"] = model.mem_email;
                    Session["mem_id"] = model.mem_id;

                    Session["mem_password"] = model.mem_password;
                    return RedirectToAction("UserPanel", "Tbl_member");

                }
                
               
            }
            else
            {
                Tbl_Vendorr model1 = db.Tbl_Vendorr
                    .Where(x => x.Vendor_email == email
                    && x.Vendor_password == pass).SingleOrDefault();

                if (model1 != null)
                {
                    Session["Vendor_email"] = model1.Vendor_email;
                    Session["Vendor_id"] = model1.Vendor_id;
                    return RedirectToAction("VendorPanel", "Tbl_Vendorr");

                }
               
            }
            return View();
        }

        public ActionResult ContactUs(tbl_contactus contactus, string email, string content)
        {
            contactus.contactus_content = content;
            contactus.contactus_email = email;
            contactus.contactys_datetime = DateTime.Now;
            db.tbl_contactus.Add(contactus);
            db.SaveChanges();
            return RedirectToAction("Contact");
        }
        [HttpGet]
        public ActionResult subscribe(/*tbl_subscribe subscribe,string subscriber_email*/)
        {

            //subscribe.sub_date = DateTime.Now;
            //subscribe.sub_email = subscriber_email;
            //db.tbl_subscribe.Add(subscribe);
            //db.SaveChanges();
            return View();
        }
        [HttpPost]
        public ActionResult subscribe(tbl_subscribe subscribe, string subscriber_email)
        {

            subscribe.sub_date = DateTime.Now;
            subscribe.sub_email = subscriber_email;
            db.tbl_subscribe.Add(subscribe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}