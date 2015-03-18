using MongoDB.Driver;
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
        const string DatabaseHost = "mongodb://127.0.0.1";
        const string DatabaseName = "MongoDB";
        const string Collection = "reports";


        static void Main(string[] args)
        {
            var sm = new SupermarketEntities();

            var groups = sm.Sales.GroupBy(p => p.ProductID);

            foreach (var group in groups)
            {
                string vendorName = sm.Products.Find(group.Key).Vendor.Vendor_Name;

                string productName = sm.Products.Find(group.Key).Product_Name;
                decimal incomes = (decimal)group.Sum(p => p.PriceSum);
                int quantitySold = (int)group.Sum(p => p.Quantity);
                int id = (int)group.Key;

                var report = generateReport(id, productName, vendorName, quantitySold, incomes);

                ExportReportToFile(report, id);
                InsertReportInMongo(report);
            }
        }

        private static SalesByProductReports generateReport(int id, string productName, string vendorName, int totalQuantity, decimal incomes)
        {
            var report = new SalesByProductReports(id, productName, vendorName, totalQuantity, incomes);

            return report;
        }

        private static void InsertReportInMongo(SalesByProductReports report)
        {
            var db = GetDatabase(DatabaseName, DatabaseHost);

            var salesByProductReports = db.GetCollection<SalesByProductReports>(Collection);
            salesByProductReports.Insert<SalesByProductReports>(report);
        }

        private static void ExportReportToFile(SalesByProductReports report, int id)
        {
            string json = JsonConvert.SerializeObject(report, Formatting.Indented);

            File.WriteAllText(@"../../JsonReports/" + id + @".json", json);
        }

        static MongoDatabase GetDatabase(string name, string fromHost)
        {
            var mongoClient = new MongoClient(fromHost);
            var server = mongoClient.GetServer();
            return server.GetDatabase(name);
        }
    }
}
