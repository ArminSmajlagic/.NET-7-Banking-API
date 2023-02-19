using Dapper;
using src.database.Contracts;
using src.Repositories.Account;
using src.Repositories.Common;
using src.Repositories.Transfers;

namespace src.Repositories.transfer
{
    public class TransferRepository : RepositoryBase, ITransferRepository
    {
        private readonly ILogger<TransferRepository> logger;

        public TransferRepository(IConnectionFactory connectionFactory, ILogger<TransferRepository> logger) : base(connectionFactory)
        {
            this.logger = logger;
        }

        public async Task<bool> Transfer(int fromAccountId, int toAccountId, double value)
        {
            string transactionQuery = @"UPDATE account SET balance = balance - @ammount, updated = NOW() WHERE id = @accountFromId;
                                        UPDATE account SET balance = balance + @ammount, updated = NOW() WHERE id = @accountToId;
                                        INSERT INTO transfer (accountToId, accountFromId, ammount, created, deleted) VALUES (@accountToId, @accountFromId, @ammount, NOW(), false) RETURNING id;";
           
            using (IDataAccess dataAccess = await CreateConnectionAsync(transaction:true))
            {
                try
                {
                    var result = await dataAccess.DbConnection.ExecuteAsync(
                        sql: transactionQuery,
                        param: new { accountFromId = fromAccountId, accountToId = toAccountId, ammount = value },
                        transaction:dataAccess.DbTransaction);

                    dataAccess.Commit();

                    return result > 0;
                }
                catch (Exception ex)
                {
                    dataAccess.Rollback();

                    logger.LogInformation(ex.Message);

                    throw new Exception("It seems that your transaction did not go through.");
                }

            }

        }
    }
}
