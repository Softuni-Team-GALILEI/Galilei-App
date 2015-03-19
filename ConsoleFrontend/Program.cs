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


        private static string inputReport = "Input/report.xml";
        private static string outputReport = "Output/report.xml";

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("[1] - Generate Xml Report");
            Console.WriteLine("[2] - Read Xml Report");
            Console.WriteLine("[3] - Something else");
            Console.WriteLine("[X] - Quit");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    GenerateXMLReport(outputReport);
                    break;
                case '2':
                    SaveExpensesToDatabase(inputReport);
                    break;
                case '3':
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
        public static void GenerateXMLReport(string path)
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
