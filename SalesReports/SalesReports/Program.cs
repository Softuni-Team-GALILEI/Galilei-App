namespace SalesReports
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            PdfExpenses.CreatePdfSalesReport(new DateTime(2014, 7, 20), new DateTime(2014, 7, 22));
        }
    }
}
