using EDDY.IS.AdMatching.Entities;
using EDDY.IS.Common.ConstantsAndEnums;
using EDDY.IS.Common.Dto.RuleEngine;
using Newtonsoft.Json;
using System;

namespace EDDY.IS.AdMatching.Core.Auxiliary;

public static class TargetingRuleExtensions
{
    public static QueryBuilderFilterRule CreateAndAssignRuleAsQueryBuilderFilterRule(this TargetingRule targetingRule)
    {
        if (targetingRule.RuleAsQueryBuilderFilterRule == null)
        {
            try
            {
                targetingRule.RuleAsQueryBuilderFilterRule =
                    JsonConvert.DeserializeObject<QueryBuilderFilterRule>(targetingRule.RuleJson);
            }
            catch (Exception e)
            {
                targetingRule.RuleAsQueryBuilderFilterRule = new QueryBuilderFilterRule()
                {
                    Condition = Condition.AND
                };
                ////NLogLogger.Trace($"Error converting the rule: {targetingRule.RuleJson}", e);
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
