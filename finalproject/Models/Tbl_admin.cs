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
    
    public partial class Tbl_admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_admin()
        {
            this.Tbl_Vendorr = new HashSet<Tbl_Vendorr>();
        }
    
        public int Admin_id { get; set; }
        public string Admin_name { get; set; }
        public string Admin_email { get; set; }
        public string Admin_password { get; set; }
        public string Admin_contact { get; set; }
        public string Admin_status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Vendorr> Tbl_Vendorr { get; set; }
    }
}
