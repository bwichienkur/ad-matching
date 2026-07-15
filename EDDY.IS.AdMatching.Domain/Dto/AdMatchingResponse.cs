
using EDDY.IS.AdMatching.Domain.BusinessEntities;

namespace EDDY.IS.AdMatching.Domain.Dto;

public class AdMatchingResponse
{
    public int AdsReturned { get; set; }
    public string Message { get; set; }
    public List<AdsMatched> AdsMatched { get; set; } = new List<AdsMatched>();
}