using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using SQLServer.Model;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SQLServer.Client
{
    static partial class SqlServerClient
    {
        private static SupermarketEntities dbContext = new SupermarketEntities();
        
        #region Get All 
        public static IEnumerable<Supermarket> GetSupermarkets()
        {
            return dbContext.Supermarkets;
        }
        public static IEnumerable<Product> GetProducts()
        {
            return dbContext.Products;
        }
        public static IEnumerable<Vendor> GetVendors()
        {
            return dbContext.Vendors;
        }
        public static IEnumerable<Sale> GetSales()
        {
            return dbContext.Sales;
        }
        public static IEnumerable<Measure> GetMeasures()
        {
            return dbContext.Measures;
        }
        public static IEnumerable<Model.Expens> GetExpenses()
        {
            return dbContext.Expenses;
        }
        #endregion

        #region GetById
        public static Supermarket GetSupermarketsById(int id)
        {
            return dbContext.Supermarkets.First( s => s.ID == id );
        }
        public static Product GetProductsById(int id)
        {
            return dbContext.Products.First( p => p.ID == id );
        }
        public static Vendor GetVendorsById(int id)
        {
            return dbContext.Vendors.First(s => s.ID == id);
        }
        public static Sale GetSalesById(int id)
        {
            return dbContext.Sales.First(s => s.ID == id);
        }
        public static Measure GetMeasuresById(int id)
        {
            return dbContext.Measures.First(s => s.ID == id);;
        }
        public static Expens GetExpensesById(int id)
        {
            return dbContext.Expenses.First(s => s.ID == id);;
        }
        #endregion

        #region Add

        public static void AddSupermarket(Supermarket item)
        {
            DbContextTransaction transaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Supermarkets.Add(item);
                dbContext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        public static void AddProduct(Product item)
        {
            DbContextTransaction transaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Products.Add(item);
                dbContext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        public static void AddVendor(Vendor item)
        {
            DbContextTransaction transaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Vendors.Add(item);
                dbContext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        public static void AddSale(Sale item)
        {
            DbContextTransaction transaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Sales.Add(item);
                dbContext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        public static void AddMeasure(Measure item)
        {
            DbContextTransaction transaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Measures.Add(item);
                dbContext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        public static void AddExpense(Expens item)
        {
            DbContextTransaction transaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Expenses.Add(item);
                dbContext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        
        #endregion

        #region Update With Expression

        public static void UpdateSupermarketByExpression(Expression<Func<Supermarket, bool>> expression, Supermarket newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                List<Supermarket> markets = dbContext.Supermarkets.Where(expression).ToList();
                markets.ForEach(m => m = newItem);

                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Dispose();
                throw;
            }
        }
        public static void UpdateProductByExpression(Expression<Func<Product, bool>> expression, Product newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                List<Product> products = dbContext.Products.Where(expression).ToList();
                products.ForEach(m => m = newItem);

                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Dispose();
                throw;
            }
        }
        public static void UpdateVendorByExpression(Expression<Func<Vendor, bool>> expression, Vendor newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                List<Vendor> Vendors = dbContext.Vendors.Where(expression).ToList();
                Vendors.ForEach(m => m = newItem);

                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }
        }
        public static void UpdateSaleByExpression(Expression<Func<Sale, bool>> expression, Sale newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                List<Sale> Sales = dbContext.Sales.Where(expression).ToList();
                Sales.ForEach(m => m = newItem);

                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }

        }
        public static void UpdateMeasureByExpression(Expression<Func<Measure, bool>> expression, Measure newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                List<Measure> Measures = dbContext.Measures.Where(expression).ToList();
                Measures.ForEach(m => m = newItem);

                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }
        }
        public static void UpdateExpenseByExpression(Expression<Func<Expens, bool>> expression, Expens newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                List<Expens> Expenses = dbContext.Expenses.Where(expression).ToList();
                Expenses.ForEach(m => m = newItem);

                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }
        }

        #endregion

        #region Update By Id
        public static void UpdateSupermarketById(int id, Supermarket newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                Supermarket market = dbContext.Supermarkets.First(s => s.ID == id);
                market = newItem;
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Dispose();
                throw;
            }
        }
        public static void UpdateProductById(int id, Product newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                Product product = dbContext.Products.First(s => s.ID == id);
                product = newItem;
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Dispose();
                throw;
            }
        }
        public static void UpdateVendorById(int id, Vendor newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                Vendor vendor = dbContext.Vendors.First(s => s.ID == id);
                vendor = newItem;
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }
        }
        public static void UpdateSaleById(int id, Sale newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                Sale sale = dbContext.Sales.First(s => s.ID == id);
                sale = newItem;
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();    
                throw;
            }
            
        }
        public static void UpdateMeasureById(int id, Measure newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                Measure measure = dbContext.Measures.First(s => s.ID == id);
                measure = newItem;
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }
        }
        public static void UpdateExpenseById(int id, Expens newItem)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                Expens expense = dbContext.Expenses.First(s => s.ID == id);
                expense = newItem;
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }
        }
        #endregion
    }

    static class TestClass
    {
        public static void Main()
        {
            ReadMarkets();
            Console.WriteLine("Adding Supermarket");
            Supermarket s = new Supermarket();
            s.Name = "Nov Supermarket2";
            SqlServerClient.AddSupermarket(s);
            ReadMarkets();
            Console.ReadKey();
        }

        public static void ReadMarkets()
        {
            foreach (var market in SqlServerClient.GetSupermarkets())
            {
                Console.WriteLine(market.Name);
            }
        }
    }
}
