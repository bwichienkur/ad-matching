using EDDY.IS.AdMatching.EAV.Models;

namespace EDDY.IS.AdMatching.EAV.Interface
{
    public interface IEddyAdsListingService
    {
        Task<EddyAdsListingResponse> GetEddyAdsListingMatched(EddyAdsListingRequest request);
    }
}
