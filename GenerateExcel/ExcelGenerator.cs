using SqliteDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MysqlContext;
using System.IO;

namespace GenerateExcel
{
    public static class ExcelGenerator
    {
        public static void GenerateExcel()
        {

            IList<FinancialVendorReport> reports = GetFinancialReports();

            ExportToExcel(reports);

        }

        private static IList<FinancialVendorReport> GetFinancialReports()
        {
            IList<FinancialVendorReport> financialReports = new List<FinancialVendorReport>();

            Dictionary<string, double> productsTaxesData = GetDataFromSqlite();

            using (var mysqlDb = new MysqlContect())
            {
                var mysqlVendors = mysqlDb.Vendors.ToList();
                foreach (var vendor in mysqlVendors)
                {
                    decimal vendorExpenses = (decimal)vendor.Expenses.Sum(ex => ex.ExpenseSum);
                    decimal productSalesSumWithTaxes = 0;
                    decimal totalVendorIncomes = 0;

                    foreach (var vendorProduct in vendor.Products)
                    {
                        decimal productSaleSumWithoutTaxes = (decimal)vendorProduct.Sales.Sum(s => s.PriceSum);

                        double productTax = 1;

                        if (productsTaxesData.ContainsKey(vendorProduct.Product_Name))
                        {
                            productTax = productsTaxesData[vendorProduct.Product_Name] / 100;
                        }

                        productSalesSumWithTaxes += productSaleSumWithoutTaxes * (decimal)productTax;
                        totalVendorIncomes += productSaleSumWithoutTaxes;
                    }

                    //Console.WriteLine(string.Format("vendor: {0} incomes: {1}, expenses {2}, taxes {3}, Result {4}", vendor.Vendor_Name, totalVendorIncomes, vendorExpenses, productSalesSumWithTaxes, financialResult));

                    var report = new FinancialVendorReport(vendor.Vendor_Name, totalVendorIncomes, vendorExpenses, productSalesSumWithTaxes);

                    financialReports.Add(report);
                }
            }

            return financialReports;
        }

        private static Dictionary<string, double> GetDataFromSqlite()
        {
            Dictionary<string, double> sqlData = new Dictionary<string, double>();

            using (var sqliteDbs = new SqliteContext())
            {
                foreach (var tax in sqliteDbs.Taxes)
                {
                    sqlData.Add(tax.ProductName, tax.TaxInPercentage);
                }
            }

            return sqlData;
        }

        private static void ExportToExcel(IList<FinancialVendorReport> reports)
        {
            // Load Excel application
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            // Create empty workbook
            excel.Workbooks.Add();

            // Create Worksheet from active sheet
            Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;

            try
            {
                // Creation of header cells

                workSheet.Cells[1, "A"] = "Vendor";
                workSheet.Cells[1, "B"] = "Incomes";
                workSheet.Cells[1, "C"] = "Expenses";
                workSheet.Cells[1, "D"] = "Total Taxes";
                workSheet.Cells[1, "E"] = "Financial Result";


                int row = 2; // start row (in row 1 are header cells)
                foreach (FinancialVendorReport report in reports)
                {
                    workSheet.Cells[row, "A"] = report.VendorName;
                    workSheet.Cells[row, "B"] = report.Incomes;
                    workSheet.Cells[row, "C"] = report.Expenses;
                    workSheet.Cells[row, "D"] = report.TotalTaxes;
                    workSheet.Cells[row, "E"] = report.FinancialResult;

                    row++;
                }

                // Apply some predefined styles for data to look nicely :)
                workSheet.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);

                // Define filename
                //string fileName = string.Format(@"{0}\ExcelData.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                
                //string absolutePath = Path.Combine(Path.GetFullPath(), @".\..\..\Output\");
                string absolutePath = Path.GetFullPath(@"..\..\..\Output");
                string fileName = string.Format(@"{0}\ExcelData.xlsx", absolutePath);

                // Save this data as a file
                workSheet.SaveAs(fileName);

                // Display SUCCESS message
                Console.WriteLine("creating excel file successfull");
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a PROBLEM saving Excel file!" + e);
            }
            finally
            {
                excel.Quit();

                // Release COM objects
                if (excel != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

                if (workSheet != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);

                // Empty variables
                excel = null;
                workSheet = null;

                // Force garbage collector cleaning
                GC.Collect();
            }
        }
    }
}
