using System.Linq.Expressions;

namespace EDDY.IS.AdMatching.Repositories.Interfaces
{
    /// <summary>
    /// IGenericReadOnlyRepository interface is used to declare common methods to be implemented and shared across multiple Repositories and Entities. It is a template.
    /// </summary>
    /// <typeparam name="TEntity">type parameter</typeparam>
    public interface IGenericReadOnlyRepository<TEntity> where TEntity: class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetByID(object Id);

    }
}
