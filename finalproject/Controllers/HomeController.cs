﻿using finalproject.Models;
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
    
        public ActionResult subscribe(tbl_subscribe subscribe,string subscriber_email)
        {

            subscribe.sub_date = DateTime.Now;
            subscribe.sub_email = subscriber_email;
            db.tbl_subscribe.Add(subscribe);
            db.SaveChanges();
            return PartialView();
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