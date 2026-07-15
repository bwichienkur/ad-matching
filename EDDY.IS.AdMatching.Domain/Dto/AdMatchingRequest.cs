namespace EDDY.IS.AdMatching.Domain.Dto;

public class AdMatchingRequest
{
    public string SearchGuid { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
    public int MaxAds { get; set; }
    public int SourceId { get; set; }
    public string StaticAdGuid { get; set; }
    public List<string> PreExcludeInstitutions { get; set; }
}