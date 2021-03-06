﻿namespace OracleToSQLServerBridge
{
    using OracleDb.Client;
    using SQLServer.Client;
    using SQLServer.Model;
    public static class OracleToSql
    {
        #region Product Migrator
        private static void MigrateProducts()
        {
            foreach (var oracleProduct in OracleDbClient.ReadAllProducts())
            {
                SQLServer.Model.Product sqlServerProduct = new Product();

                sqlServerProduct.Product_Name   = oracleProduct.PRODUCT_NAME;
                sqlServerProduct.Price          = oracleProduct.PRICE;
                sqlServerProduct.ID             = oracleProduct.ID;
                sqlServerProduct.MeasureID      = oracleProduct.MEASURE_ID*100;
                sqlServerProduct.VendorID       = oracleProduct.VENDOR_ID*10;

                SqlServerClient.AddProduct(sqlServerProduct);
            }
        }
        #endregion

        #region Vendor migrator
        private static void MigrateVendors()
        {

            foreach (var oracleVendor in OracleDbClient.ReadAllVendors())
            {
                SQLServer.Model.Vendor sqlServerVendor = new Vendor();

                sqlServerVendor.Vendor_Name     = oracleVendor.VENDOR_NAME;
                sqlServerVendor.ID              = oracleVendor.ID*10;

                SqlServerClient.AddVendor(sqlServerVendor);
            }
        }
        #endregion

        #region Measure migrator
        static void MigrateMeasures()
        {
            //foreach (var measure in OracleDbClient.ReadAllMeasures())
            //{
            //    System.Console.WriteLine(measure.MEASURE_NAME);
            //}

            foreach (var oracleMeasure in OracleDbClient.ReadAllMeasures())
            {
                SQLServer.Model.Measure sqlServerMeasure = new Measure();
                sqlServerMeasure.Measure_Name = oracleMeasure.MEASURE_NAME;
                sqlServerMeasure.ID = oracleMeasure.ID*100;

                SqlServerClient.AddMeasure(sqlServerMeasure);
            }

        }
        #endregion

        #region Database migrator
        public static void MigrateDatabase()
        {

            MigrateVendors();
            MigrateMeasures();
            MigrateProducts();
        }
        #endregion
    }
}
