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
    
    public partial class tbl_blog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_blog()
        {
            this.tbl_cmnts = new HashSet<tbl_cmnts>();
        }
    
        public int blog_id { get; set; }
        public System.DateTime blog_datetime { get; set; }
        public int blog_groupid { get; set; }
        public int blog_createdby { get; set; }
        public string blog_img { get; set; }
        public string blog_title { get; set; }
        public byte[] blog_body { get; set; }
    
        public virtual tbl_group tbl_group { get; set; }
        public virtual tbl_member tbl_member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_cmnts> tbl_cmnts { get; set; }
    }
}
