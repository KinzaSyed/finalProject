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
    
    public partial class Tbl_Vendorr
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Vendorr()
        {
            this.Tbl_Books = new HashSet<Tbl_Books>();
        }
    
        public int Vendor_id { get; set; }
        public string Vendor_name { get; set; }
        public string Vendor_email { get; set; }
        public string Vendor_password { get; set; }
        public Nullable<int> Vendor_contactno { get; set; }
        public string Vendor_status { get; set; }
        public string Vendor_ShopAdr { get; set; }
        public Nullable<int> Admin_id { get; set; }
    
        public virtual Tbl_admin Tbl_admin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Books> Tbl_Books { get; set; }
    }
}
