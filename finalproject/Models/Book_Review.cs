//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace finalproject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book_Review
    {
        public int Review_id { get; set; }
        public string ContentC { get; set; }
        public double Rating { get; set; }
        public System.DateTime DatePost { get; set; }
        public int BookId { get; set; }
        public int memberId { get; set; }
    
        public virtual tbl_member tbl_member { get; set; }
        public virtual Tbl_Books Tbl_Books { get; set; }
    }
}