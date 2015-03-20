namespace GenerateXmlReport
{
    using System;
    using System.Xml;
    using System.Linq;
    using SQLServer.Model;
    using SQLServer.Client;
    using System.Collections.Generic;
    public static class XmlSalesReportGenerator
    {
        #region Report Generator
        public static void PrintReport(IEnumerable<Vendor> vendors, IEnumerable<Sale> sales, string path)
        {
            Console.Write("Enter start date:");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Enter end date: ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine();

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode salesNode = doc.CreateElement("sales");
            doc.AppendChild(salesNode);

            foreach (Vendor vendor in vendors)
            {
                XmlNode saleNode = doc.CreateElement("sale");
                XmlAttribute saleAttribute = doc.CreateAttribute("vendor");
                saleAttribute.Value = vendor.Vendor_Name;
                saleNode.Attributes.Append(saleAttribute);

                foreach (Sale sale in sales.Where(s => s.Date >= startDate).Where(s => s.Date <= endDate).Where(s => s.Product.VendorID == vendor.ID))
                {
                    XmlNode exp = doc.CreateElement("summary");
                    XmlAttribute date = doc.CreateAttribute("date");

                    DateTime expDateTime = (DateTime)sale.Date;

                    date.Value = expDateTime.ToString("dd-MMM-yyyy");
                    exp.InnerText = sale.PriceSum.ToString();

                    exp.Attributes.Append(date);
                    saleNode.AppendChild(exp);
                }

                salesNode.AppendChild(saleNode);
            }

            doc.Save(path);
            Console.WriteLine("XML report created.");
        }
        #endregion
    }
}