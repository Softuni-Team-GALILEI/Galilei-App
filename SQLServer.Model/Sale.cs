//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SQLServer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sale
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> PriceSum { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> SupermarketID { get; set; }
        public Nullable<decimal> PriceUnit { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Supermarket Supermarket { get; set; }
    }
}
