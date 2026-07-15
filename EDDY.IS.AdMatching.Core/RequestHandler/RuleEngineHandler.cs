using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;
using EDDY.IS.RuleEngine.CustomRuleEngine;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.RequestHandler
{
    public class RuleEngineHandler : IChainHandler<AdMatchingModel>
    {
        private readonly IRuleEngine _ruleEngine;
        private readonly DebugLogger _debugLogger;
        private readonly ILogger<RuleEngineHandler> _logger;
        private readonly PerformanceLogger _performanceLog;
        public IChainHandler<AdMatchingModel> Next { get; }

        public RuleEngineHandler(
            IChainHandler<AdMatchingModel> next, 
            IRuleEngine ruleEngine, 
            DebugLogger debugLogger, 
            ILogger<RuleEngineHandler> logger, 
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
               
            FilterCampaignTargetingRules(model);
            FilterAdGroupTargetingRules(model);
             
            _performanceLog.LogPerformanceDetail(model.SearchGuid, 1, "RuleEngineHandler", startTime);

            if (Next is not null)
                await Next.Handle(model);
        }

        private void FilterAdGroupTargetingRules(AdMatchingModel model)
        {
            //process the rules for the AdGroup, that should include both the targeting rules AND the optimization rules
            foreach (var targetingRule in model.Filtered.TargetingRulesFiltered
                         .Where(tr => tr.AdGroupId != null))
            {
                var res = _ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(model.Parameters,
                    targetingRule.RuleAsQueryBuilderFilterRule);

                if (!res.Pass)
                    foreach (var adToRemove in model?.MainDictionaryEvaluated.SlimAdsDictionary
                                 .Where(a=>a.Value.AdGroupId == targetingRule.AdGroupId))
                        model?.MainDictionaryEvaluated.SlimAdsDictionary.Remove(adToRemove.Key);
            }
        }

        private void FilterCampaignTargetingRules(AdMatchingModel model)
        {
            var campaignIdsToRemove = new List<int>();
            
            //process the rules for the campaign, that should include both the targeting rules AND the optimization rules
            var targetingRulesToEvaluate = model.Filtered.TargetingRulesFiltered
                .Where(tr => tr.CampaignId != null && !tr.IsDynamicBid);

            foreach (var targetingRule in targetingRulesToEvaluate)
            {
                var res = _ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(model.Parameters,
                    targetingRule.RuleAsQueryBuilderFilterRule);

                if (!res.Pass)
                {
                    campaignIdsToRemove.Add(targetingRule.CampaignId.Value);
                    
                    foreach (var adToRemove in model?.MainDictionaryEvaluated.SlimAdsDictionary
                                 .Where(a=>a.Value.CampaignId == targetingRule.CampaignId))
                        model?.MainDictionaryEvaluated.SlimAdsDictionary.Remove(adToRemove.Key);
                }
            }
            
            foreach (var campaignIdToRemove in campaignIdsToRemove)
                model?.MainDictionaryEvaluated?.CampaignsList?.Remove(campaignIdToRemove);
            
        }
    }
}