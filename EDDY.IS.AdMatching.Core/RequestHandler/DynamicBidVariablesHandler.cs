using EDDY.IS.AdMatching.Core.Auxiliary;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;
using EDDY.IS.RuleEngine.CustomRuleEngine;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.RequestHandler
{
    public class DynamicBidVariablesHandler : IChainHandler<AdMatchingModel>
    {
        private readonly IRuleEngine _ruleEngine;
        private readonly ILogger<AdSortingHandler> _logger;
        private readonly DebugLogger _debugLogger;
        private readonly PerformanceLogger _performanceLog;
        public IChainHandler<AdMatchingModel> Next { get; }

        public DynamicBidVariablesHandler(
            IChainHandler<AdMatchingModel> next, 
            IRuleEngine ruleEngine, 
            DebugLogger debugLogger, 
            ILogger<AdSortingHandler> logger, 
            PerformanceLogger performanceLogger)
        {
            _logger = logger;
            _ruleEngine = ruleEngine;
            _debugLogger = debugLogger;
            Next = next;
            _performanceLog = performanceLogger;
        }

        [Trace]
        public async Task Handle(AdMatchingModel model)
        {
            DateTime startTime = DateTime.Now;
                
            ApplyDynamicRulesFromRulesEngine(model);

            _performanceLog.LogPerformanceDetail(model.SearchGuid, 1, "DynamicBidVariablesHandler", startTime);

            if (Next is not null)
                await Next.Handle(model);
        }

        private void ApplyDynamicRulesFromRulesEngine(AdMatchingModel model)
        {
            //process the rules for the AdGroup, that should include both the targeting rules AND the optimization rules
            if (model.Filtered.TargetingRulesFiltered != null)
            {
                foreach (var targetingRule in model.Filtered.TargetingRulesFiltered
                             .Where(tr => tr.IsDynamicBid
                                          //do this just for the campaigns of the final ads selected
                                          && model.FinalAdsList != null 
                                          && model.FinalAdsList.Exists(a => a.CampaignId == tr.CampaignId)
                                          && !tr.IsEmptyRule()
                             ))
                {
                    var res = _ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(model.Parameters,
                        targetingRule.RuleAsQueryBuilderFilterRule);

                    if (res.Pass)
                    {
                        foreach (var adsMatched in model.FinalAdsList.Where(a =>
                                     a.CampaignId == targetingRule.CampaignId 
                                     && targetingRule.IsEnabled
                                     && !targetingRule.IsDeleted
                                 ))
                        {
                            var DynamicBidValue = targetingRule.DynamicBoostPercent == null ? 0 : (decimal)targetingRule.DynamicBoostPercent;
                            adsMatched.DynamicBoostPercent.Add(DynamicBidValue); 
                        }
                    }
                }
            }
        }
    }
}