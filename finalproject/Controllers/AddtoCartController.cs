using finalproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace finalproject.Controllers
{
    public class AddtoCartController : Controller
    {
        public string ses;

        private lastDbEntities db = new lastDbEntities();


       

        // GET: AddtoCart
        public ActionResult Index()
        {
            if (Session["mem_id"] != null && Session["mem_email"] != null)
            {
                ses = Session["mem_id"].ToString();

            }
            Session["mem_id"] = ses;
            if (TempData["cart"] != null)
            {
                float x = 0;
                List<cart> li2 = TempData["cart"] as List<cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                }
                TempData["totalprice"] = x;
            }
            TempData.Keep();
            return View(db.Tbl_Books.OrderByDescending(x => x.Book_id).ToList());
        }




        public ActionResult addtocart(int? id)
        {
            Tbl_Books book = db.Tbl_Books.Where(x => x.Book_id == id).SingleOrDefault();
            return View(book);
        }
        List<cart> li = new List<cart>();

        [HttpPost]
        public ActionResult addtocart(Tbl_Books book, string qty, int id)
        {
            Tbl_Books b1 = db.Tbl_Books.Where(x => x.Book_id == id).SingleOrDefault();
            cart c = new cart();
            c.Book_id = b1.Book_id;
            c.Book_name = b1.Book_name;
            c.Book_price = (float)b1.Book_price;
            c.qty = Convert.ToInt32(qty);
            c.bill = c.Book_price * c.qty;
            if (TempData["cart"] == null)
            {

                li.Add(c);
                TempData["cart"] = li;

            }
            else
            {
                List<cart> li2 = TempData["cart"] as List<cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.Book_id == c.Book_id)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);
                }

                TempData["cart"] = li2;
            }



            TempData.Keep();


            return RedirectToAction("Index");
        }


        public ActionResult CheckOut()
        {

            TempData.Keep();
            return View();
        }


        [HttpPost]
        public ActionResult CheckOut(BTransaction O)
        {
            List<cart> li2 = TempData["cart"] as List<cart>;

            Invoice iv = new Invoice();
            iv.mem_id = Convert.ToInt32(Session["mem_id"].ToString());
            iv.in_date = System.DateTime.Now;
            iv.in_totalbill = (float)TempData["totalprice"];
            db.Invoices.Add(iv);
            db.SaveChanges();
            foreach (var item in li2)
            {
                BTransaction od = new BTransaction();
                od.Book_id = item.Book_id;
                od.in_id = iv.in_id;
                od.Transaction_date = System.DateTime.Now;
                od.Transaction_qty = item.qty;
                od.mem_id = Convert.ToInt32(Session["mem_id"].ToString());
                od.Transaction_price = (int)item.Book_price;
                od.Transaction_bill = (int)item.bill;
                db.BTransactions.Add(od);
                db.SaveChanges();
            }
            TempData.Remove("totalPrice");
            TempData.Remove("cart");
            TempData["msg"] = "Transaction Completed";
            TempData.Keep();
            return RedirectToAction("Index");
        }
        public ActionResult remove(int? id)
        {
            List<cart> li2 = TempData["cart"] as List<cart>;
            cart c = li2.Where(x => x.Book_id == id).SingleOrDefault();
            li2.Remove(c);
            float h = 0;
            foreach (var item in li2)
            {
                h += item.bill;
            }
            TempData["totalprice"] = h;

            return RedirectToAction("CheckOut");
        }

    }
}