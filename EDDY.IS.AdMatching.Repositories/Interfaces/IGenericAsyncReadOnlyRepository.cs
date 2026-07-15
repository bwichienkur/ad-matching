using System.Linq.Expressions;


namespace EDDY.IS.AdMatching.Repositories.Interfaces
{
    /// <summary>
    /// IGenericAsyncReadOnlyRepository interfaced is used to declare common methods across all Repositories and Entities. It is a async template
    /// </summary>
    /// <typeparam name="TEntity">type parameter</typeparam>
    public interface IGenericAsyncReadOnlyRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetByIDAsync(int id);
        /*
        Task InsertAsync(TEntity entity);
        Task DeleteAsync(object id);
        Task DeleteAsync(TEntity entityToDelete);
        Task UpdateAsync(TEntity entityToUpdate);
        Task SaveAsync();*/
    }
}
