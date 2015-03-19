using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo
{
    public class SqliteContext : DbContext
    {
        public DbSet<TaxInfo> Taxes { get; set; }
    }
}
