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
    
    public partial class Tbl_Books
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Books()
        {
            this.Book_Review = new HashSet<Book_Review>();
            this.BTransactions = new HashSet<BTransaction>();
            this.tbl_wishlist = new HashSet<tbl_wishlist>();
        }
    
        public int Book_id { get; set; }
        public string Book_name { get; set; }
        public string Book_Edition { get; set; }
        public Nullable<int> Book_price { get; set; }
        public string Book_img { get; set; }
        public Nullable<int> auth_id { get; set; }
        public Nullable<int> pub_id { get; set; }
        public Nullable<int> cat_id { get; set; }
        public Nullable<int> Vendor_id { get; set; }
    
        public virtual Book_author Book_author { get; set; }
        public virtual Book_categoryy Book_categoryy { get; set; }
        public virtual Book_publisher Book_publisher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book_Review> Book_Review { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BTransaction> BTransactions { get; set; }
        public virtual Tbl_Vendorr Tbl_Vendorr { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_wishlist> tbl_wishlist { get; set; }
    }
}