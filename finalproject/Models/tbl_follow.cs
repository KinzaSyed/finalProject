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
    
    public partial class tbl_follow
    {
        public int follow_id { get; set; }
        public Nullable<System.DateTime> follow_time { get; set; }
        public int followedby_id { get; set; }
        public int followed_id { get; set; }
    
        public virtual tbl_member tbl_member { get; set; }
        public virtual tbl_member tbl_member1 { get; set; }
    }
}