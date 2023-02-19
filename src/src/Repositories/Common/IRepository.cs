using src.Models.Common;
using System.Linq.Expressions;

namespace src.Repositories.Common
{
    public interface IRepository<in TKey, TEntity> where TEntity : IEntity<TKey>
    {
        Task<IEnumerable<TEntity>> FindAll();
        Task<TEntity> Find(TKey id);
        Task<int> Insert(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<int> Upsert(TEntity entity);
        Task<bool> Delete(TKey id);
        Task<int> Count();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, int? limit = null, int? offset = null, bool descending = false);
    }
}
