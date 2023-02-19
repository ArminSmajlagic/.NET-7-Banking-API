using src.database.Contracts;
using src.database.Implementation;

namespace src.Repositories.Common
{
    public class RepositoryBase
    {
        protected readonly IConnectionFactory _connectionFactory;
        public RepositoryBase(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        protected virtual Task<IDataAccess> CreateConnectionAsync(bool transaction = false)
        {
            return Task.Run((Func<IDataAccess>)(() => new DataAccess(_connectionFactory.GetConnection(transaction), _connectionFactory.Transaction)));
        }
    }
}
