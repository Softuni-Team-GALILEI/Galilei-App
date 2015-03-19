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
        public static void PrintReport(IEnumerable<Vendor> vendors, IEnumerable<Sale> expenses, string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode salesNode = doc.CreateElement("summary");
            doc.AppendChild(salesNode);

            foreach (Vendor vendor in vendors)
            {
                XmlNode saleNode = doc.CreateElement("vendor");
                XmlAttribute saleAttribute = doc.CreateAttribute("vendor");
                saleAttribute.Value = vendor.Vendor_Name;
                saleNode.Attributes.Append(saleAttribute);

                foreach (Sale sale in expenses.Where(s => s.Product.VendorID == vendor.ID))
                {
                    XmlNode exp = doc.CreateElement("sale");
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