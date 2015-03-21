using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MysqlContext;
using System.Data.SqlClient;
using SQLServer.Model;
using SQLServer.Client;

namespace MigrateToMysql
{
    public static class Migrator
    {
        public static void MigrateToMysql()
        {
            MigrateVendors();
            MigrateProducts();
            MigrateSales();
            MigrateExpenses();

        }

        private static void MigrateExpenses()
        {
            var expensesFromSqlServer = SqlServerClient.GetExpenses().ToList();

            using (var mysqlDb = new MysqlContect())
            {

                foreach (var expense in expensesFromSqlServer)
                {
                    Console.WriteLine(string.Format("{0} --> {1}", expense.Time,expense.Vendor.Vendor_Name));
                    int correspondingVendorId = GetRelatedVendorIdFromVendorName(expense.Vendor.Vendor_Name);

                    mysqlDb.Expenses.Add(new Expens() { ExpenseSum = expense.ExpenseSum, Time = expense.Time, VendorID = correspondingVendorId });
                    mysqlDb.SaveChanges();
                }
            }
        }

        private static void MigrateSales()
        {
            var salesFromSqlServer = SqlServerClient.GetSales().ToList();

            using (var mysqlDb = new MysqlContect())
            {

                foreach (var sale in salesFromSqlServer)
                {
                    int correspondingProductId = GetRelatedProductByProductName(sale.Product.Product_Name);

                    mysqlDb.Sales.Add(new Sale() { Date = sale.Date, PriceSum = sale.PriceSum, PriceUnit = sale.PriceUnit, Quantity = sale.Quantity, ProductID = correspondingProductId });
                    mysqlDb.SaveChanges();
                }
            }
        }

        private static void MigrateProducts()
        {
            var productsFromSqlServer = SqlServerClient.GetProducts().ToList();

            using (var mysqlDb = new MysqlContect())
            {

                foreach (var product in productsFromSqlServer)
                {
                    int correspondingVendorId = GetRelatedVendorIdFromVendorName(product.Vendor.Vendor_Name);

                    mysqlDb.Products.Add(new Product() { Price = product.Price, Product_Name = product.Product_Name, VendorID = correspondingVendorId });
                    mysqlDb.SaveChanges();
                }
            }
        }

        private static int GetRelatedVendorIdFromVendorName(string vendorName)
        {
            using (var mysqlDb = new MysqlContect())
            {
                int vendorId = mysqlDb.Vendors.Where(v => v.Vendor_Name == vendorName).FirstOrDefault().ID;
                return vendorId;
            }
        }

        private static int GetRelatedProductByProductName(string productName)
        {
            using (var mysqlDb = new MysqlContect())
            {
                int productId = mysqlDb.Products.Where(p=>p.Product_Name==productName).FirstOrDefault().ID;
                return productId;
            }
        }
        
        private static void MigrateVendors()
        {
            var vendorsFromSqlServer = SqlServerClient.GetVendors().ToList();
            using (var mysqlDb = new MysqlContect())
            {

                foreach (var vendor in vendorsFromSqlServer)
                {
                    mysqlDb.Vendors.Add(new Vendor() { Vendor_Name = vendor.Vendor_Name, Expenses = vendor.Expenses });
                    mysqlDb.SaveChanges();
                }
            }
        }
    }
}

