using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.RuleEngine.CustomRuleEngine.Operators
{
    /*
     *  Operator			            DataType	HardCodedRange/Value	Range/Values				Filters using this
     *  ContainsOneOfStringOperator		String		Yes						string values, Multiple		Url
     */
    public static class ContainsOneOfStringOperator
    {
        internal static RuleEngineResult Evaluate(Dictionary<string, string> sourceValues, QueryBuilderFilterRule rule)
        {
            var result = new RuleEngineResult(rule);
            
            if (sourceValues.TryGetValue(rule.Id.ToString(), out string valueFromParameters))
            {
                if (rule.Value != null && rule.Value.Length > 0 && !string.IsNullOrEmpty(valueFromParameters))
                {
                    foreach (var splitedValue in rule.Value[0].Split(','))
                    {
                        if (valueFromParameters.Contains(splitedValue, StringComparison.InvariantCultureIgnoreCase))
                        {
                            result.Pass = true;
                            result.ReasonDidntPass = String.Empty;
                            return result;
                        }
                        else
                        {
                            result.Pass = false;
                            result.ReasonDidntPass += RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(
                                sourceValues, rule,
                                $"{splitedValue} doesn't contains one of {string.Join(",", rule.Value)}{Environment.NewLine}");
                        }
                    }
                }
                else
                {
                    result.Pass = false;
                    result.ReasonDidntPass += RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"The rule does not provide possible values{Environment.NewLine}");
                }
            }
            else
            {
                result.Pass = false;
                result.ReasonDidntPass += RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"{rule.Id.ToString()} not provided{Environment.NewLine}");
            }

            return result;
        }


    }
}
