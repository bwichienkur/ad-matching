using EDDY.IS.AdMatching.Data.Extensions;
using EDDY.IS.AdMatching.Data.Repositories;
using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.AdMatching.Entities;
using EDDY.IS.AdMatching.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;

namespace EDDY.IS.AdMatching.Data
{
    /// <summary>
    /// CommonDataManager manager works with data in Redis Cache or Common Unit of Work Pattern Factory to build Dictionary Container and return this to
    /// upper layer in the application which is CommonEngine.
    /// </summary>
    public class CommonDataManager : IDataManager
    {
        private readonly ICommonUnitOfWorkRepositoryFactory _commonUnitOfWorkRepositoryFactory;
        private readonly ILogger<CommonDataManager> _logger;

        public CommonDataManager(
            ICommonUnitOfWorkRepositoryFactory commonUnitOfWorkRepositoryFactory,
            ILogger<CommonDataManager> logger
        )
        {
            _commonUnitOfWorkRepositoryFactory = commonUnitOfWorkRepositoryFactory;
            _logger = logger;
        }

        [Trace]
        public DictionaryContainer GetDictionaryContainer()
        {
            var dictionaryContainer = new DictionaryContainer();

            var SourcesByCampaigns = ((GenericReadOnlyRepository<VwSourceByCampaignAms>)_commonUnitOfWorkRepositoryFactory
                    .SourceByCampaignRepository)
                    .GetAll();

            dictionaryContainer.CampaignsBySource =
                SourcesByCampaigns.GroupBy(x => x.SourceId)
                .ToDictionary(s => s.Key, s => s.ToList());

            dictionaryContainer.CampaignSourceDictionary =
                ((GenericReadOnlyRepository<CampaignSource>) _commonUnitOfWorkRepositoryFactory
                    .CampaignSourceRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(cs => string.Concat(cs.CampaignId, ":", cs.SourceId), cs => cs);

            dictionaryContainer.AdGroupList =
                ((GenericReadOnlyRepository<AdGroup>) _commonUnitOfWorkRepositoryFactory
                    .AdGroupRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(s => s.AdGroupId, s => s);

            dictionaryContainer.AdGroupAdList =
                ((GenericReadOnlyRepository<AdGroupAd>) _commonUnitOfWorkRepositoryFactory
                    .AdGroupAdRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(s => s.AdGroupAdId, s => s);

            dictionaryContainer.CampaignsList =
                ((GenericReadOnlyRepository<Campaign>)_commonUnitOfWorkRepositoryFactory
                    .CampaignRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(s => s.CampaignId, s => s);

            dictionaryContainer.ClientAdAccounts =
                ((GenericReadOnlyRepository<ClientAdAccount>) _commonUnitOfWorkRepositoryFactory
                    .ClientAdAccountRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(s => s.ClientAdAccountId, s => s);

            dictionaryContainer.ClientAdAccountParameterList =
                ((GenericReadOnlyRepository<ClientAdAccountParameter>) _commonUnitOfWorkRepositoryFactory
                    .ClientAdAccountParameterRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(s => s.ClientAdAccountParametersId, s => s);

            dictionaryContainer.ClientAdAccountDefaultParameterList =
                ((GenericReadOnlyRepository<ClientAdAccountDefaultParameter>)_commonUnitOfWorkRepositoryFactory
                    .ClientAdAccountDefaultParameterRepository)
                .GetAll(x => x.IsEnabled)
                .ToDictionary(s => s.ClientAdAccountDefaultParametersId, s => s);

            var targetingRules =
               ((GenericReadOnlyRepository<TargetingRule>)_commonUnitOfWorkRepositoryFactory
                    .TargetingRuleRepository)
                        .GetAll(x => x.IsEnabled && !x.IsDeleted)
                        .ToList();

            targetingRules.ForEach(t => {
                t.CreateAndAssignRuleAsQueryBuilderFilterRule(_logger);
                if (!t.IsEmptyRule())
                    dictionaryContainer.TargetingRules.Add(t);
            });

            dictionaryContainer.CampaignSchedulerList =
                ((GenericReadOnlyRepository<CampaignSchedule>) _commonUnitOfWorkRepositoryFactory
                    .CampaignScheduleRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(s => s.CampaignScheduleId, s => s);

            dictionaryContainer.ScheduleOptionList =
                ((GenericReadOnlyRepository<ScheduleOption>) _commonUnitOfWorkRepositoryFactory
                    .ScheduleOptionRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(s => s.ScheduleOptionId, s => s);

            dictionaryContainer.ClientAccountStopList =
                ((GenericReadOnlyRepository<ClientAdAccountStop>) _commonUnitOfWorkRepositoryFactory
                    .ClientAccountStopRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .GroupBy(x => x.ClientAdAccountId)
                .ToDictionary(s => s.Key, s => s.ToList());


            dictionaryContainer.CampaignStopList =
                ((GenericReadOnlyRepository<CampaignStop>) _commonUnitOfWorkRepositoryFactory
                    .CampaignStopRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(s => s.CampaignStopId, s => s);

            dictionaryContainer.AdsDictionary =
                ((GenericReadOnlyRepository<VwAdsAm>)_commonUnitOfWorkRepositoryFactory
                    .AdsAMSRepository)
                .GetAll()
                .ToDictionary(s => s.Id, s => s);

            dictionaryContainer.SlimAdsDictionary =
                ((GenericReadOnlyRepository<SlimAd>) _commonUnitOfWorkRepositoryFactory
                    .SlimAdsAMSRepository)
                .GetAll()
                .ToDictionary(s => s.Id, s => s);

            dictionaryContainer.CampaignRelationshipList =
                ((GenericReadOnlyRepository<CampaignRelationship>) _commonUnitOfWorkRepositoryFactory
                    .CampaignRelationshipRepository)
                .GetAll(x => !x.IsDeleted)
                .ToDictionary(s => s.CampaignRelationshipId, s => s);

            dictionaryContainer.ClientAdAccountParameterList =
                ((GenericReadOnlyRepository<ClientAdAccountParameter>) _commonUnitOfWorkRepositoryFactory
                    .ClientAdAccountParameterRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(s => Convert.ToInt32(s.ClientAdAccountParametersId), s => s);

            dictionaryContainer.TimeZoneList =
                ((GenericReadOnlyRepository<Entities.TimeZone>) _commonUnitOfWorkRepositoryFactory.TimeZoneRepository)
                .GetAll(x => x.IsEnabled)
                .ToDictionary(s => Convert.ToInt32(s.TimeZoneId), s => s);

            dictionaryContainer.StateTimeZoneList =
                ((GenericReadOnlyRepository<StateTimeZone>)_commonUnitOfWorkRepositoryFactory.StateTimeZoneRepository)
                .GetAll()
                .ToDictionary(s => s.StateCode, s => s);

            dictionaryContainer.ProductTypesBySource =
                ((GenericReadOnlyRepository<SourceProductType>)_commonUnitOfWorkRepositoryFactory
                    .SourceProductTypeRepository)
                .GetAll(x => x.IsEnabled)
                .GroupBy(s => s.SourceId)
                .ToDictionary(s => Convert.ToInt32(s.Key), s => s.ToList());

            dictionaryContainer.ClientAdAccountBudgetList =
                ((GenericReadOnlyRepository<ClientAdAccountBudget>) _commonUnitOfWorkRepositoryFactory
                    .ClientAdAccountBudgetRepository)
                .GetAll(x => x.IsEnabled && !x.IsDeleted)
                .ToDictionary(s => Convert.ToInt32(s.ClientAdAccountBudgetId), s => s);

            return dictionaryContainer;
        }

        public Ad? GetStaticAd(Guid statidAdGuid) {
            return ((GenericReadOnlyRepository<Ad>)_commonUnitOfWorkRepositoryFactory
                    .AdRepository)
                .Get(x => x.AdGuid == statidAdGuid)
                .FirstOrDefault();
        }
    }

    public static class AdExtensions
    {
        public static string GetAdHash(this VwAdsAm ad)
        {
            return $"{ad.ClientAdAccountId}:{ad.CampaignId}:{ad.AdGroupId}:{ad.AdId}";
        }
    }
}