using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.DataAccess.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string connectionString;
        public ConnectionFactory(string connection)
        {
            connectionString = connection;
        }
        public SqlConnection CreateConnection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }
    }
}
