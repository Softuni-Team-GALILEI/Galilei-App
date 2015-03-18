using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LoadDataFromXml
{
    class Program
    {
        static void Main()
        {
            Console.Write("XML file path: ");
            string inputFile = Console.ReadLine();
            // C:\Users\user\Desktop\Database-Apps-Teamwork-Project\Sample-Vendor-Expenses.xml

            SupermarketEntities db = new SupermarketEntities();

            var doc = new XmlDocument();
            doc.Load(inputFile);

            XmlNodeList vendors = doc.SelectNodes("expenses-by-month/vendor");

            foreach (XmlNode vendor in vendors)
            {
                var vendorName = vendor.Attributes["name"].Value;
                Console.WriteLine(" --- {0} --- ", vendorName);

                Vendor dbVendor = (from vnd in db.Vendors where vnd.Vendor_Name == vendorName select vnd).Single();

                foreach (XmlNode expense in vendor)
                {
                    var expenseDate = expense.Attributes["month"].Value;
                    var amount = expense.InnerText;

                    Console.WriteLine("\t {0} - {1}", expenseDate, amount);

                    var expenses = db.Set<Expens>();
                    expenses.Add(new Expens
                    {
                        VendorID = dbVendor.ID,
                        Time = DateTime.Parse(expenseDate),
                        ExpenseSum = Decimal.Parse(amount)
                    });
                }
            }

            db.SaveChanges();
            Console.WriteLine("XML file imported to the database.");
        }
    }
}
