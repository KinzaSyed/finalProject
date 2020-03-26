using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace finalproject.Models
{
    [MetadataType(typeof(publisherMetaData))]
    public partial class Book_publisher
    {
        public class publisherMetaData
        {
            public int pub_id { get; set; }
            [DisplayName("Publisher Name")]
            public string pub_name { get; set; }
            [DisplayName("Email")]
            public string pub_email { get; set; }
            [DisplayName("Contact")]
            public string pub_contact { get; set; }

        }
    }
}