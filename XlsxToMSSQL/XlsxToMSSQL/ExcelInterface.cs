using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using Excel;
using Ionic.Zip;

namespace XlsxToMSSQL
{
    class ExcelInterface
    {
        static void LoadWorksheets(Stream stream, SqlDateTime date)
        {
            Console.WriteLine(date);
            IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(stream);
            Console.WriteLine(reader.Name);
            DataSet result = reader.AsDataSet();
            foreach (DataTable table in result.Tables)
            {                
                foreach (DataRow row in table.Rows)
                {
                    int i = 0;
                    object[] data = new object[6];
                    foreach (var item in row.ItemArray)
                    {
                        if (item != null)
	                    {
                            //data[i] = item;
                            //i++;                     
	                    }
                        Console.WriteLine(item.ToString());
                    }
                }
            }
        }

        private static void ParseWorksheetData(object[] data)
        {
            SqlString productName = data[0] as string;
            SqlInt32 productQty = SqlInt32.Parse(data[1] as string);
            SqlDecimal productPrice = SqlDecimal.Parse(data[2] as string);
            SqlDecimal totalSum = SqlDecimal.Parse(data[3] as string);
            SqlDateTime date = (SqlDateTime)data[4];
            SqlString location = data[5] as string;
        }
        static void ReadFilesFromZip(string path)
        {
            ZipFile zip = new ZipFile(path);
            Console.WriteLine(zip.Name);
            foreach (ZipEntry zipEntry in zip.Entries)
            {
                if (zipEntry.IsDirectory)
                {
                    Console.WriteLine(zipEntry.FileName);
                }
                if (!zipEntry.IsDirectory && ( zipEntry.FileName.Contains(".xlsx") || zipEntry.FileName.Contains(".xls") ))
                {
                    Console.WriteLine(zipEntry.FileName);
                    MemoryStream memStream = new MemoryStream();
                    zipEntry.Extract(memStream);
                    LoadWorksheets(memStream, DateTime.Parse(zipEntry.FileName.Substring(0, zipEntry.FileName.IndexOf('/'))));
                }
            }
        }
        void PopulateList()
        {

        }

        static void InputZip()
        {
            Console.WriteLine("Please input path to zip");
            string path = Console.ReadLine();
            ReadFilesFromZip(path);
            Console.ReadKey();
            ConsoleLoop();
        }
        static void ConsoleLoop()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("[1] - Read a Zip file");
            Console.WriteLine("[2] - Get Excel Worksheets");
            Console.WriteLine("[3] - Get directories inside the zip");
            Console.WriteLine("[X] - Exit the program");
            Console.Write("Your choice: ");
            ConsoleKeyInfo choice = Console.ReadKey();
            Console.Clear();
            switch ( choice.KeyChar )
            {
                case '1':
                    InputZip();
                    break;
                case '2':
                    InputXlsx();
                    break;
                case '3':
                    break;
                case 'x':
                    break;
            }
        }

        private static void InputXlsx()
        {
            Console.WriteLine("Please input a path to an xlsx file");
            string path = Console.ReadLine();
            Console.ReadKey();
            ConsoleLoop();
        }

        public static void Main()
        {
            ConsoleLoop();
        }
    }
}
