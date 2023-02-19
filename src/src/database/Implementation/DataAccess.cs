using src.database.Contracts;
using System.Data;

namespace src.database.Implementation
{
    public class DataAccess : IDataAccess
    {
        public bool IsDisposed { get; private set; } = false;
        public bool UseTransaction { get; private set; }
        public ConnectionState ConnectonState { get; private set; }
        public IDbConnection? DbConnection { get; private set; }
        public IDbTransaction? DbTransaction { get; private set; }

        public DataAccess(IDbConnection connection, bool useTransaction)
        {
            DbConnection = connection;
            UseTransaction = useTransaction;

            if (ConnectonState != ConnectionState.Open)
            {
                DbConnection.Open();
                ConnectonState = DbConnection.State;
            }
            if (useTransaction)
            {
                DbTransaction = DbConnection.BeginTransaction();
            }
        }
    
        public void Commit()
        {
            if (UseTransaction && ConnectonState != ConnectionState.Closed && DbTransaction != null)
            {
                DbTransaction.Commit();
                DbTransaction.Dispose();
                DbTransaction = null;
                UseTransaction = false;
            }
        }
        public void Rollback()
        {

            if (UseTransaction && ConnectonState != ConnectionState.Closed && DbTransaction != null)
            {
                DbTransaction.Rollback();
                DbTransaction.Dispose();
                DbTransaction = null;
                UseTransaction = false;
            }
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                DbConnection.Close();
                DbConnection.Dispose();
                IsDisposed = true;
                GC.Collect();
            }
        }

        ~DataAccess() {
            DbConnection = null;
            DbTransaction = null;
        }
    }
}
