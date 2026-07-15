using EDDY.IS.AdMatching.Entities;
using EDDY.IS.Common.Dto.RuleEngine;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EDDY.IS.AdMatching.Data.Extensions;

public static class TargetingRuleExtensions
{
    public static QueryBuilderFilterRule CreateAndAssignRuleAsQueryBuilderFilterRule<TEntity>(this TargetingRule targetingRule, ILogger<TEntity> logger)
    {
        if (targetingRule.RuleAsQueryBuilderFilterRule == null)
        {
            targetingRule.RuleAsQueryBuilderFilterRule = new QueryBuilderFilterRule();
            try
            {
                targetingRule.RuleAsQueryBuilderFilterRule =
                    JsonConvert.DeserializeObject<QueryBuilderFilterRule>(targetingRule.RuleJson);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error trying to deserialize targeting rule id {targetingRule.TargetingRuleId}", ex);
            }
        }

        return targetingRule.RuleAsQueryBuilderFilterRule;
    }

    public static bool IsEmptyRule(this TargetingRule targetingRule)
    {
        if (targetingRule.RuleAsQueryBuilderFilterRule == null
            || targetingRule.RuleAsQueryBuilderFilterRule.Rules == null
            || targetingRule.RuleAsQueryBuilderFilterRule.Rules.Count == 0)
        {
            return true;
        }

        return false;
    }
}
