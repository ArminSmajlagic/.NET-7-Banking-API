using Dapper;
using src.database.Contracts;
using src.Repositories.Common;
using System.Linq.Expressions;
using System.Text;

namespace src.Repositories.Account
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        public AccountRepository(IConnectionFactory connectionFactory) : base(connectionFactory){}
        public async Task<int> Count()
        {
            var query = "SELECT COUNT(1) FROM account";

            using (IDataAccess dataAccess = await CreateConnectionAsync())
            {
                return await dataAccess.DbConnection.QuerySingleOrDefaultAsync<int>(query);
            }
        }
        public async Task<IEnumerable<Models.Account.Account>> FindAll()
        {
            var query = "SELECT * FROM account WHERE deleted <> true";
            using (IDataAccess dataAccess = await CreateConnectionAsync())
            {
                return await dataAccess.DbConnection.QueryAsync<Models.Account.Account>(query);
            }
        }
        public async Task<Models.Account.Account> Find(int id)
        {
            using (IDataAccess dataAccess = await CreateConnectionAsync())
            {
                var query = "SELECT * FROM account WHERE id = @id AND deleted <> true";
                return await dataAccess.DbConnection.QuerySingleOrDefaultAsync<Models.Account.Account>(query, new { id });
            }
        }
        public async Task<bool> Delete(int id)
        {
            string query = "UPDATE account SET deleted=true WHERE id = @id";

            using (IDataAccess dataAccess = await CreateConnectionAsync())
            {
                return await dataAccess.DbConnection.ExecuteAsync(query, new { id }) > 0;
            }
        }
        public async Task<int> Insert(Models.Account.Account entity)
        {
            string query = "INSERT INTO account (username, password, created, deleted) VALUES (@username, @password, NOW(), false) RETURNING id";

            using (IDataAccess dataAccess = await CreateConnectionAsync())
            {
                var result = await dataAccess.DbConnection.QuerySingleAsync(query, entity);
                return result.id;
            }
        }
        public async Task<bool> Update(Models.Account.Account entity)
        {
            string query = "UPDATE account SET username = @username, password = @password, updated = NOW() WHERE id = @id";

            using (IDataAccess dataAccess = await CreateConnectionAsync())
            {
                return await dataAccess.DbConnection.ExecuteAsync(query, entity) > 0;
            }
        }
        public async Task<int> Upsert(Models.Account.Account entity)
        {
            throw new NotImplementedException();
            string query = "";

            using (IDataAccess dataAccess = await CreateConnectionAsync())
            {
                entity.id = await dataAccess.DbConnection.ExecuteAsync(query, entity);
                return entity.id;
            }
        }
        public async Task<IEnumerable<Models.Account.Account>> Find(Expression<Func<Models.Account.Account, bool>> predicate, int? limit = null, int? offset = null, bool descending = false)
        {
            // TODO lacks WHERE predicates & OrderBys
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT * FROM account WHERE deleted <> true");

            string orderBy = descending ? "ASC" : "DESC";

            stringBuilder.Append($" ORDER BY id {orderBy}");

            if (limit.HasValue)
            {
                stringBuilder.Append($" LIMIT {limit}");
            }
            if (offset.HasValue)
            {
                stringBuilder.Append($" OFFSET {offset}");
            }
            using (IDataAccess dataAccess = await CreateConnectionAsync())
            {
                var result = await dataAccess.DbConnection.QueryAsync<Models.Account.Account>(stringBuilder.ToString());

                return result.Any() ? result : null;
            }
        }
    }
}
