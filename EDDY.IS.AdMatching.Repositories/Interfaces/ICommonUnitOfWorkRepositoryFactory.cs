
namespace EDDY.IS.AdMatching.Repositories.Interfaces
{
    /// <summary>
    /// ICommonUnitOfWorkRepositoryFactory Unit of Work Design Patterh with shareable [APP]Context to be re-used by different Repositories
    /// </summary>
    public interface ICommonUnitOfWorkRepositoryFactory: IDisposable
    {
        ICampaignRepository CampaignRepository { get; }
        IAdGroupRepository AdGroupRepository { get; }
        IAdGroupAdRepository AdGroupAdRepository { get; }
        IClientAdAccountRepository ClientAdAccountRepository { get; }
        ICampaignSourceRepository CampaignSourceRepository { get; }
        ICampaignScheduleRepository CampaignScheduleRepository { get; }

        IScheduleOptionRepository ScheduleOptionRepository { get; }
        IClientAccountStopRepository ClientAccountStopRepository { get; }
        ICampaignStopRepository CampaignStopRepository { get; }
        IAdsAMSRepository AdsAMSRepository { get; }
        ISlimAdsAMSRepository SlimAdsAMSRepository { get; }
        IClientAdAccountDefaultParameterRepository ClientAdAccountDefaultParameterRepository { get; }
        IClientAdAccountParameterRepository ClientAdAccountParameterRepository { get; }
        ICampaignRelationshipRepository CampaignRelationshipRepository { get; }
        ITargetingRuleRepository TargetingRuleRepository { get; }
        ITimeZoneRepository TimeZoneRepository { get; }
        IStateTimeZoneRepository StateTimeZoneRepository { get; }
        ISourceProductTypeRepository SourceProductTypeRepository { get; }
        IClientAdAccountBudgetRepository ClientAdAccountBudgetRepository { get; }
        //void Save();
        ISourceByCampaignRepository SourceByCampaignRepository { get; }
        IAdRepository AdRepository { get; }
    }
}
