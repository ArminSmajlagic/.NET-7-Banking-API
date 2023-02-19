using System.Data;

namespace src.database.Contracts
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection GetConnectionNamed(string connectionName, bool transaction = false);
        IDbConnection GetConnection(bool transaction = false);
        bool Transaction { get; }
    }
}
