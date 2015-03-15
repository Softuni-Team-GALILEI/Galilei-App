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

                if (vendorsForUpdate.Count == 0)
                {
                    throw new InvalidOperationException("No Vendors found.");
                }

                foreach (var v in vendorsForUpdate)
                {
                    v.VENDOR_NAME = vendor.VENDOR_NAME;
                }


                return db.SaveChanges();
            }
        }

        public static int UpdateVendorById(int id, Vendor vendor)
        {
            using (var db = new OracleDbContext())
            {
                
                var vendorForUpdate = db.Vendors.Where(v => v.ID == id).FirstOrDefault();

                if (vendorForUpdate == null)
                {
                    throw new InvalidOperationException("Vendor with this id does not exists.");
                }

                vendorForUpdate.VENDOR_NAME = vendor.VENDOR_NAME;

                return db.SaveChanges();
            }
        }

        public static int DeleteVendorsByExpression(Expression<Func<Vendor, bool>> expression)
        {
            using (var db = new OracleDbContext())
            {
                var vendorsForDelete = db.Vendors.Where(expression).ToList();

                if (vendorsForDelete.Count == 0)
                {
                    throw new InvalidOperationException("No Vendors found.");
                }

                foreach (var v in vendorsForDelete)
                {
                    db.Vendors.Remove(v);
                }

                return db.SaveChanges();
            }
        }

        public static int DeleteVendorById(int id)
        {
            using (var db = new OracleDbContext())
            {
                var vendorsForDelete = db.Vendors.Where(v => v.ID == id).FirstOrDefault();

                if (vendorsForDelete == null)
                {
                    throw new InvalidOperationException("Vendor with this id does not exists.");
                }

                db.Vendors.Remove(vendorsForDelete);
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

                if (measuresForUpdate.Count == 0)
                {
                    throw new InvalidOperationException("No Measures found");
                }

                foreach (var m in measuresForUpdate)
                {
                    m.MEASURE_NAME = measure.MEASURE_NAME;
                }

                return db.SaveChanges();
            }
        }

        public static int UpdateMeasureById(int id, Measure measure)
        {
            using (var db = new OracleDbContext())
            {
                var measureForUpdate = db.Measures.Where(m => m.ID == id).FirstOrDefault();

                if (measureForUpdate == null)
                {
                    throw new InvalidOperationException("Measure with this ide does not exists.");
                }

                measureForUpdate.MEASURE_NAME = measure.MEASURE_NAME;

                return db.SaveChanges();
            }
        }

        public static int DeleteMeasuresByExpression
            (Expression<Func<Measure, bool>> expression)
        {
            using (var db = new OracleDbContext())
            {
                var measuresForDelete = db.Measures.Where(expression).ToList();

                if (measuresForDelete.Count == 0)
                {
                    throw new InvalidOperationException("No Measures found.");
                }

                foreach (var m in measuresForDelete)
                {
                    db.Measures.Remove(m);
                }

                return db.SaveChanges();
            }
        }

        public static int DeleteMeasureById(int id)
        {
            using (var db = new OracleDbContext())
            {
                var measureForDelete = db.Measures.Where(m => m.ID == id).FirstOrDefault();

                if (measureForDelete == null)
                {
                    throw new InvalidOperationException("Measure with such id does not exists.");
                }

                db.Measures.Remove(measureForDelete);

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
    }
}
