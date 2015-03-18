namespace OracleDb.Data
{
    using System.Data.Entity;
    using OracleDb.Model;

    public class OracleDbContext : DbContext
    {
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Vendor> Vendors { get; set; }
        public IDbSet<Measure> Measures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HR");
        }
    }
}
