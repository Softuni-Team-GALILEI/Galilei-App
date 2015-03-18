using System.Threading;

namespace XlsxToMSSQL
{
    using Excel;
    using System;
    using Ionic.Zip;
    using System.IO;
    using System.Linq;
    using System.Data;
    using SQLServer.Model;
    using SQLServer.Client;
    using System.Collections.Generic;

    class ExcelInterface
    {
        #region Private Methods
        private static Sale GenerateSale(DateTime date,string productName, string supermarketName, decimal priceUnit, int quantity)
        {
            Sale newSale            = new Sale();

            newSale.SupermarketID   = SqlServerClient.GetSupermarkets().First(s => s.Name == supermarketName).ID;
            newSale.ProductID       = SqlServerClient.GetProducts().First(p => p.Product_Name == productName).ID;
            newSale.PriceUnit       = priceUnit;
            newSale.Quantity        = quantity;
            newSale.PriceSum        = priceUnit * quantity;
            newSale.Date            = date;

            return newSale;
        }

        private static Tuple<string, int, decimal> ParseObjectArray(object[] itemArray)
        {
            return new Tuple<string, int, decimal>(
                (itemArray[0] as string),
                (Convert.ToInt32(itemArray[1])),
                Convert.ToDecimal(itemArray[2])
            );
        }

        #endregion

        #region Excel Logic
        public static IEnumerable<Sale> GetSalesFromWorksheet(Stream stream, DateTime date)
        {
            IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(stream);
            DataSet result          = reader.AsDataSet();
            string[] identifiers    = {"Product", "Quantity", "Unit Price", "Sum"};
            bool dataValid          = false;
            string supermarket      = "";
            foreach (DataTable table in result.Tables)
            {
                int y               = 0;
                foreach (DataRow row in table.Rows)
                {
                    int x               = 0;
                    object[] dataArray  = new object[4];
                    bool rowValid       = false;
                    
                    foreach (var item in row.ItemArray)
                    {
                        if (!dataValid)
                        {
                            var startCheck = item as string;
                            if (startCheck != null && startCheck.Contains("Supermarket"))
                            {
                                supermarket = startCheck;
                                dataValid = true;
                                x = 0;
                                y = 0; // Sets start pos of valid data
                                break;
                            }
                        }
                        else
                            if (y > 1 && x > 0)
                            {
                                if ((item) == null ||  item is DBNull)
                                {
                                    dataValid = false;
                                    rowValid = false;
                                    break;
                                }

                                rowValid = true;
                                dataArray[x-1] = item;
                            }
                        x++;
                    }

                    if (rowValid)
                    {
                        Tuple<string, int, decimal> parsedObject = ParseObjectArray(dataArray);
                        yield return GenerateSale(date, parsedObject.Item1, supermarket, parsedObject.Item3, parsedObject.Item2);
                    }

                    y++;
                }
            }
            yield break;
        }
        #endregion

        #region Zip Logic
        public static IEnumerable<Sale> ReadSalesFromZip(string path)
        {
            ZipFile zip = new ZipFile(path);
            IEnumerable<Sale> sales = null;

            foreach (ZipEntry zipEntry in zip.Entries)
            {
                if (!zipEntry.IsDirectory && ( zipEntry.FileName.Contains(".xlsx") || zipEntry.FileName.Contains(".xls") ))
                {
                    MemoryStream memStream = new MemoryStream();
                    zipEntry.Extract(memStream);

                    if (sales == null)
                    {
                        sales = GetSalesFromWorksheet(memStream,
                            DateTime.Parse(zipEntry.FileName.Substring(0, zipEntry.FileName.IndexOf('/'))));
                    }
                    else
                    {
                        sales = sales.Union(GetSalesFromWorksheet(memStream,
                            DateTime.Parse(zipEntry.FileName.Substring(0, zipEntry.FileName.IndexOf('/')))));
                    }
                }
            }
            return sales.ToList();
        }
        #endregion
    }

    class TestingClass
    {
        static void InputZip()
        {
            Console.WriteLine("Please input path to zip");
            string path = Console.ReadLine();
            IEnumerable<Sale> sales =ExcelInterface.ReadSalesFromZip(path);
            PrintSales(sales);


            Console.WriteLine("Do you want to import these entries to sql server?(Y/N)");
            ConsoleKeyInfo choice = Console.ReadKey();
            switch (choice.Key)
            {
                case ConsoleKey.Y:
                    try
                    {
                        ImportSalesInSQLServer(sales);
                        Console.WriteLine("Importing...");
                    }
                    catch (Exception e)
                    {
                        Error(e);
                        throw;
                    }
                    break;
                default:
                    break;
            }
            ReturnToMenu();
        }

        static void PrintSales(IEnumerable<Sale> sales)
        {
            foreach (Sale sale in sales)
            {
                Console.WriteLine("{0} \t\t\t {1} \t {2} \t {3}", SqlServerClient.GetProductsById(sale.ProductID).Product_Name, sale.PriceUnit, sale.PriceSum, SqlServerClient.GetSupermarketsById((int)sale.SupermarketID).Name);
            }
        }
        static void ReturnToMenu()
        {
            Console.WriteLine("Press the any key to return to menu!");
            Console.ReadKey();
            ConsoleLoop();
        }
        static void Done()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done! Press any key to return to menu!");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Gray;
            ConsoleLoop();
        }

        static void Error(Exception e)
        {
            Console.WriteLine("There was an error {0}", e.Message);
        }
        static void ImportSalesInSQLServer(IEnumerable<Sale> sales)
        {
            foreach (var sale in sales)
            {
                SqlServerClient.AddSale(sale);
            }
        }
        static void ConsoleLoop()
        {
            Console.Clear();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("[1] - Read Sales from a Zip file");
            Console.WriteLine("[X] - Exit the program");
            Console.Write("Your choice: ");
            ConsoleKeyInfo choice = Console.ReadKey();
            Console.Clear();
            switch (choice.KeyChar)
            {
                case '1':
                    InputZip();
                    break;
                case 'x':
                    break;
            }
        }

        public static void Main()
        {
            ConsoleLoop();
        }
    }
}
