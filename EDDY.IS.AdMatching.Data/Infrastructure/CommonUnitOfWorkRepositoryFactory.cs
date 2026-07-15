using EDDY.IS.AdMatching.Data.Context;
using EDDY.IS.AdMatching.Data.Repositories;
using EDDY.IS.AdMatching.Repositories.Interfaces;

namespace EDDY.IS.AdMatching.Data.Infrastructure
{
    /// <summary>
    /// CommonUnitOfWorkRepositoryFactory factory implements Unit of Work Design Pattern to share [APP]Context across all utilized Repositories
    /// </summary>
    public class CommonUnitOfWorkRepositoryFactory: ICommonUnitOfWorkRepositoryFactory
    {
        private GlassPanelContext _context;
        private ICampaignRepository _campaignRepository { get; set; }
        private IAdGroupRepository _adGroupRepository { get; set; }
        private IAdGroupAdRepository _adGroupAdRepository { get; set; }
        private IClientAdAccountRepository _clientAdAccountRepository { get; set; }
        private ICampaignSourceRepository _campaignSourceRepository { get; set; }
        private ICampaignScheduleRepository _campaignScheduleRepository { get; set; }
        private IScheduleOptionRepository _scheduleOptionRepository { get; set; }
        private ICampaignStopRepository _campaignStopRepository { get; set; }
        private IClientAccountStopRepository _clientAccountStopRepository { get; set; }
        private IAdsAMSRepository _adsAMSRepository { get; set; }
        private ISlimAdsAMSRepository _slimAdsAMSRepository { get; set; }
        private IClientAdAccountDefaultParameterRepository _clientAdAccountDefaultParameterRepository { get; set; }
        private ITargetingRuleRepository _targetingRuleRepository { get; set; }
        private IClientAdAccountParameterRepository _clientAdAccountParameterRepository { get; set; }
        private ICampaignRelationshipRepository _campaignRelationshipRepository { get; set; }
        private ITimeZoneRepository _timeZoneRepository { get; set; }
        private IStateTimeZoneRepository _stateTimeZoneRepository { get; set; }
        private ISourceProductTypeRepository _sourceProductTypeRepository { get; set;}
        private IClientAdAccountBudgetRepository _clientAdAccountBudgetRepository { get; set; }
        private ISourceByCampaignRepository _sourceByCampaignRepository { get; set; }
        private IAdRepository _adRepository { get; set; }
        

        public CommonUnitOfWorkRepositoryFactory(GlassPanelContext context) {
            this._context = context;
        }

        public ISourceByCampaignRepository SourceByCampaignRepository
        {
            get
            {
                if (_sourceByCampaignRepository == null)
                {
                    _sourceByCampaignRepository = new SourceByCampaignRepository(_context);
                }
                return _sourceByCampaignRepository;
            }
        }

        public ICampaignRepository CampaignRepository
        {
            get {
                if (_campaignRepository == null) {
                    _campaignRepository = new CampaignRepository(_context);
                }
                return _campaignRepository;
            }
        }

        public IAdGroupRepository AdGroupRepository {
            get
            {
                if (_adGroupRepository == null)
                {
                    _adGroupRepository = new AdGroupRepository(_context);
                }
                return _adGroupRepository;
            }
        }

        public IAdGroupAdRepository AdGroupAdRepository {
            get
            {
                if (_adGroupAdRepository == null)
                {
                    _adGroupAdRepository = new AdGroupAdRepository(_context);
                }
                return _adGroupAdRepository;
            }
        }

        
        public IClientAdAccountRepository ClientAdAccountRepository {
            get
            {
                if (_clientAdAccountRepository == null)
                {
                    _clientAdAccountRepository = new ClientAdAccountRepository(_context);
                }
                return _clientAdAccountRepository;
            }
        }



        public ICampaignSourceRepository CampaignSourceRepository {

            get
            {
                if (_campaignSourceRepository == null)
                {
                    _campaignSourceRepository = new CampaignSourceRepository(_context);
                }
                return _campaignSourceRepository;
            }

        }

        public ICampaignScheduleRepository CampaignScheduleRepository
        {

            get
            {
                if (_campaignScheduleRepository == null)
                {
                    _campaignScheduleRepository = new CampaignScheduleRepository(_context);
                }
                return _campaignScheduleRepository;
            }

        }

        public IScheduleOptionRepository ScheduleOptionRepository
        {

            get
            {
                if (_scheduleOptionRepository == null)
                {
                    _scheduleOptionRepository = new ScheduleOptionRepository(_context);
                }
                return _scheduleOptionRepository;
            }

        }

        public ICampaignStopRepository CampaignStopRepository
        {

            get
            {
                if (_campaignStopRepository == null)
                {
                    _campaignStopRepository = new CampaignStopRepository(_context);
                }
                return _campaignStopRepository;
            }

        }

        public IClientAccountStopRepository ClientAccountStopRepository
        {

            get
            {
                if (_clientAccountStopRepository == null)
                {
                    _clientAccountStopRepository = new AccountStopRepository(_context);
                }
                return _clientAccountStopRepository;
            }

        }

        public IAdsAMSRepository AdsAMSRepository
        {
            get
            {
                if(_adsAMSRepository == null)
                {
                    _adsAMSRepository = new AdsAMSRepository(_context);
                }
                return _adsAMSRepository;
            }
        }

        public ISlimAdsAMSRepository SlimAdsAMSRepository
        {
            get
            {
                if (_slimAdsAMSRepository == null)
                {
                    _slimAdsAMSRepository = new SlimAdsAMSRepository(_context);
                }
                return _slimAdsAMSRepository;
            }
        }

        public ITargetingRuleRepository TargetingRuleRepository
        {
            get
            {
                if(_targetingRuleRepository == null)
                {
                    _targetingRuleRepository = new TargetingRuleRepository(_context);
                }
                return _targetingRuleRepository;
            }
        }
        
        public IClientAdAccountDefaultParameterRepository ClientAdAccountDefaultParameterRepository
        {
            get
            {
                if(_clientAdAccountDefaultParameterRepository == null)
                {
                    _clientAdAccountDefaultParameterRepository = new ClientAdAccountDefaultParameterRepository(_context);
                }
                return _clientAdAccountDefaultParameterRepository;
            }
        }

        public IClientAdAccountParameterRepository ClientAdAccountParameterRepository
        {
            get
            {
                if (_clientAdAccountParameterRepository == null)
                {
                    _clientAdAccountParameterRepository = new ClientAdAccountParameterRepository(_context);
                }
                return _clientAdAccountParameterRepository;
            }
        }

        public ICampaignRelationshipRepository CampaignRelationshipRepository
        {
            get
            {
                if(_campaignRelationshipRepository == null)
                {
                    _campaignRelationshipRepository = new CampaignRelationshipRepository(_context);
                }
                return _campaignRelationshipRepository;
            }
        }

        public ITimeZoneRepository TimeZoneRepository
        {
            get
            {
                if(_timeZoneRepository == null)
                {
                    _timeZoneRepository = new TimeZoneRepository(_context);
                }
                return _timeZoneRepository;
            }
        }

        public IStateTimeZoneRepository StateTimeZoneRepository
        {
            get
            {
                if (_stateTimeZoneRepository == null)
                {
                    _stateTimeZoneRepository = new StateTimeZoneRepository(_context);
                }
                return _stateTimeZoneRepository;
            }
        }

        public ISourceProductTypeRepository SourceProductTypeRepository
        {
            get
            {
                if(_sourceProductTypeRepository == null)
                {
                    _sourceProductTypeRepository = new SourceProductTypeRepository(_context);
                }
                return _sourceProductTypeRepository;
            }
        }

        public IClientAdAccountBudgetRepository ClientAdAccountBudgetRepository
        {
            get
            {
                if(_clientAdAccountBudgetRepository == null)
                {
                    _clientAdAccountBudgetRepository = new ClientAdAccountBudgetRepository(_context);
                }
                return _clientAdAccountBudgetRepository;
            }
        }

        public IAdRepository AdRepository
        {
            get
            {
                if (_adRepository == null)
                {
                    _adRepository = new AdRepository(_context);
                }
                return _adRepository;
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /*
        public void Save()
        {
            _context.SaveChanges();   
        }*/
    }
}
