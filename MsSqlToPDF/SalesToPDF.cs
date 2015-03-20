namespace MsSqlToPDF
{
    using System;
    using System.IO;
    using System.Linq;
    using SQLServer.Model;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System.Collections.Generic;

    public static class PdfExpenses
    {
        public static void CreatePdfSalesReport(string path, DateTime startDate, DateTime endDate, List<Supermarket> supermarkets, List<Sale> sales, List<Product> products)
        {  
        // TODO: change save dir, add table style
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document(PageSize.A4, 36, 36, 36, 36);
            PdfWriter.GetInstance(doc, fs);
            doc.Open();
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            PdfPCell cellHeader = new PdfPCell(new Phrase("Aggregated Sales Report"));
            cellHeader.Colspan = 5;
            cellHeader.HorizontalAlignment = 1;
            table.AddCell(cellHeader);

            int differenceInDays = (int)(endDate - startDate).TotalDays;
            var grandSum = 0.00;
            for (int i = 0; i <= differenceInDays; i++)
            {
                var currentDayDate = startDate.AddDays(i).Date;
                var daySum = 0.00;
                PdfPCell date = new PdfPCell(new Phrase(String.Format("Date: {0}", startDate.AddDays(i).ToString("d"))));
                date.Colspan = 5;
                date.HorizontalAlignment = 0;
                var grayColor = new BaseColor(175, 175, 175);

                var cellProductHeader = new PdfPCell(new Phrase("Product"));
                var cellQuantityHeader = new PdfPCell(new Phrase("Quantity"));
                var cellUnitPriceHeader = new PdfPCell(new Phrase("Unit Price"));
                var cellLocationHeader = new PdfPCell(new Phrase("Location"));
                var cellSumHeader = new PdfPCell(new Phrase("Sum"));

                cellProductHeader.BackgroundColor = grayColor;
                cellQuantityHeader.BackgroundColor = grayColor;
                cellUnitPriceHeader.BackgroundColor = grayColor;
                cellLocationHeader.BackgroundColor = grayColor;
                cellSumHeader.BackgroundColor = grayColor;

                table.AddCell(date);
                table.AddCell(cellProductHeader);
                table.AddCell(cellQuantityHeader);
                table.AddCell(cellUnitPriceHeader);
                table.AddCell(cellLocationHeader);
                table.AddCell(cellSumHeader);

                // TODO: get units
                var daySales = from s in sales
                                join p in products on s.ProductID equals p.ID
                                join l in supermarkets on s.SupermarketID equals l.ID
                                where s.Date == currentDayDate
                                select new
                                {
                                    productName = p.Product_Name,
                                    quantity = s.Quantity,
                                    unitPrice = p.Price,
                                    location = l.Name,
                                    sum = s.PriceSum
                                };


                foreach (var daySale in daySales)
                {
                    // TODO: format numbers
                    var productCell = new PdfPCell(new Phrase(daySale.productName));
                    var quantityCell = new PdfPCell(new Phrase(daySale.quantity.ToString()));
                    quantityCell.HorizontalAlignment = 1;
                    var unitPriceCell = new PdfPCell(new Phrase(daySale.unitPrice.ToString()));
                    unitPriceCell.HorizontalAlignment = 1;
                    var locationCell = new PdfPCell(new Phrase(daySale.location));
                    var sumCell = new PdfPCell(new Phrase(daySale.sum.ToString()));
                    sumCell.HorizontalAlignment = 2;

                    table.AddCell(productCell);
                    table.AddCell(quantityCell);
                    table.AddCell(unitPriceCell);
                    table.AddCell(locationCell);
                    table.AddCell(sumCell);

                    daySum += (double)daySale.sum;
                }

                var daySumCell = new PdfPCell(new Phrase(String.Format("Total sum for {0}:",
                    currentDayDate.ToString("d"))));
                daySumCell.Colspan = 4;
                daySumCell.HorizontalAlignment = 2;
                var sumCellSum = new PdfPCell(new Phrase(String.Format(daySum.ToString())));
                sumCellSum.HorizontalAlignment = 2;

                table.AddCell(daySumCell);
                table.AddCell(sumCellSum);
                grandSum += daySum;
                

                var grandTotalTextCell = new PdfPCell(new Phrase("Grand total:"));
                grandTotalTextCell.BackgroundColor = new BaseColor(173, 224, 255);
                grandTotalTextCell.HorizontalAlignment = 2;
                var grandTotalCell = new PdfPCell(new Phrase(String.Format(grandSum.ToString())));
                grandTotalCell.BackgroundColor = new BaseColor(173, 224, 255);
                grandTotalCell.HorizontalAlignment = 2;

                table.AddCell(grandTotalTextCell);
                table.AddCell(grandTotalCell);
            }
            doc.Add(table);
            doc.Close();
        }
    }
}
