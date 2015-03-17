namespace OracleDb.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //Set name of the table
    [Table("MEASURES")]
    public class Measure
    {
        //Primary key Measure
        [Key]
        public int ID { get; set; }

        [MaxLength(100)]
        public string MEASURE_NAME { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
