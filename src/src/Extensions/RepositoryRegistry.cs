using src.database.Contracts;
using src.database.Implementation;
using src.Repositories.Account;
using src.Repositories.transfer;
using src.Repositories.Transfers;

namespace src.Extensions
{
    public static class RepositoryRegistry
    {
        public static IServiceCollection RegisterRepositoryDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<ITransferRepository, TransferRepository>();

            return services;
        }
    }
}
