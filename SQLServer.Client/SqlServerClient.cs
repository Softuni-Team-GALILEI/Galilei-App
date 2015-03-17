using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
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

        //Code Goes here
        
        #endregion

        #region Update With Expression
        #endregion

        #region Update By Id
        public static Supermarket UpdateSupermarketById(int id)
        {
            throw new NotImplementedException();
        }
        public static Product UpdateProductById(int id)
        {
            throw new NotImplementedException();
        }
        public static Vendor UpdateVendorById(int id)
        {
            throw new NotImplementedException();
        }
        public static Sale UpdateSaleById(int id)
        {
            throw new NotImplementedException();
        }
        public static Measure UpdateMeasureById(int id)
        {
            throw new NotImplementedException();
        }
        public static Expens UpdateExpenseById(int id, Expens newItem)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    static class TestClass
    {
        public static void Main()
        {
            
            foreach (var market in SqlServerClient.GetSupermarkets())
            {
                Console.WriteLine(market.Name);
            }
            Console.ReadKey();
            foreach (var market in SqlServerClient.GetMeasures())
            {
                Console.WriteLine(market.Measure_Name);
            }
            Console.ReadKey();
            foreach (var market in SqlServerClient.GetProducts())
            {
                Console.WriteLine(market.Product_Name);
            }
            Console.ReadKey();
            foreach (var market in SqlServerClient.GetVendors())
            {
                Console.WriteLine(market.Vendor_Name);
            }
            Console.ReadKey();
            foreach (var market in SqlServerClient.GetExpenses())
            {
                Console.WriteLine(market.ExpenseSum);
            }
            Console.ReadKey();
        }
    }
}
