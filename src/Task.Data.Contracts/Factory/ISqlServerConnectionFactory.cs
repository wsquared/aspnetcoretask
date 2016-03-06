using System.Data.Common;

namespace Task.Data.Contracts.Factory
{
    public interface ISqlServerConnectionFactory
    {
        DbConnection Create(string connectionString);
    }
}
