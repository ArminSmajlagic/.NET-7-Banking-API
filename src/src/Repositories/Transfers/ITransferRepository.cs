using src.Repositories.Common;

namespace src.Repositories.Transfers
{
    public interface ITransferRepository
    {
        Task<bool> Transfer(int fromAccount, int toAccount, double ammount);
    }
}
