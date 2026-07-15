using EDDY.IS.AdMatching.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EDDY.IS.AdMatching.Data.Repositories
{
    /// <summary>
    /// GenericAsyncReadOnlyRepository Repository is used to provide to concrete / specific Repositories common methods to GetAll, Get by filter, GetById
    /// </summary>
    /// <typeparam name="TEntity">type parameter</typeparam>
    public class GenericAsyncReadOnlyRepository<TEntity> : IGenericAsyncReadOnlyRepository<TEntity> where TEntity : class
    {
        protected DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericAsyncReadOnlyRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// GetAllAsync async method returns collection of type parameter entities
        /// </summary>
        /// <returns>return collection of entities of specific type defined by type parameter</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// GetAsync method
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="includeProperties">include child properties to be filled with data</param>
        /// <returns>return collection of entities of specific type defined by type parameter</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).AsNoTracking().ToListAsync();
            }
            else
            {
                return await query.AsNoTracking().ToListAsync();
            }
        }

        /// <summary>
        /// GetByIDAsync method
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>returns entity of specific type defined by type parameter</returns>
        public virtual async Task<TEntity> GetByIDAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
        /*
        public virtual async Task InsertAsync(TEntity entity)
        {
            dbSet.Add(entity);
            await SaveAsync();
        }

        public virtual async Task DeleteAsync(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            await DeleteAsync(entityToDelete);
        }

        public async Task DeleteAsync(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);

            await SaveAsync();
        }

        public async Task UpdateAsync(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;

            await SaveAsync();
        }

        public virtual async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }*/
    }
}
