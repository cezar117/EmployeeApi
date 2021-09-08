using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.DataAccess.Connection
{
    public abstract class BaseRepository
    {
        private readonly IConnectionFactory connection;

        protected BaseRepository(IConnectionFactory connectionFactory)
        {
            connection = connectionFactory;
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (var cn = connection.CreateConnection)
                {
                    await cn.OpenAsync().ConfigureAwait(false);
                    return await getData(cn).ConfigureAwait(false);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (SqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
            }
        }
    }
}
