using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonAndMongo
{
    public class SalesByProductReports
    {
        public SalesByProductReports(int id, string productName, string vendorName, int totalQuantity, decimal incomes)
        {
            this.Id = id;
            this.ProductName = productName;
            this.VendorName = vendorName;
            this.Quantity = totalQuantity;
            this.Incomes = incomes;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public int Quantity { get; set; }
        public decimal Incomes { get; set; }

        public override string ToString()
        {
            return string.Format("product-id: {0}, product-name: {1}, vandor-name: {2}, total-quantity-sold: {3}, total-incomes: {3}",
                this.Id, this.ProductName, this.VendorName, this.Quantity,this.Incomes);
        }
    }
}
