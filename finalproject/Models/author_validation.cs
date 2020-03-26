using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace finalproject.Models
{

    [MetadataType(typeof(authorMetaData))]
    public partial class Book_author
    {

        

        public class authorMetaData
        {


            [DisplayName("Author Name")]
            public string auth_name { get; set; }

            [DisplayName("Email")]
            public string auth_email { get; set; }
            [DisplayName("Contact")]
            public string auth_contact { get; set; }
       
           
           
            
           


        }
    }
}