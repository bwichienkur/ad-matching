using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Entities;

namespace EDDY.IS.AdMatching.Domain.Models
{
    public class AdMatchingModel
    {
        public AdMatchingModel()
        {
            FinalAdsList = new();
            Messages = new();
            DynamicParameters = new();
            Parameters = new();
            FinalAdGroupList = new();
            StaticAds = new();
            PreExcludeInstitutions = new();
            Filtered = new();
            MainDictionaryEvaluated = new();
            IsStatic = false;
            MaxAds = 0;
            SearchGuid = String.Empty;
        }

        public FilteredContainerDictionary Filtered { get; set; }
        public FilteredDictionary MainDictionaryEvaluated { get; set; }
        public List<AdsMatched> FinalAdsList { get; set; }
        public List<string> Messages { get; set; }
        public Dictionary<string, string> DynamicParameters { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string SearchGuid { get; set; }
        public List<AdGroup> FinalAdGroupList { get; set; }
        public int MaxAds { get; set; }
        public VwAdsAm StaticAds { get; set; }
        public bool IsStatic { get; set; }
        public Guid StaticAdGuid { get; set; }
        public List<string> PreExcludeInstitutions { get; set; }
        public int SourceId { get; set; }
    }
}
