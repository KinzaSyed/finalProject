using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class Tbl_for_books
    {


        public int Book_id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Book_name { get; set; }
        [Required(ErrorMessage = "*")]
        public string Book_Edition { get; set; }
        [Required(ErrorMessage = "*")]
        public Nullable<int> Book_price { get; set; }


        [Required(ErrorMessage = "*")]

        public string Book_img { get; set; }


        public int auth_id { get; set; }
        public string auth_name { get; set; }


        public int cat_id { get; set; }
        public string cat_name { get; set; }


        public int pub_id { get; set; }
        public string pub_name { get; set; }

        public int Vendor_id { get; set; }
        public string Vendor_name { get; set; }


    }
}