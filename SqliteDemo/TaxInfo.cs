using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo
{
    public class TaxInfo
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public double TaxInPercentage { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}; ProductName: {1}; TaxInPercentage: {2}", this.Id, this.ProductName, this.TaxInPercentage);
        }
    }
}
