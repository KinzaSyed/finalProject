using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    [MetadataType(typeof(memberMetaData))]

    public partial class tbl_member
    {
        public class memberMetaData
        {
            [DisplayName("Member Name")]
            public string mem_name { get; set; }
            [DisplayName("Email")]

            public string mem_email { get; set; }
            [DisplayName("Contact")]

            public string mem_contact { get; set; }
            [DisplayName("Password")]

            
            public string mem_password { get; set; }
            [DisplayName("Address")]

            public string mem_address { get; set; }
            [DisplayName("Date of Birth")]

            public string mem_dob { get; set; }

        }
    }
}