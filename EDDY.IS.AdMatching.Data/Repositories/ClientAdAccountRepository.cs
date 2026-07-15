using EDDY.IS.AdMatching.Data.Context;
using EDDY.IS.AdMatching.Entities;
using EDDY.IS.AdMatching.Repositories.Interfaces;

namespace EDDY.IS.AdMatching.Data.Repositories
{
    /// <summary>
    /// ClientAdAccountRepository repository implements IClientAdAccountRepository with specific method implementations and inherits common methods from GenericReadOnlyRepository
    /// </summary>
    public class ClientAdAccountRepository : GenericReadOnlyRepository<ClientAdAccount>, IClientAdAccountRepository
    {

        public ClientAdAccountRepository(GlassPanelContext dbContext) : base(dbContext) { }

        private bool disposed = true;

        /// <summary>
        /// Dispose method disposes of context that is utilized under the hood and deallocates / marks Repository for Garbage collection
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
