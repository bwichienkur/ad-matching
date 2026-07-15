using EDDY.IS.Common.ConstantsAndEnums;
using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.RuleEngine.CustomRuleEngine.Operators
{
    public static class AcceptMissingKeyOperator
    {
        internal static RuleEngineResult Evaluate(Dictionary<string, string> sourceValues, QueryBuilderFilterRule rule)
        {
            var result = new RuleEngineResult(rule);

            if (sourceValues.TryGetValue(rule.Field.ToString(), out string value) && !string.IsNullOrEmpty(value))
            {
                result.Pass = false;
                result.ReasonDidntPass = RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"{rule.Field} is present with value {value}");
            }
            else if (rule.Value.Any() && rule.Value.First().Equals(Constants.IfThisValueIsPresentTheClickWillBeAccepted))
            {
                result.Pass = true;
                result.ReasonDidntPass = String.Empty;
            }
            else
            {
                result.Pass = false;
                result.ReasonDidntPass = RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"{rule.Field} is not present and AcceptMissingKeyOperator was not set");
            }

            return result;
        }


    }
}
