//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesReports
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.Sales = new HashSet<Sale>();
            this.Supermarkets = new HashSet<Supermarket>();
        }
    
        public int ID { get; set; }
        public int VendorID { get; set; }
        public string Product_Name { get; set; }
        public Nullable<int> MeasureID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> SupermarketID { get; set; }
    
        public virtual Measure Measure { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Supermarket> Supermarkets { get; set; }
    }
}
