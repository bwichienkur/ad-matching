
using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.RuleEngine.CustomRuleEngine
{
    public interface IRuleEngine
    {
        RuleEngineResult EvaluateRulesForDictionaryAndQueryBuilderFilterRule(Dictionary<string, string> sourceValues, QueryBuilderFilterRule queryBuilderFilterRule);
    }
}
