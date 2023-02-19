using src.Models.Common;
using src.Repositories.Common;

namespace src.database.Repository.IRepository
{
    public interface IRepositoryBatch<TK, TEntity> : IRepository<TK, TEntity> where TEntity : IEntity<TK>
    {
        void BatchInsert(IEnumerable<TEntity> items);

        void BatchUpsert(IEnumerable<TEntity> items);
    }
}
