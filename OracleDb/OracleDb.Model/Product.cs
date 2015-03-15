namespace OracleDb.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //Set name of the table
    [Table("PRODUCTS")]
    public class Product
    {
        //Primary key Product.
        [Key]
        public int ID { get; set; }

        //Foreign key Vendor.
        public int VENDOR_ID { get; set; }

        [MaxLength(100)]
        public string PRODUCT_NAME { get; set; }

        //Foreign key Measure.
        public int MEASURE_ID { get; set; }

        public decimal PRICE { get; set; }

        [ForeignKey("VENDOR_ID")]
        public virtual Vendor Vendor { get; set; }

        [ForeignKey("MEASURE_ID")]
        public virtual Measure Measure { get; set; }
    }
}
