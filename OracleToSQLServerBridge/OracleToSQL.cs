using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OracleDb.Client;

namespace OracleToSQLServerBridge
{
    class OracleToSQL
    {
        
        static void Main(string[] args)
        {
            
        }

        static ICollection<OracleDb.Model.Product> GetOracleEntries()
        {
            OracleDbClient.InitDb();
            return OracleDbClient.ReadAllProducts();            
        }
        static void MigrateEntriesToSQLServer()
        {

        }
    }
}
