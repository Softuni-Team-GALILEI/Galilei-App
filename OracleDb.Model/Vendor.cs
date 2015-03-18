namespace OracleDb.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //Set name of the table
    [Table("VENDORS")]
    public class Vendor
    {
        //Primary key Vendor
        [Key]
        public int ID { get; set; }

        [MaxLength(100)]
        public string VENDOR_NAME { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
