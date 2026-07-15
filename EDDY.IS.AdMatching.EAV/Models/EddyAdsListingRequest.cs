namespace EDDY.IS.AdMatching.EAV.Models
{
    public class EddyAdsListingRequest
    {
        public EddyAdsListingRequest(int maxAds, string trackId, string placementViewGuid
                                    , int maxPrograms, double conversionRate, string? widgetName
                                    , string? widgetRequestGuid, Dictionary<string, string>? parameters
                                    , int applicationId, List<string> duplicateForInstitutionList
                                    , string? LeadIniatingUrl
                                    )
        {
            MaxAds = maxAds;
            TrackId = trackId;
            PlacementViewGuid = placementViewGuid;
            ConversionRate = conversionRate;
            WidgetName = widgetName;
            WidgetRequestGuid = widgetRequestGuid;
            Parameters = parameters;
            ApplicationId = applicationId;
            DuplicateForInstitutionList = duplicateForInstitutionList;
            LeadInitiatingUrl = LeadIniatingUrl;
            MaxPrograms = maxPrograms;
        }
        
        public int MaxAds { get; set; }
        public string TrackId { get; set; }
        public string PlacementViewGuid { get; set; }
        public int MaxPrograms { get; set; }
        public double ConversionRate { get; set; }
        public string? WidgetName { get; set; }
        public string? WidgetRequestGuid { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }
        public int ApplicationId { get; set; }
        public List<string> DuplicateForInstitutionList { get; set; }
        public string? LeadInitiatingUrl { get; set; }
    }
}
