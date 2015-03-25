namespace OracleDb.Client
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;

    using OracleDb.Model;
    using OracleDb.Data;
    using System.Linq.Expressions;

    public static class OracleDbClient
    {
        public static void InitDb()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<OracleDbContext>());

            var db = new OracleDbContext();

            //If not get table from database she's not initialize.
            var vendors = db.Vendors.ToList();
        }

        public static int CreateVendor(Vendor vendor)
        {
            using (var db = new OracleDbContext())
            {
                db.Vendors.Add(vendor);

                return db.SaveChanges();
            }
        }

        public static ICollection<Vendor> ReadAllVendors()
        {
            using (var db = new OracleDbContext())
            {
                return db.Vendors.ToList();
            }
        }

        public static Vendor ReadVendorById(int id)
        {
            using (var db = new OracleDbContext())
            {
                return db.Vendors.Where(v => v.ID == id).FirstOrDefault();
            }
        }

        public static ICollection<Vendor> ReadVendorsByExpression(Expression<Func<Vendor,bool>> expression)
        {
            using (var db = new OracleDbContext())
            {
                return db.Vendors.Where(expression).ToList();
            }
        }

        public static int UpdateVendorsByExpression
            (Expression<Func<Vendor, bool>> expression, Vendor vendor)
        {
            using (var db = new OracleDbContext())
            {
                var vendorsForUpdate = db.Vendors.Where(expression).ToList();

                if (vendorsForUpdate.Count > 0)
                {
                    foreach (var v in vendorsForUpdate)
                    {
                        v.VENDOR_NAME = vendor.VENDOR_NAME;
                    }
                }

                return db.SaveChanges();
            }
        }

        public static int UpdateVendorById(int id, Vendor vendor)
        {
            using (var db = new OracleDbContext())
            {
                
                var vendorForUpdate = db.Vendors.Where(v => v.ID == id).FirstOrDefault();

                if (vendorForUpdate != null)
                {
                    vendorForUpdate.VENDOR_NAME = vendor.VENDOR_NAME;
                }

                return db.SaveChanges();
            }
        }

        public static int DeleteVendorsByExpression(Expression<Func<Vendor, bool>> expression)
        {
            using (var db = new OracleDbContext())
            {
                var vendorsForDelete = db.Vendors.Where(expression).ToList();

                if (vendorsForDelete.Count > 0)
                {
                    foreach (var v in vendorsForDelete)
                    {
                        db.Vendors.Remove(v);
                    }
                }

                return db.SaveChanges();
            }
        }

        public static int DeleteVendorById(int id)
        {
            using (var db = new OracleDbContext())
            {
                var vendorsForDelete = db.Vendors.Where(v => v.ID == id).FirstOrDefault();

                if (vendorsForDelete != null)
                {
                    db.Vendors.Remove(vendorsForDelete);
                }

                return db.SaveChanges();
            }
        }

        public static int CreateMeasaur(Measure measure)
        {
            using (var db = new OracleDbContext())
            {
                db.Measures.Add(measure);
                return db.SaveChanges();
            }
        }

        public static ICollection<Measure> ReadAllMeasures()
        {

            using (var db = new OracleDbContext())
            {
                return db.Measures.ToList();
            }
        }

        public static Measure ReadMeasureById(int id)
        {
            using (var db = new OracleDbContext())
            {
                return db.Measures.Where(m => m.ID == id).FirstOrDefault();
            }
        }

        public static ICollection<Measure> ReadMeasuresByExpression(Expression<Func<Measure, bool>> expression)
        {
            using (var db = new OracleDbContext())
            {
                return db.Measures.Where(expression).ToList();
            }
        }

        public static int UpdateMeasuresByExpression
            (Expression<Func<Measure, bool>> expression, Measure measure)
        {
            using (var db = new OracleDbContext())
            {
                var measuresForUpdate = db.Measures.Where(expression).ToList();

                if (measuresForUpdate.Count > 0)
                {
                    foreach (var m in measuresForUpdate)
                    {
                        m.MEASURE_NAME = measure.MEASURE_NAME;
                    }
                }

                return db.SaveChanges();
            }
        }

        public static int UpdateMeasureById(int id, Measure measure)
        {
            using (var db = new OracleDbContext())
            {
                var measureForUpdate = db.Measures.Where(m => m.ID == id).FirstOrDefault();

                if (measureForUpdate != null)
                {
                    measureForUpdate.MEASURE_NAME = measure.MEASURE_NAME;
                }

                return db.SaveChanges();
            }
        }

        public static int DeleteMeasuresByExpression
            (Expression<Func<Measure, bool>> expression)
        {
            using (var db = new OracleDbContext())
            {
                var measuresForDelete = db.Measures.Where(expression).ToList();

                if (measuresForDelete.Count > 0)
                {
                    foreach (var m in measuresForDelete)
                    {
                        db.Measures.Remove(m);
                    }
                }

                return db.SaveChanges();
            }
        }

        public static int DeleteMeasureById(int id)
        {
            using (var db = new OracleDbContext())
            {
                var measureForDelete = db.Measures.Where(m => m.ID == id).FirstOrDefault();

                if (measureForDelete != null)
                {
                    db.Measures.Remove(measureForDelete);
                }

                return db.SaveChanges();
            }
        }

        public static int CreateProduct(Product product)
        {
            using (var db = new OracleDbContext())
            {
                db.Products.Add(product);
                return db.SaveChanges();
            }
        }

        public static ICollection<Product> ReadAllProducts()
        {
            using (var db = new OracleDbContext())
            {
                return db.Products.ToList();
            }
        }

        public static Product ReadProductById(int id)
        {
            using (var db = new OracleDbContext())
            {
                return db.Products.Where(p => p.ID == id).FirstOrDefault();
            }
        }

        public static ICollection<Product> ReadProductsByExpression(Expression<Func<Product, bool>> expression)
        {
            using (var db = new OracleDbContext())
            {
                return db.Products.Where(expression).ToList();
            }
        }

        public static int UpdateProductsByExpression
            (Expression<Func<Product, bool>> expression, Product product)
        {
            using (var db = new OracleDbContext())
            {
                var productsForUpdate = db.Products.Where(expression).ToList();

                if (productsForUpdate.Count > 0)
                {
                    foreach (var p in productsForUpdate)
                    {
                        p.VENDOR_ID = product.VENDOR_ID != null ? product.VENDOR_ID : p.VENDOR_ID;
                        p.PRODUCT_NAME = product.PRODUCT_NAME != null ? product.PRODUCT_NAME : p.PRODUCT_NAME;
                        p.MEASURE_ID = product.MEASURE_ID != null ? product.MEASURE_ID : p.MEASURE_ID;
                        p.PRICE = product.PRICE != null ? product.PRICE : p.PRICE;
                    }
                }

                return db.SaveChanges();
            }
        }

        public static int UpdateProductById(int id, Product product)
        {
            using (var db = new OracleDbContext())
            {
                var productForUpdate = db.Products.Where(p => p.ID == id).FirstOrDefault();

                if (productForUpdate != null)
                {
                        productForUpdate.VENDOR_ID = product.VENDOR_ID != null 
                            ? product.VENDOR_ID : productForUpdate.VENDOR_ID;

                        productForUpdate.PRODUCT_NAME = product.PRODUCT_NAME != null
                            ? product.PRODUCT_NAME : productForUpdate.PRODUCT_NAME;

                        productForUpdate.MEASURE_ID = product.MEASURE_ID != null
                            ? product.MEASURE_ID : productForUpdate.MEASURE_ID;

                        productForUpdate.PRICE = product.PRICE !=null
                            ? product.PRICE : productForUpdate.PRICE;
                }

                return db.SaveChanges();
            }
        }

        public static int DeleteProductsByExpression
            (Expression<Func<Product, bool>> expression)
        {
            using (var db = new OracleDbContext())
            {
                var productsForDelete = db.Products.Where(expression).ToList();

                if (productsForDelete.Count > 0)
                {
                    foreach (var p in productsForDelete)
                    {
                        db.Products.Remove(p);
                    }
                }

                return db.SaveChanges();
            }
        }

        public static int DeleteProductById(int id)
        {
            using (var db = new OracleDbContext())
            {
                var productForDelete = db.Products.Where(p => p.ID == id).FirstOrDefault();

                if (productForDelete != null)
                {
                    db.Products.Remove(productForDelete);
                }

                return db.SaveChanges();
            }
        }

        public static void AddDataToOracle() 
        {
            string[] measureNames = {"Ton", "Gallon", "Mililitres", "Newton", "Centimetre"};
            string[] vendorNames = {"Shezhen Electronics", "Sofia Beer Company", "Bansko Water Farm"};
            string[] productNames = {"Teqila Sofia", "Cake Malinka"};



            using (var db = new OracleDbContext())
            {
                foreach (var item in db.Products)
                {
                    
                Console.WriteLine();
                }
                //foreach(string str in measureNames)
                //{
                //    db.Measures.Add(new Measure(){MEASURE_NAME = str});
                //    db.SaveChanges();
                //}
                //foreach (string str in vendorNames) 
                //{
                //    db.Vendors.Add(new Vendor(){VENDOR_NAME = str});
                //    db.SaveChanges();
                //}
                //foreach(string str in productNames)
                //{
                //    Random r = new Random();
                //    db.Products.Add(new Product()
                //    {
                //        PRODUCT_NAME = str,
                //        VENDOR_ID = db.Vendors.ToList()[r.Next(0, db.Vendors.Count())].ID,
                //        PRICE = (decimal)(r.NextDouble() * r.Next(0, 1000)),
                //        MEASURE_ID = db.Measures.ToList()[r.Next(0, db.Measures.Count())].ID
                //    });
                //    db.SaveChanges();
                //}
                
            }
        }
    }
}
