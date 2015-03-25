namespace JsonAndMongo
{
    using System.IO;
    using System.Linq;
    using MongoDB.Driver;
    using SQLServer.Model;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public static class ExportToMongo
    {
        const string DatabaseHost = "mongodb://127.0.0.1";
        const string DatabaseName = "MongoDB";
        const string Collection = "reports";


        public static void ExportReportsToMongoAndJson(string path, List<Sale> sales, List<Product> products)
        {
            var groups = sales.GroupBy(p => p.ProductID);

            foreach (var group in groups)
            {
                string vendorName = products.Find(p => p.ID == group.Key).Vendor.Vendor_Name;
                string productName = products.Find(p => p.ID == group.Key).Product_Name;
                decimal incomes = (decimal)group.Sum(p => p.PriceSum);
                int quantitySold = (int)group.Sum(p => p.Quantity);
                int id = (int)group.Key;

                var report = generateReport(id, productName, vendorName, quantitySold, incomes);

                ExportReportToFile(path, report, id);
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

        private static void ExportReportToFile(string path, SalesByProductReports report, int id)
        {
            string json = JsonConvert.SerializeObject(report, Formatting.Indented);

            File.WriteAllText(path + id + @".json", json);
        }

        static MongoDatabase GetDatabase(string name, string fromHost)
        {
            var mongoClient = new MongoClient(fromHost);
            var server = mongoClient.GetServer();
            return server.GetDatabase(name);
        }
    }
}
