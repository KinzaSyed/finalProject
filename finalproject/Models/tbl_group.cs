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
    
    public partial class tbl_group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_group()
        {
            this.tbl_blog = new HashSet<tbl_blog>();
            this.tbl_groupmem = new HashSet<tbl_groupmem>();
        }
    
        public int group_id { get; set; }
        public System.DateTime group_datetime { get; set; }
        public int group_createdby { get; set; }
        public string group_title { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_blog> tbl_blog { get; set; }
        public virtual tbl_member tbl_member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_groupmem> tbl_groupmem { get; set; }
    }
}
