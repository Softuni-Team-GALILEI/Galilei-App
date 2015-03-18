using System;
using System.Data.Entity;
using System.Linq;
using System.Data.SQLite.EF6;
using System.Data.SqlClient;
using System.Data.SQLite.Linq;

namespace SqliteDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SqliteContext())
            {
                var taxes = db.Taxes;
                foreach (var item in taxes)
                {
                    Console.WriteLine(item);
                }
            }
        }

    }
}