using Npgsql;
using src.database.Contracts;
using System.Data;

namespace src.database.Implementation
{
    public class ConnectionFactory : IConnectionFactory
    {
        private IConfiguration _configuration;
        private string _connectionName;
        public bool Transaction { get; private set; }
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnectionNamed(string connectionName, bool transaction = false)
        {
            _connectionName = connectionName;
            Transaction = transaction;
            return new NpgsqlConnection(connectionName);
        }
        public IDbConnection GetConnection(bool transaction = false)
        {
            _connectionName = "bankingdbTest";
            string connectionString = _configuration.GetConnectionString(_connectionName);

            if(String.IsNullOrEmpty(connectionString))
            {
                _connectionName = "bankingdb";

                connectionString = _configuration.GetConnectionString(_connectionName);
            }

            Transaction = transaction;
            return new NpgsqlConnection(connectionString);
        }
        public void Dispose()
        {
            _connectionName = "EMPTY";
            GC.SuppressFinalize(this);
        }
    }
}
