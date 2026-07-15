using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Entities;

namespace EDDY.IS.AdMatching.Domain.Services.Interfaces
{
    /// <summary>
    /// IDataManager interface is used to declare method to get data out of RedisCache clustering environment from within
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// GetDictionaryContainer method
        /// </summary>
        /// <returns>return DictionaryContainer encapsulating data collections to be filtered by CampaignSource eventually by Engine concept</returns>
        public DictionaryContainer GetDictionaryContainer();

        /// <summary>
        /// Get static ad no matter if the ad is paused or deleted
        /// </summary>
        /// <param name="statidAdGuid"></param>
        /// <returns></returns>
        Ad? GetStaticAd(Guid statidAdGuid);
    }
}
