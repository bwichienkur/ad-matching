using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.RuleEngine.CustomRuleEngine.Operators
{
    public static class IsMultiStringOperator
    {
        internal static RuleEngineResult Evaluate(Dictionary<string, string> sourceValues, QueryBuilderFilterRule rule)
        {
            var result = new RuleEngineResult(rule);

            if (sourceValues.TryGetValue(rule.Id.ToString(), out string value))
            {
                if (rule.Value != null && rule.Value.Length > 0 && !string.IsNullOrEmpty(rule.Value[0]))
                {
                    foreach (var splitedValue in value.Split('|'))
                    {
                        var possibleValues = rule.Value[0].Trim('"').Split(",");

                        if (possibleValues.Any<string>(x => x.Equals(splitedValue, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            result.Pass = true;
                            result.ReasonDidntPass = String.Empty;
                            return result;
                        }
                        else
                        {
                            result.Pass = false;
                            result.ReasonDidntPass = RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"{value} is not one of {string.Join(",", rule.Value)}");
                        }
                    }
                }
                else
                {
                    result.Pass = false;
                    result.ReasonDidntPass = RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"The rule does not provide possible values");
                }
            }
            else
            {
                result.Pass = false;
                result.ReasonDidntPass = RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"{rule.Id.ToString()} not provided");
            }

            return result;
        }
    }
}
