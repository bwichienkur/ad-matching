using EDDY.IS.AdMatching.Data.Context;
using EDDY.IS.AdMatching.Entities;
using EDDY.IS.AdMatching.Repositories.Interfaces;

namespace EDDY.IS.AdMatching.Data.Repositories
{
    public class CampaignScheduleRepository: GenericReadOnlyRepository<CampaignSchedule>, ICampaignScheduleRepository
    {
        public CampaignScheduleRepository(GlassPanelContext dbContext) : base(dbContext) { }

        private bool disposed = true;

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
