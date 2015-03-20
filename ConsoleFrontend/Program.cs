using JsonAndMongo;

namespace ConsoleFrontend
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GenerateXmlReport;
    using LoadDataFromXml;
    using SQLServer.Client;
    using SQLServer.Model;

    class Program
    {
        private static string inputExcel            = "Input/excel.zip";
        private static string inputReport           = "Input/report.xml";
        private static string outputReport          = "Output/report.xml";
        private static string outputPdf             = "Output/report.pdf";
        private static string outputJson            = "Output/";
        private static DateTime sampleDataStartDate = new DateTime(2014, 7, 20);
        private static DateTime sampleDataEndDate   = new DateTime(2014, 7, 22);
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("[1] - Transfer Oracle DB to MSSQL");
            Console.WriteLine("[2] - Read Excel Sales Reports");
            Console.WriteLine("[3] - Generate PDF Sales Report");
            Console.WriteLine("[4] - Generate Xml Report");
            Console.WriteLine("[5] - Generate Product Json Files ");
            Console.WriteLine("[6] - Read Expenses from Xml");
            Console.WriteLine("[X] - Quit");
            switch (Console.ReadKey().KeyChar)
            {
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
                case 'X':
                    break;
                default:
                    Menu();
                    break;
            }
        }

        static void Main(string[] args)
        {
            Menu();
        }

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
            Menu();
        }
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
        public static void GenerateXmlReport(string path)
        {
            try
            {
                XmlSalesReportGenerator.PrintReport(SqlServerClient.GetVendors().ToList(),
                            SqlServerClient.GetSales().ToList(),
                            path);
                Console.WriteLine("Done! Press Any key to return to main menu.");
            }
            catch (Exception)
            {
                Console.WriteLine("Fuck this");
                throw;
            }
            Console.ReadKey();
        }

        public static void SaveExpensesToDatabase(string path)
        {
            try
            {
                List<Expens> expenses = XmlLoader.LoadExpensesFromXml("Input/reports.xml");
                expenses.ForEach( e => SqlServerClient.AddExpense(e));
                Console.WriteLine("\nDone! Press Any key to return to main menu.");
            }
            catch (Exception)
            {
                Console.WriteLine("Fuck this");
                throw;
            }
            Console.ReadKey();
        }
    }
}
