using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwStandardClientReport
    {
        public int DateKey { get; set; }
        public int ClientAdAccountId { get; set; }
        public string ClientAccountName { get; set; } = null!;
        public int? CampaignId { get; set; }
        public string? CampaignName { get; set; }
        public int? AdGroupId { get; set; }
        public string? AdGroupName { get; set; }
        public int? AdId { get; set; }
        public string? AdName { get; set; }
        public string? AllocationType { get; set; }
        public int FormLoadCount { get; set; }
        public int NumberOfSearches { get; set; }
        public int RevenueSearch { get; set; }
        public int UniqueSearches { get; set; }
        public int NumberOfImpressions { get; set; }
        public int AveragePosition { get; set; }
        public int UniqueClicks { get; set; }
        public int PositionClick { get; set; }
        public int NumberOfClicks { get; set; }
        public int SearchClicks { get; set; }
        public int AdCtr { get; set; }
        public int Cost { get; set; }
        public int MediaCost { get; set; }
        public int Margin { get; set; }
        public int UipixelCount { get; set; }
        public int Cruipixel { get; set; }
        public int Pixel1Cost { get; set; }
        public int TotalLeads { get; set; }
        public int CrtotalLeads { get; set; }
        public int CostPerTotalLead { get; set; }
        public int GoodLeads { get; set; }
        public int CrgoodLeads { get; set; }
        public int GoodLeadCost { get; set; }
        public int Apps { get; set; }
        public int AppCost { get; set; }
        public int Starts { get; set; }
        public int StartCost { get; set; }
        public int? ZipCode { get; set; }
        public string UserCity { get; set; } = null!;
        public string UserCounty { get; set; } = null!;
        public string UserState { get; set; } = null!;
        public string DeviceType { get; set; } = null!;
        public string LandingPage { get; set; } = null!;
        public int? SourceId { get; set; }
        public int? SourceName { get; set; }
        public int? SubSourceId { get; set; }
        public int? SubSourceName { get; set; }
        public int ClicksRepId { get; set; }
        public int? ProductTypeId { get; set; }
        public string? ProductType { get; set; }
        public int ClientRelationshipId { get; set; }
        public string Vendor { get; set; } = null!;
        public string VendorSubDeal { get; set; } = null!;
        public string VendorPlacement { get; set; } = null!;
        public string VendorCreative { get; set; } = null!;
        public string VendorLineItem { get; set; } = null!;
        public int Year { get; set; }
        public int Quarter { get; set; }
        public int Month { get; set; }
        public int Weekday { get; set; }
        public int Day { get; set; }
        public int DayOfWeek { get; set; }
        public int Hour { get; set; }
        public int LocalHour { get; set; }
        public string Time { get; set; } = null!;
        public string WeekdayOrWeekend { get; set; } = null!;
        public int? AdType { get; set; }
        public int? UrlAccountName { get; set; }
        public int? CampaignType { get; set; }
    }
}
