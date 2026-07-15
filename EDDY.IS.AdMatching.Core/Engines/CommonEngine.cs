using EDDY.IS.AdMatching.Core.Auxiliary;
using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.AdMatching.Entities;
using EDDY.IS.Common.Settings;
using Microsoft.Extensions.Options;
using NewRelic.Api.Agent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.Engines
{
    public class CommonEngine : IEngine
    {
        private readonly ICacheService _cacheService;
        private IDataManager _dataManager;

        public CommonEngine(
            IDataManager dataManager,
            ICacheService cacheService
            )
        {
            _cacheService = cacheService;
            _dataManager = dataManager;
        }

        private static string GetSourceCacheKey(int sourceId) => $"filteredDictionaryCacheKey:ForSourceId{sourceId}";
        private static string GetSharedCacheKey => $"sharedDictionaryCacheKey";

        [Trace]
        public async Task<FilteredContainerDictionary> LoadSharedContainer(DictionaryContainer container)
        {
            FilteredContainerDictionary dictionaryContainerFiltered = new FilteredContainerDictionary();
            var needRecompute = await _cacheService.NeedsReCompute(GetSharedCacheKey);
            if (!needRecompute)
            {
                dictionaryContainerFiltered = await _cacheService.RefreshFromDistributedCache<FilteredContainerDictionary>(GetSharedCacheKey);
                if (dictionaryContainerFiltered != null) return dictionaryContainerFiltered;

                dictionaryContainerFiltered = new FilteredContainerDictionary();
            }

            //Shared items
            dictionaryContainerFiltered.Ads = container.AdsDictionary;
            dictionaryContainerFiltered.SlimAds = container.SlimAdsDictionary;
            dictionaryContainerFiltered.CampaignRelationshipList = container.CampaignRelationshipList;
            dictionaryContainerFiltered.TimeZoneList = container.TimeZoneList;
            dictionaryContainerFiltered.StateTimeZoneList = container.StateTimeZoneList;
            dictionaryContainerFiltered.ScheduleOptionList = container.ScheduleOptionList;

            await _cacheService.SetValueToCache(GetSharedCacheKey, dictionaryContainerFiltered);
            return dictionaryContainerFiltered;
        }

        [Trace]
        public async Task<FilteredContainerDictionary> FilterDictionaryContainer(int sourceId, DictionaryContainer container)
        {
            FilteredContainerDictionary dictionaryContainerFiltered = new FilteredContainerDictionary();

            var cacheKey = GetSourceCacheKey(sourceId);
            var needRecompute = await _cacheService.NeedsReCompute(cacheKey);
            if (!needRecompute) {
                dictionaryContainerFiltered = await _cacheService.RefreshFromDistributedCache<FilteredContainerDictionary>(cacheKey);
                if (dictionaryContainerFiltered != null) return dictionaryContainerFiltered;

                dictionaryContainerFiltered = new FilteredContainerDictionary();
            }

            dictionaryContainerFiltered.CampaignsFiltered = 
                container.CampaignsBySource.GetValueOrDefault(sourceId, new List<VwSourceByCampaignAms>());

            var productTypeFiltered =
                container.ProductTypesBySource.GetValueOrDefault(sourceId, new List<SourceProductType>());

            //Filter the children campaings for all the campaigns related to the source provided
            var campaignSourceList =
                AddChildrenCampaignToCampaignSource(container, sourceId);

            //filter client ad accounts by SourceProductType
            var clientAdAccountBudgetList =
                FilterClientAdAccountBudget(productTypeFiltered, container);

            //filter campaigns by ClientAdAccount
            dictionaryContainerFiltered.CampaignsList = FilterCampaignByClientAdAccountBudget(
                clientAdAccountBudgetList,
                productTypeFiltered, container, sourceId,
                campaignSourceList);

            //filter client ad accounts by campaigns
            dictionaryContainerFiltered.ClientAdAccounts =
                FilterClientAccounts(dictionaryContainerFiltered.CampaignsList,
                dictionaryContainerFiltered.CampaignsFiltered,
                container);

            var adGroupList =
                FilterAdGroups(dictionaryContainerFiltered.CampaignsList, container);

            //Filter Ads for AMS by campaign
            dictionaryContainerFiltered.SlimAdsDictionary =
                FilterAdsByCampaing(sourceId, dictionaryContainerFiltered.CampaignsList, container, adGroupList);

            //filter client ad account stops
            dictionaryContainerFiltered.ClientAccountStopFiltered =
                FilterClientAccountStops(dictionaryContainerFiltered.ClientAdAccounts, container);

            //filter client ad account parameters
            dictionaryContainerFiltered.ClientAdAccountParameterList =
                FilterClientAccountParameters(dictionaryContainerFiltered.ClientAdAccounts, container);

            dictionaryContainerFiltered.ClientAdAccountDefaultParameterList =
                FilterClientAccountDefaultParameters(container);

            //Filter Campaign Stops
            dictionaryContainerFiltered.CampaignStopList =
                FilterCampaignStops(dictionaryContainerFiltered.CampaignsList, container);

            //Filter Campaing Scheduling
            dictionaryContainerFiltered.CampaignSchedulerList =
                FilterSchedulingRules(dictionaryContainerFiltered, container.CampaignSchedulerList,
                    container.CampaignRelationshipList, container.CampaignsList);

            dictionaryContainerFiltered.TargetingRulesFiltered =
                FilterTargetingRules(dictionaryContainerFiltered, container.TargetingRules,
                    container.CampaignRelationshipList, container.CampaignsList, adGroupList);

            await _cacheService.SetValueToCache(cacheKey, dictionaryContainerFiltered);

            return dictionaryContainerFiltered;
        }

        [Trace]
        public async Task<FilteredContainerDictionary> GetCacheDictionaryContainer(int sourceId) {

            var dictionaryFiltered = await _cacheService.GetFromCache<FilteredContainerDictionary>(GetSourceCacheKey(sourceId));
            var containerDictionary = await _cacheService.GetFromCache<FilteredContainerDictionary>(GetSharedCacheKey);

            if (dictionaryFiltered == null || containerDictionary == null) {
                var container = _dataManager.GetDictionaryContainer();
                containerDictionary = await this.LoadSharedContainer(container);
                dictionaryFiltered = await this.FilterDictionaryContainer(sourceId, container);
            }

            // Return shallow copy to avoid mutations on cached item
            return dictionaryFiltered.CreateShallowCopy(containerDictionary);
        }

        public List<TargetingRule> FilterTargetingRules(
            FilteredContainerDictionary dictionaryContainerFiltered,
            List<TargetingRule>? containerTargetingRules,
            Dictionary<int, CampaignRelationship>? containerCampaignRelationshipList,
            Dictionary<int, Campaign>? containerCampaignsList,
            Dictionary<int, AdGroup> adGroupAdList)
        {
            var resultingFilteredTargetingRules = new List<TargetingRule>();

            //Get the targeting rules for the campaign
            //Update: based on the CampaignRelationship we need to retrieve the parent campaigns and override the 
            //targeting rules based on the parents BUT write the rules as if they belong to the child
            foreach (var campaign in dictionaryContainerFiltered.CampaignsList)
                foreach (Override overrideType in Enum.GetValues(typeof(Override)))
                {
                    var parentCampaign =
                        GetParentCampaignOverriding(campaign.Value,
                            overrideType,
                            containerCampaignRelationshipList,
                            containerCampaignsList
                        );
                    IEnumerable<TargetingRule> parentTargetingRules = null;

                    if (parentCampaign != null)
                    {
                        //get the targeting rules for the parent that will override this type AND 
                        parentTargetingRules = containerTargetingRules.Where(tr =>
                            tr.CampaignId == parentCampaign.CampaignId
                            && OverrideMatchesTheTargetingRule(tr, overrideType)
                        );

                        if (parentTargetingRules != null)
                            foreach (var parentTargetingRule in parentTargetingRules)
                            {
                                /* Parent replaces all child rules if linked */
                                var childTargetingRule = new TargetingRule()
                                {
                                    CampaignId = campaign.Value?.CampaignId,
                                    RuleJson = parentTargetingRule.RuleJson,
                                    IsDeleted = parentTargetingRule.IsDeleted,
                                    IsDynamicBid = parentTargetingRule.IsDynamicBid,
                                    IsOptimization = parentTargetingRule.IsOptimization,
                                    RuleAsQueryBuilderFilterRule = parentTargetingRule.RuleAsQueryBuilderFilterRule,
                                    DynamicBoostPercent = parentTargetingRule.DynamicBoostPercent,
                                    IsEnabled = parentTargetingRule.IsEnabled
                                };
                                
                                AddTargetingRuleToResult(resultingFilteredTargetingRules, childTargetingRule);
                                
                            }
                    }
                    else
                    {
                        var childTargetingRules = containerTargetingRules.Where(tr =>
                            tr.CampaignId == campaign.Value?.CampaignId
                            && OverrideMatchesTheTargetingRule(tr, overrideType)
                        );
                        foreach (var childTargetingRule in childTargetingRules)
                        {
                            //childTargetingRule.CreateAndAssignRuleAsQueryBuilderFilterRule();
                            AddTargetingRuleToResult(resultingFilteredTargetingRules, childTargetingRule);
                        }
                    }
                }

            //Get the targeting rules for the AdGroup
            if (adGroupAdList != null && adGroupAdList.Any())
            {
                var adGroupTargetingRules = from tr in containerTargetingRules
                    where adGroupAdList.Any
                    (ag => ag.Value.AdGroupId == tr.AdGroupId
                           && !tr.IsDeleted
                           && tr.IsEnabled  //check why are being created not enabled on the db
                    )
                    select tr;

                AddTargetingRulesToResult(resultingFilteredTargetingRules, adGroupTargetingRules);
            }

            return resultingFilteredTargetingRules;
        }

        public Dictionary<int, CampaignSchedule> FilterSchedulingRules(FilteredContainerDictionary dictionaryContainerFiltered,
            Dictionary<int, CampaignSchedule>? containerSchedulerList,
            Dictionary<int, CampaignRelationship>? containerCampaignRelationshipList,
            Dictionary<int, Campaign>? containerCampaignsList)
        {
            var resultingFilteredCampaignSchedules = new Dictionary<int, CampaignSchedule>();


            // TODO: Refactor this thing and get rid of those indexes from counters

            //Get the targeting rules for the campaign
            //Update: based on the CampaignRelationship we need to retrieve the parent campaigns and override the 
            //targeting rules based on the parents BUT write the rules as if they belong to the child
            foreach (var campaign in dictionaryContainerFiltered.CampaignsList)
            {
                var parentCampaignOverriding =
                    GetParentCampaignOverridingScheduling(campaign.Value,
                        containerCampaignRelationshipList,
                        containerCampaignsList
                    );

                if (parentCampaignOverriding != null)
                {
                    IEnumerable<KeyValuePair<int, CampaignSchedule>> parentSchedulerList = containerSchedulerList.Where(
                        tr =>
                            tr.Value.CampaignId == parentCampaignOverriding.CampaignId
                            && tr.Value.IsEnabled && !tr.Value.IsDeleted
                    );

                    if (parentSchedulerList != null)
                        foreach (var parentScheduler in parentSchedulerList)
                        {
                            var childCampaignSchedule =
                                new KeyValuePair<int, CampaignSchedule>(parentScheduler.Key, new CampaignSchedule());
                            childCampaignSchedule.Value.CampaignScheduleId = resultingFilteredCampaignSchedules.Count+1;
                            childCampaignSchedule.Value.CampaignId = campaign.Value.CampaignId;
                            childCampaignSchedule.Value.StartTime = parentScheduler.Value.StartTime;
                            childCampaignSchedule.Value.EndTime = parentScheduler.Value.EndTime;
                            childCampaignSchedule.Value.IsEnabled = parentScheduler.Value.IsEnabled;
                            childCampaignSchedule.Value.IsDeleted = parentScheduler.Value.IsDeleted;
                            childCampaignSchedule.Value.ScheduleOption = parentScheduler.Value.ScheduleOption;
                            childCampaignSchedule.Value.ScheduleOptionId = parentScheduler.Value.ScheduleOptionId;
                            childCampaignSchedule.Value.Bid = parentScheduler.Value.Bid;
                            resultingFilteredCampaignSchedules.Add(resultingFilteredCampaignSchedules.Count+1, childCampaignSchedule.Value);
                        }
                }
                else
                {
                    var schedulerForCampaigns = containerSchedulerList.Where(tr =>
                        tr.Value.CampaignId == campaign.Value?.CampaignId
                    );
                    foreach (var schedulerForCampaign in schedulerForCampaigns)
                    {
                        resultingFilteredCampaignSchedules.Add(resultingFilteredCampaignSchedules.Count+1, schedulerForCampaign.Value);
                    }
                }
            }
            return resultingFilteredCampaignSchedules;
        }

        private Campaign? GetParentCampaignOverridingScheduling(
            Campaign? campaign,
            Dictionary<int, CampaignRelationship>? containerCampaignRelationshipList,
            Dictionary<int, Campaign>? containerCampaignsList)
        {
            Campaign? resParentCampaign = null;

            if (containerCampaignRelationshipList != null && containerCampaignRelationshipList.Any() && campaign != null)
            {
                var parentCampaignRelationship =
                    containerCampaignRelationshipList
                    .Where(cr => cr.Value.CampaignId == campaign.CampaignId)
                    .Select(cr => cr.Value)
                    .FirstOrDefault();

                if (parentCampaignRelationship != null && containerCampaignsList != null)
                {
                    var parentCampaignId = parentCampaignRelationship.ParentCampaignId;
                    var parentCampaign = containerCampaignsList
                                            .Where(c => c.Value.CampaignId == parentCampaignId)
                                            .Select(c => c.Value)
                                            .FirstOrDefault();

                    if (parentCampaign != null && parentCampaignRelationship.LinkParentSchedule)
                        resParentCampaign = parentCampaign;

                    var res = GetParentCampaignOverridingScheduling(resParentCampaign,
                        containerCampaignRelationshipList,
                        containerCampaignsList);
                    if (res != null) resParentCampaign = res;
                }
            }

            return resParentCampaign;
        }

        private Campaign? GetParentCampaignOverriding(
            Campaign? campaign,
            Override overrideType,
            Dictionary<int, CampaignRelationship>? containerCampaignRelationshipList,
            Dictionary<int, Campaign>? containerCampaignsList)
        {
            Campaign? resParentCampaign = null;

            if (containerCampaignRelationshipList != null && containerCampaignRelationshipList.Any() && campaign != null)
            {
                var parentCampaignRelationship =
                    containerCampaignRelationshipList.Where(
                        cr => cr.Value.CampaignId == campaign.CampaignId
                    ).Select(cr => cr.Value)
                    .FirstOrDefault();

                if (parentCampaignRelationship != null && containerCampaignsList != null)
                {
                    var parentCampaignId = parentCampaignRelationship.ParentCampaignId;
                    var parentCampaign = containerCampaignsList.FirstOrDefault(c =>
                        c.Value.CampaignId == parentCampaignId);

                    if (OverrideMatchesTheCampaignRelationshipOverrideType(parentCampaignRelationship, overrideType))
                        resParentCampaign = parentCampaign.Value;

                    var res = GetParentCampaignOverriding(resParentCampaign, 
                        overrideType,
                        containerCampaignRelationshipList,
                        containerCampaignsList);
                    if (res != null) resParentCampaign = res;
                }
            }

            return resParentCampaign;
        }

        private bool OverrideMatchesTheTargetingRule(TargetingRule targetingRule, Override overrideType)
        {
            switch (overrideType)
            {
                case Override.BidingVariable:
                    return targetingRule.IsDynamicBid;
                case Override.OptimizationRule:
                    return targetingRule.IsOptimization;
                case Override.TargetingRule:
                    return !targetingRule.IsDynamicBid && !targetingRule.IsOptimization;
            }

            return false;
        }

        private bool OverrideMatchesTheCampaignRelationshipOverrideType(CampaignRelationship campaignRelationship,
            Override overrideType)
        {
            switch (overrideType)
            {
                case Override.BidingVariable:
                    return campaignRelationship.LinkParentDynamicBidVariables;
                case Override.OptimizationRule:
                    return campaignRelationship.LinkParentOptimizations;
                case Override.TargetingRule:
                    return campaignRelationship.LinkParentRules;
            }

            return false;
        }

        private static void AddTargetingRulesToResult(List<TargetingRule> resultingTargetingRules,
            IEnumerable<TargetingRule> rulesToAppend)
        {
            foreach (var targetingRule in rulesToAppend)
            {
                resultingTargetingRules.Add(targetingRule);
            }
        }

        private static void AddTargetingRuleToResult(List<TargetingRule> resultingTargetingRules,
            TargetingRule ruleToAppend)
        {
            if (!ruleToAppend.IsEmptyRule())
                resultingTargetingRules.Add(ruleToAppend);
        }


        /// <summary>
        /// Return the client ad accounts that are active and has relationship with the source id.
        /// </summary>
        /// <param name="filteredCampaigns"></param>
        /// <param name="FilteredSourcesByCampaing"></param>
        /// <param name="_container"></param>
        /// <returns></returns>
        private Dictionary<int, ClientAdAccount> FilterClientAccounts(
            Dictionary<int, Campaign> filteredCampaigns,
            List<VwSourceByCampaignAms> FilteredSourcesByCampaing,
            DictionaryContainer _container)
        {
            Dictionary<int, ClientAdAccount> FilteredClientAccounts = null;

            // p => client ad accounts that are active
            var filtered = from p in _container.ClientAdAccounts
                where filteredCampaigns.Any
                (bc => bc.Value.ClientAdAccountId == p.Value.ClientAdAccountId && p.Value.IsEnabled &&
                       !p.Value.IsDeleted && !p.Value.Status.Contains("Paused"))
                select p;


            // p => client ad accounts that has campaign with source associated
            var filteredAccounts = from p in filtered
                                    where FilteredSourcesByCampaing.Any
                                    (bc => bc.ClientAdAccountId == p.Value.ClientAdAccountId)
                                    select p;

            FilteredClientAccounts = filteredAccounts.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return FilteredClientAccounts;
        }

        /// <summary>
        /// Return the list of client ad account budgets that match with the product type
        /// </summary>
        /// <param name="filteredSourceProductType"></param>
        /// <param name="_container"></param>
        /// <returns></returns>
        private List<ClientAdAccountBudget> FilterClientAdAccountBudget(
            List<SourceProductType> filteredSourceProductType, 
            DictionaryContainer _container)
        {
            // TODO: add filter by clientAdAccount
            var FilteredClientAccountBudgets = new List<ClientAdAccountBudget>();

            var filtered = from p in _container.ClientAdAccountBudgetList
                where filteredSourceProductType.Any
                (bc => bc.ProductTypeId == p.Value.ProductTypeId && !p.Value.IsCapped &&
                       (bool) p.Value.IsEnabled && !p.Value.IsDeleted)
                select p.Value;

            FilteredClientAccountBudgets = filtered.ToList();

            return FilteredClientAccountBudgets;
        }

        private Dictionary<int, Campaign> FilterCampaignByClientAdAccountBudget(
            List<ClientAdAccountBudget> FilteredClientAccountBudgets,
            List<SourceProductType> filteredSourceProductType, DictionaryContainer _container, int sourceId,
            List< CampaignSource> FilteredCampaignSources)
        {
            Dictionary<int, Campaign> FilteredCampaigns = null;

            var filtered = from p in _container.CampaignsList
                join f in filteredSourceProductType on p.Value.ProductTypeId equals f.ProductTypeId
                where FilteredClientAccountBudgets.Any
                (bc => bc.ClientAdAccountId == p.Value.ClientAdAccountId && !p.Value.IsCapped &&
                       (bool) p.Value.IsEnabled && !p.Value.IsDeleted &&
                       p.Value.ProductTypeId == bc.ProductTypeId)
                select p;

            var filteredCampaigns = from p in filtered
                where FilteredCampaignSources.Any
                (bc => bc.CampaignId == p.Value.CampaignId && bc.SourceId == sourceId &&
                       bc.IsEnabled && !bc.IsDeleted)
                select p;

            FilteredCampaigns = filteredCampaigns.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return FilteredCampaigns;
        }


        private List<CampaignSource> AddChildrenCampaignToCampaignSource(DictionaryContainer container,
            int sourceId)
        {
            // TODO: Refactor this logic, maybe can be simplify after the Main Dictionary refactor

            List<CampaignSource> FinalCampaignSource = new List<CampaignSource>();
            List<CampaignSource> AdditionalCampaigns = new List<CampaignSource>();

            FinalCampaignSource = container.CampaignSourceDictionary.Values
                .Where(x => x.SourceId == sourceId)
                .ToList();

            int maxId = FinalCampaignSource.Count > 0 ? FinalCampaignSource.Max(x => x.CampaignSourceId) : 0;

            foreach (CampaignSource campaignSource in FinalCampaignSource)
            {
                var campaignRelationship = from p in container.CampaignRelationshipList
                    where p.Value.ParentCampaignId == campaignSource.CampaignId
                    select p;

                foreach (var item in campaignRelationship)
                {
                    if (item.Value.LinkParentSources && !item.Value.IsDeleted)
                    {
                        CampaignSource campaignSourceToAdd = new CampaignSource()
                        {
                            CampaignSourceId = maxId + 1,
                            CampaignId = item.Value.CampaignId,
                            SourceId = sourceId,
                            IsDeleted = false,
                            IsEnabled = true,
                            BidMultiplier = campaignSource.BidMultiplier,
                        };

                        AdditionalCampaigns.Add(campaignSourceToAdd);

                        maxId++;
                    }
                }
            }

            AdditionalCampaigns.ForEach(FinalCampaignSource.Add);

            return FinalCampaignSource;
        }

        private List<ClientAdAccountStop> FilterClientAccountStops(
            Dictionary<int, ClientAdAccount> filteredAccounts, 
            DictionaryContainer _container)
        {
            var filteredClientAccountStops = new List<ClientAdAccountStop>();

             _container.ClientAccountStopList?
                .Where(s => filteredAccounts.ContainsKey(s.Key))
                .Select(s => s.Value)
                .ToList()
                .ForEach(s => filteredClientAccountStops.AddRange(s));

            return filteredClientAccountStops;
        }

        private Dictionary<int, ClientAdAccountParameter> FilterClientAccountParameters(
            Dictionary<int, ClientAdAccount> filteredAccounts, DictionaryContainer _container)
        {
            Dictionary<int, ClientAdAccountParameter> FilterClientAccountParameters = null;

            var filtered = from p in _container.ClientAdAccountParameterList
                where filteredAccounts.Any
                    (bc => bc.Value.ClientAdAccountId == p.Value.ClientAdAccountId)
                select p;
            FilterClientAccountParameters = filtered.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return FilterClientAccountParameters;
        }

        private Dictionary<int, ClientAdAccountDefaultParameter> FilterClientAccountDefaultParameters(
            DictionaryContainer _container)
        {
            Dictionary<int, ClientAdAccountDefaultParameter> FilterClientAccountParameters = null;

            var filtered = from p in _container.ClientAdAccountDefaultParameterList
                where ((bool) p.Value.IsEnabled)
                select p;

            FilterClientAccountParameters = filtered.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return FilterClientAccountParameters;
        }

        private Dictionary<int, CampaignStop> FilterCampaignStops(Dictionary<int, Campaign> filteredCampaigns,
            DictionaryContainer _container)
        {
            Dictionary<int, CampaignStop> FilteredCampaignStops = null;

            var filtered = from p in _container.CampaignStopList
                where filteredCampaigns.Any
                    (bc => bc.Value.CampaignId == p.Value.CampaignId)
                select p;

            FilteredCampaignStops = filtered.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return FilteredCampaignStops;
        }

        private Dictionary<int, AdGroup> FilterAdGroups(Dictionary<int, Campaign> CampaignList,
            DictionaryContainer _container)
        {
            Dictionary<int, AdGroup> FilteredAdGroups = null;
            var filtered = from p in _container.AdGroupList
                where CampaignList.Any
                    (bc => bc.Value.CampaignId == p.Value.CampaignId && bc.Value.IsEnabled && !bc.Value.IsDeleted)
                select p;

            FilteredAdGroups = filtered.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return FilteredAdGroups;
        }

        private Dictionary<string, SlimAd> FilterAdsByCampaing(int sourceId, Dictionary<int, Campaign> CampaignList,
            DictionaryContainer _container, Dictionary<int,AdGroup> adGroupAdList)
        {

            var filteredAds = _container.SlimAdsDictionary?.Values
                .Where(ad => adGroupAdList.ContainsKey(ad.AdGroupId) && ad.SourceId == sourceId)
                .ToDictionary(ad => ad.Id, ad => new SlimAd()
                                {
                                    AdId = ad.AdId,
                                    CampaignId = ad.CampaignId,
                                    AdGroupId = ad.AdGroupId,
                                    ClientAdAccountId = ad.ClientAdAccountId,
                                    SourceBid = ad.SourceBid,
                                    SourceId = ad.SourceId
                                });

            return filteredAds ?? new Dictionary<string, SlimAd>();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_dataManager != null)
                    {
                        
                    }
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    internal enum Override
    {
        TargetingRule,
        OptimizationRule,
        BidingVariable
    }
}