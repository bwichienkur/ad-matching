namespace EDDY.IS.AdMatching.Core.Auxiliary
{
    public enum Status
    {
        Success = 200,
        Error = 400

    }

    public enum CampaignCapTypeId
    {
        NoCaps = 1,
        DailyClickCaps = 2,
        DailyAllocationCaps = 3

    }

    public enum CampaignLevelId
    {
        Campaign = 1,
        AdGroup = 2,
        Ad = 3

    }

    public enum ScheduleOptionId
    {
        Alldays = 1,
        Weekdays = 2,
        Weekends = 3,
        Monday = 4,
        Tuesday = 5,
        Wednesday = 6,
        Thursday = 7,
        Friday = 8,
        Saturday = 9,
        Sunday = 10
    }

    public enum TimeZoneId
    {        
        Local = 8
    }

    public enum ISApplication
    {
        eLDrupal = 1,
        EMDDrupal = 2,
        ExpressDirectories = 3,
        FormsEngine = 4,
        MatchingEngine = 5,
        VendorAPI = 6,
        HostAndPost = 7,
        PixelService = 8,
        Apollo = 9,
        TDC = 10,
        CE = 11,
        SC = 12,
        LandingPages = 13,
        ProspectService = 14,
        Tracking = 15,
        LeadService = 16,
        ABEAService = 17,
        Leadping = 18,
        LeadScoring = 19,
        AdAggregator = 20,
        ISAdmin = 21,
        EddyAdVendorService = 22,
        ValidationService = 23,
        ExternalMatch = 24,
        DataExchange = 25,
        EMS = 27,
        AzureFunction = 28,
        EMSLeadService = 29,
        Unigo = 30,
        AdSyncProcess = 31,
        AdMatching = 32
    }
}
