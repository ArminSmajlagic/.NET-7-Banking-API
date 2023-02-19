using System.Data;

namespace src.database.Contracts
{
    public interface IDataAccess: IDisposable
    {
        IDbConnection DbConnection { get; }

        IDbTransaction DbTransaction { get; }

        bool UseTransaction { get; }

        void Rollback();

        void Commit();
    }
}
