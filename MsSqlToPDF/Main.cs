namespace MsSqlToPDF
{
    using System;
    using SalesReports;
    class MainClass
    {
        static void Main(string[] args)
        {
            PdfExpenses.CreatePdfSalesReport(new DateTime(2014, 7, 20), new DateTime(2014, 7, 22));
        }
    }
}
