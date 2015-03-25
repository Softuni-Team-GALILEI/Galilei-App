namespace ConsoleFrontend
{
    using System;
    using System.Linq;
    using JsonAndMongo;
    using SQLServer.Model;
    using LoadDataFromXml;
    using SQLServer.Client;
    using GenerateXmlReport;
    using System.Collections.Generic;
    using MysqlContext;
    using MigrateToMysql;
    using GenerateExcel;

    class ConsoleFrontend
    {
        static void Main()
        {

            Menu();
        }


        #region Hard-Coded Input & Output

        private static string inputExcel            = "../../../Input/excel.zip";
        private static string inputReport           = "../../../Input/report.xml";
        private static string outputReport          = "../../../Output/report.xml";
        private static string outputPdf             = "../../../Output/report.pdf";
        private static string outputJson            = "../../../Output/";
        private static DateTime sampleDataStartDate = new DateTime(2014, 7, 20);
        private static DateTime sampleDataEndDate   = new DateTime(2014, 7, 22);

        #endregion


        #region CLI menu
        static void Menu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("[0] - insert data into oracle");
            Console.WriteLine("[1] - Transfer Oracle DB to MSSQL");
            Console.WriteLine("[2] - Read Excel Sales Reports");
            Console.WriteLine("[3] - Generate PDF Sales Report");
            Console.WriteLine("[4] - Generate Xml Report");
            Console.WriteLine("[5] - Generate Product Json Files ");
            Console.WriteLine("[6] - Read Expenses from Xml");
            Console.WriteLine("[7] - Migrate MSSQL to Mysql");
            Console.WriteLine("[8] - Create Financial Report in excel file");
            Console.WriteLine("[X] - Quit");
            switch (Console.ReadKey().KeyChar)
            {
                case '0':
                    OracleDb.Client.OracleDbClient.AddDataToOracle();
                    break;
                case '1':
                    OracleToSQLServerBridge.OracleToSql.MigrateDatabase();
                    break;
                case '2':
                    InputFromExcelZip(inputExcel);
                    break;
                case '3':
                    GeneratePdfReport(outputPdf, sampleDataStartDate, sampleDataEndDate);
                    break;
                case '4':
                    GenerateXmlReport(outputReport);
                    break;
                case '5':
                    ExportToJson(outputJson);
                    break;
                case '6':
                    SaveExpensesToDatabase(inputReport);
                    break;
                case '8':
                    ExcelGenerator.GenerateExcel();
                    break;
                case '7':
                    Migrator.MigrateToMysql();
                    break;
                case 'X':
                    break;
                default:

                    Menu();
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone! Press Any Key to continue . . .");
            Console.ReadKey();
            Menu();

        }
        #endregion
        

        #region Problem 2 - Import from Excel in Zip Controller
        static void InputFromExcelZip(string path)
        {
            
            IEnumerable<Sale> sales = XlsxToMSSQL.ExcelInterface.ReadSalesFromZip(path);
            
            foreach (Sale sale in sales)
            {
                    Console.WriteLine("{0} \t\t\t {1} \t {2} \t {3}",
                    SqlServerClient.GetProductsById(sale.ProductID).Product_Name, sale.PriceUnit, sale.PriceSum,
                    SqlServerClient.GetSupermarketsById((int)sale.SupermarketID).Name);
            }

           Console.WriteLine("Do you want to import these entries to sql server?(Y/N)");
           switch (Console.ReadKey().KeyChar)
            {
                case 'Y':
                    try
                    {
                        foreach (Sale sale in sales)
                        {
                            SqlServerClient.AddSale(sale);
                        }
                        Console.WriteLine("Importing...");
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Problem 3 - Generate PDF Report Controller
        public static void GeneratePdfReport(string path, DateTime start, DateTime end)
        {
            MsSqlToPDF.PdfExpenses.CreatePdfSalesReport(path,
                                                        start,
                                                        end, 
                                                        SqlServerClient.GetSupermarkets().ToList(),
                                                        SqlServerClient.GetSales().ToList(),
                                                        SqlServerClient.GetProducts().ToList()
                                                    );
        }
        #endregion

        #region Problem 4 - Generate XML Report Controller
        public static void GenerateXmlReport(string path)
        {
            try
            {
                XmlSalesReportGenerator.PrintReport(SqlServerClient.GetVendors().ToList(),
                            SqlServerClient.GetSales().ToList(),
                            path);
            }
            catch (Exception)
            {
                Console.WriteLine("Fuck this");
                throw;
            }
        }
        #endregion

        #region Problem 5 - Export to Json & Mongo Controller
        static void ExportToJson(string path)
        {
            try
            {
                ExportToMongo.ExportReportsToMongoAndJson(path, SqlServerClient.GetSales().ToList(), SqlServerClient.GetProducts().ToList());
                Console.WriteLine("Success");
            }
            catch (Exception)
            {
                Console.WriteLine("Fuck!");
                throw;
            }
        }
        #endregion

        #region Problem 6 - Import XML Controller
        public static void SaveExpensesToDatabase(string path)
        {
            try
            {
                List<Expens> expenses = XmlLoader.LoadExpensesFromXml(path);
                expenses.ForEach( e => SqlServerClient.AddExpense(e));
            }
            catch (Exception e)
            {
                Console.WriteLine("Fuck this");
                throw e;
            }
        }
        #endregion
    }
}
