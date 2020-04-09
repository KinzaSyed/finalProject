using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    public class Tbl_for_Ebooks
    {


        public int Ebook_id { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("EBook Name")]
        public string Ebook_name { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Ebook Publisher")]
        public string Ebook_publisher { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Ebook Author")]
        public string Ebook_author { get; set; }


        
        public int cat_id { get; set; }
        [DisplayName("EBook Category")]
        public string cat_name { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("EBook Image")]
        public string Ebook_img { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("EBook Pdf")]
        public string Ebook_pdffile { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Ebook Edition")]
        public string Ebook_edition { get; set; }



        public int mem_id { get; set; }
        [DisplayName("Member Name")]
        public string mem_name { get; set; }
      

    }
}