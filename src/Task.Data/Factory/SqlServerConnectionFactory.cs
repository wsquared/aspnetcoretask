using System.Data.Common;
using System.Data.SqlClient;
using Task.Data.Contracts.Factory;

namespace Task.Data.Factory
{
    public class SqlServerConnectionFactory : ISqlServerConnectionFactory
    {
        public DbConnection Create(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}