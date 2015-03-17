using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonAndMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sm = new SupermarketEntities();

            var groups = sm.Sales.GroupBy(p => p.ProductID);

            foreach (var group in groups)
            {
                string vendorName = sm.Products.Find(group.Key).Vendor.Vendor_Name;
                //Console.WriteLine(vendorName);
                string productName = sm.Products.Find(group.Key).Product_Name;
                //Console.WriteLine(productName);
                decimal incomes = (decimal)group.Sum(p => p.PriceSum);
                //Console.WriteLine(incomes);
                int quantitySold = (int)group.Sum(p => p.Quantity);
                //Console.WriteLine(quantitySold);
                //Console.WriteLine();
                int id = (int)group.Key;
                generateJson(id, productName, vendorName, quantitySold, incomes);

            }
        }

        private static void generateJson(int id, string productName, string vendorName, int totalQuantity, decimal incomes)
        {
            var report = new SalesByProductReports(id, productName, vendorName, totalQuantity, incomes);

            string serializedReport = JsonConvert.SerializeObject(report, Formatting.Indented);

            ExportJsonToFile(serializedReport, id);
        }

        private static void ExportJsonToFile(string json, int id)
        {
            File.WriteAllText(@"../../JsonReports/" + id + @".json", json);
        }
    }
}
