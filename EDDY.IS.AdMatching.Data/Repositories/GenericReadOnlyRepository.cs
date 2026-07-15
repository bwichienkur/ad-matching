using EDDY.IS.AdMatching.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.Json;
using EDDY.IS.Common.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace EDDY.IS.AdMatching.Data.Repositories
{
    /// <summary>
    /// GenericReadOnlyRepository interfaced is used to declare common methods across all Repositories and Entities. It is a  template
    /// </summary>
    /// <typeparam name="TEntity">type parameter</typeparam>
    public class GenericReadOnlyRepository<TEntity> : IGenericReadOnlyRepository<TEntity> where TEntity : class
    {
        protected DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericReadOnlyRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Get method
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="orderBy">order by</param>
        /// <param name="includeProperties">include child properties to be filled with data</param>
        /// <returns>return collection of entities of specific type defined by type parameter</returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] {','},
                         StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// GetByID method
        /// </summary>
        /// <param name="Id">id</param>
        /// <returns>returns entity of specific type defined by type parameter by id</returns>
        public virtual TEntity GetByID(object Id)
        {
            return dbSet.Find(Id);
        }

        /// <summary>
        /// GetAll gets all entities
        /// </summary>
        /// <returns>returns collection of entities of specific type defined by type parameter</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate)
        {
            return dbSet.Where(predicate) .ToList();
        }


        /// <summary>
        /// GetPartial gets an entity with the specified fields. The fields not specified will be null
        /// </summary>
        /// <param name="selector">An entity with the specification of the fields to get from DB </param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetPartial(Func<TEntity, object> selector)
        {
            var t = context.Set<TEntity>().Select(selector).ToList();

            return t.OfType<TEntity>();
        }
        
    }
}