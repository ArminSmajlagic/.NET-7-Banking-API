using src.Repositories.Common;

namespace src.Repositories.Account
{
    public interface IAccountRepository : IRepository<int, Models.Account.Account>
    {
    }
}
