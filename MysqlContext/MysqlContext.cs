using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLServer.Model;

namespace MysqlContext
{
    public class MysqlContect : DbContext
    {
        public DbSet<Expens> Expenses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supermarket> Supermarkets { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
    }
}
