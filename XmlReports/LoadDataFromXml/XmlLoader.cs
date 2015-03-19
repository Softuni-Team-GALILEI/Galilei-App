
namespace LoadDataFromXml
{
    using System;
    using System.Xml;
    using System.Linq;
    using SQLServer.Model;
    using SQLServer.Client;
    using System.Collections.Generic;
    public class XmlLoader
    {
        public static List<Expens> LoadExpensesFromXml(string path)
        {
            ICollection<Expens> expenses    = new List<Expens>();
            IEnumerable<Vendor> Vendors     = SqlServerClient.GetVendors();
            var doc                         = new XmlDocument();

            doc.Load(path);
            XmlNodeList vendorNodes = doc.SelectNodes("expenses-by-month/vendor");

            foreach (XmlNode vendor in vendorNodes)
            {
                var vendorName = vendor.Attributes["name"].Value;
                Console.WriteLine(" --- {0} --- ", vendorName);

                Vendor dbVendor = (from vnd in Vendors where vnd.Vendor_Name == vendorName select vnd).Single();

                foreach (XmlNode expense in vendor)
                {
                    var expenseDate = expense.Attributes["month"].Value;
                    var amount = expense.InnerText;

                    Console.WriteLine("\t {0} - {1}", expenseDate, amount);

                    expenses.Add(new Expens
                    {
                        VendorID = dbVendor.ID,
                        Time = DateTime.Parse(expenseDate),
                        ExpenseSum = Decimal.Parse(amount)
                    });
                }
            }

            return expenses.ToList();
        }
    }
}
