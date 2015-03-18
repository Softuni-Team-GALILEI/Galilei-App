namespace SQLServer.Client
{
    using System;
    using System.Linq;
    using SQLServer.Model;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    public static partial class SqlServerClient
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
        public static Measure GetMeasureById(int id)
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
                foreach (Supermarket market in markets)
                {
                    market.Name = newItem.Name;
                }

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
                foreach (Product product in products)
                {
                    if (newItem.Price!= null)
                    {
                        product.Price = newItem.Price;   
                    }
                    if (newItem.Product_Name != null)
                    {
                        product.Product_Name = newItem.Product_Name;   
                    }
                }
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
                foreach (Vendor vendor in Vendors)
                {
                    vendor.Vendor_Name = newItem.Vendor_Name;
                    if (newItem.Expenses != null)
                    {
                        vendor.Products = newItem.Products;  
                    }
                    if (newItem.Expenses != null)
                    {
                        vendor.Expenses = newItem.Expenses;
                    }
                }

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
                foreach (Sale sale in Sales)
                {
                    if (sale.PriceSum != null)
                    {
                        sale.PriceSum = newItem.PriceSum;
                    }
                    if (sale.Date != null)
                    {
                        sale.Date = newItem.Date;
                    }
                    if (sale.Supermarket != null)
                    {
                        sale.Supermarket = newItem.Supermarket;
                    }
                    if (sale.PriceUnit != null)
                    {
                        sale.PriceUnit = newItem.PriceUnit;
                    }
                    if (sale.Quantity != null)
                    {
                        sale.Quantity = newItem.Quantity;
                    }
                }
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
                foreach (Measure measure in Measures)
                {
                    if (newItem.Measure_Name != null)
                    {
                        measure.Measure_Name = newItem.Measure_Name;
                    }
                }
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
                foreach (Expens expense in Expenses)
                {
                    if (newItem.ExpenseSum != null)
                    {
                        expense.ExpenseSum = newItem.ExpenseSum;
                    }
                    if (newItem.Time != null)
                    {
                        expense.Time = newItem.Time;
                    }
                }

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

        #region Delete By Id
        public static void DeleteSupermarketById(int id)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Supermarkets.Remove(GetSupermarketsById(id));
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Dispose();
                throw;
            }
        }
        public static void DeleteProductById(int id)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Products.Remove(GetProductsById(id));
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Dispose();
                throw;
            }
        }
        public static void DeleteVendorById(int id)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Vendors.Remove(GetVendorsById(id));
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }
        }
        public static void DeleteSaleById(int id)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Sales.Remove(GetSalesById(id));
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }

        }
        public static void DeleteMeasureById(int id)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Measures.Remove(GetMeasureById(id));
                dbContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }
        }
        public static void DeleteExpenseById(int id)
        {
            var dbTransaction = dbContext.Database.BeginTransaction();
            try
            {
                dbContext.Expenses.Remove(GetExpensesById(1));
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
    #region TestClass
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
            Supermarket s2 = new Supermarket();
            s2.Name = "O6tePoNovMarket";
            SqlServerClient.UpdateSupermarketByExpression(supermarket => supermarket.Name == "Nov Supermarket", s2);
            ReadMarkets();
            SqlServerClient.DeleteSupermarketById(6);
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
#endregion
}
