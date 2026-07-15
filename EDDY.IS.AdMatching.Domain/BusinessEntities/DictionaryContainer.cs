using EDDY.IS.AdMatching.Entities;

namespace EDDY.IS.AdMatching.Domain.BusinessEntities
{
    /// <summary>
    /// DictionaryContainer - dummy container mimics utilization of RedisCache at this time.
    /// </summary>
    public class DictionaryContainer
    {
        public DictionaryContainer() {
            ClientAdAccounts = new Dictionary<int, ClientAdAccount>();
            CampaignsList = new Dictionary<int, Campaign>();
            CampaignSchedulerList = new Dictionary<int, CampaignSchedule>();
            
            CampaignsBySource = new Dictionary<int, List<VwSourceByCampaignAms>>();
            ProductTypesBySource = new Dictionary<int, List<SourceProductType>>();

            SlimAdsDictionary = new Dictionary<string, SlimAd>();

            TargetingRules = new List<TargetingRule>();
            CampaignSourceDictionary = new Dictionary<string, CampaignSource>();

            TimeZoneList = new Dictionary<int, Entities.TimeZone>();
            StateTimeZoneList = new Dictionary<string, StateTimeZone>(); 
        }
        public Dictionary<string, CampaignSource> CampaignSourceDictionary { get; set; }
        public Dictionary<int, AdGroup>? AdGroupList { get; set; }
        public Dictionary<int, Campaign>? CampaignsList { get; set; }
        public Dictionary<int, AdGroupAd>? AdGroupAdList { get; set; }
        public Dictionary<int, ClientAdAccount>? ClientAdAccounts { get; set; }
        public Dictionary<int, CampaignSchedule>? CampaignSchedulerList { get; set; }
        public Dictionary<int, CampaignStop>? CampaignStopList { get; set; }
        public Dictionary<int, List<ClientAdAccountStop>>? ClientAccountStopList { get; set; }
                
        public Dictionary<int, ClientAdAccountBudget>? ClientAdAccountBudgetList { get; set; }
        public Dictionary<string, VwAdsAm>? AdsDictionary { get; set; }
        public Dictionary<string, SlimAd>? SlimAdsDictionary { get; set; }
        public Dictionary<int, ClientAdAccountDefaultParameter>? ClientAdAccountDefaultParameterList { get; set; }
        public Dictionary<int, ClientAdAccountParameter>? ClientAdAccountParameterList { get; set; }
        
        //Shared
        public Dictionary<int, CampaignRelationship>? CampaignRelationshipList { get; set; }
        public Dictionary<int, Entities.TimeZone>? TimeZoneList { get; set; }
        public Dictionary<string, StateTimeZone> StateTimeZoneList { get; set; }
        public Dictionary<int, ScheduleOption>? ScheduleOptionList { get; set; }

        // Revised
        public List<TargetingRule> TargetingRules { get; set; }

        //Refactored
        public Dictionary<int, List<VwSourceByCampaignAms>> CampaignsBySource { get; set; }
        public Dictionary<int, List<SourceProductType>> ProductTypesBySource { get; set; }



    }

    public class FilteredContainerDictionary : FilteredDictionary
    {
        public FilteredContainerDictionary() 
            : base()
        {
            Ads = new Dictionary<string, VwAdsAm>();
            SlimAds = new Dictionary<string, SlimAd>();
            CampaignRelationshipList = new Dictionary<int, CampaignRelationship>();
            TimeZoneList = new Dictionary<int, Entities.TimeZone>();
            StateTimeZoneList = new Dictionary<string, StateTimeZone>();
            ScheduleOptionList = new Dictionary<int, ScheduleOption>();
    }

        public void LoadContainer(FilteredContainerDictionary container) {
            Ads = container.Ads;
            SlimAds = container.SlimAds;
            CampaignRelationshipList = container.CampaignRelationshipList;
            TimeZoneList = container.TimeZoneList;
            StateTimeZoneList = container.StateTimeZoneList;
            ScheduleOptionList = container.ScheduleOptionList;
        }

        // shared
        public Dictionary<string, VwAdsAm>? Ads { get; set; }
        public Dictionary<string, SlimAd>? SlimAds { get; set; }
        public Dictionary<int, CampaignRelationship>? CampaignRelationshipList { get; set; }
        public Dictionary<int, Entities.TimeZone>? TimeZoneList { get; set; }
        public Dictionary<string, StateTimeZone> StateTimeZoneList { get; set; }
        public Dictionary<int, ScheduleOption>? ScheduleOptionList { get; set; }

        public FilteredContainerDictionary CreateShallowCopy(FilteredContainerDictionary container)
        {

            var copy = new FilteredContainerDictionary();
            copy.CampaignsList = new Dictionary<int, Campaign>(this.CampaignsList);
            copy.ClientAdAccounts = new Dictionary<int, ClientAdAccount>(this.ClientAdAccounts);
            copy.CampaignSchedulerList = new Dictionary<int, CampaignSchedule>(this.CampaignSchedulerList);
            copy.CampaignStopList = new Dictionary<int, CampaignStop>(this.CampaignStopList);
            copy.SlimAdsDictionary = new Dictionary<string, SlimAd>(this.SlimAdsDictionary);
            copy.CampaignsFiltered = new List<VwSourceByCampaignAms>(this.CampaignsFiltered);
            copy.ClientAccountStopFiltered = new List<ClientAdAccountStop>(this.ClientAccountStopFiltered);
            copy.TargetingRulesFiltered = new List<TargetingRule>(this.TargetingRulesFiltered);
            copy.ClientAdAccountDefaultParameterList = new Dictionary<int, ClientAdAccountDefaultParameter>(this.ClientAdAccountDefaultParameterList);
            copy.ClientAdAccountParameterList = new Dictionary<int, ClientAdAccountParameter>(this.ClientAdAccountParameterList);

            copy.Ads = container.Ads;
            copy.CampaignRelationshipList = container.CampaignRelationshipList;
            copy.TimeZoneList = container.TimeZoneList;
            copy.StateTimeZoneList = container.StateTimeZoneList;
            copy.ScheduleOptionList = container.ScheduleOptionList;

            return copy;
        }
    }


    public class FilteredDictionary
    {

        public FilteredDictionary() {
            CampaignsList = new Dictionary<int, Campaign>();
            CampaignSchedulerList = new Dictionary<int, CampaignSchedule>();
            SlimAdsDictionary = new Dictionary<string, SlimAd>();
            ClientAdAccounts = new Dictionary<int, ClientAdAccount>();
            CampaignsFiltered = new List<VwSourceByCampaignAms>();
            ClientAccountStopFiltered = new List<ClientAdAccountStop>();
            TargetingRulesFiltered = new List<TargetingRule>();
        }

        public Dictionary<int, Campaign> CampaignsList { get; set; }
        public Dictionary<int, ClientAdAccount> ClientAdAccounts { get; set; }
        public Dictionary<int, CampaignSchedule> CampaignSchedulerList { get; set; }
        public Dictionary<int, CampaignStop>? CampaignStopList { get; set; }
        public Dictionary<string, SlimAd> SlimAdsDictionary { get; set; }
        
        public Dictionary<int, ClientAdAccountDefaultParameter>? ClientAdAccountDefaultParameterList { get; set; }
        public Dictionary<int, ClientAdAccountParameter>? ClientAdAccountParameterList { get; set; }


        // Filtered
        public List<VwSourceByCampaignAms> CampaignsFiltered { get; set; }
        public List<ClientAdAccountStop> ClientAccountStopFiltered { get; set; }
        public List<TargetingRule> TargetingRulesFiltered { get; set; }
    }

}
