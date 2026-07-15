using EDDY.IS.AdMatching.Domain.Dto;

namespace EDDY.IS.AdMatching.Domain.Services.Interfaces;

public interface IAdMatchingService
{
    Task<AdMatchingResponse> GetAdsMatched(AdMatchingRequest request);
}