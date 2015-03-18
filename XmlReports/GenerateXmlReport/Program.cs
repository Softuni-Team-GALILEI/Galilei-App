using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GenerateXmlReport
{
    class Program
    {
        static void Main(string[] args)
        {
            SupermarketEntities db = new SupermarketEntities();

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode salesNode = doc.CreateElement("sasles");
            doc.AppendChild(salesNode);

            var vendors = (from vnd in db.Vendors select vnd).ToList();

            foreach (Vendor vendor in vendors)
            {
                XmlNode sale = doc.CreateElement("sale");
                XmlAttribute saleAttribute = doc.CreateAttribute("vendor");
                saleAttribute.Value = vendor.Vendor_Name;
                sale.Attributes.Append(saleAttribute);

                var expenses = (from exp in db.Expenses where exp.VendorID == vendor.ID select exp).ToList();

                foreach (Expens expense in expenses)
                {
                    XmlNode exp = doc.CreateElement("summary");
                    XmlAttribute date = doc.CreateAttribute("date");
                    XmlAttribute totalSum = doc.CreateAttribute("total-sum");

                    DateTime expDateTime = (DateTime)expense.Time;

                    date.Value = expDateTime.ToString("dd-MMM-yyyy");
                    totalSum.Value = expense.ExpenseSum.ToString();

                    exp.Attributes.Append(date);
                    exp.Attributes.Append(totalSum);

                    sale.AppendChild(exp);
                }

                salesNode.AppendChild(sale);
            }

            doc.Save("expenses-report.xml");

            Console.WriteLine("XML report created.");
        }
    }
}
