using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.RuleEngine.CustomRuleEngine.Operators
{
    /*
     *  Operator			DataType	HardCodedRange/Value	Range/Values				Filters using this
     *  Is					String		Yes						string values, Multiple		Zip,LeadDeliveredTo, Tracking Tags
     *      													, user provided, 			
	 *	        												, comma separated
     */
    public static class IsStringOperator
    {
        internal static RuleEngineResult Evaluate(Dictionary<string, string> sourceValues, QueryBuilderFilterRule rule)
        {
            var result = new RuleEngineResult(rule);

            if (sourceValues.TryGetValue(rule.Id.ToString(), out string value))
            {
                if (rule.Value != null && rule.Value.Length > 0 && !string.IsNullOrEmpty(rule.Value[0]))
                {
                    var possibleValues = rule.Value[0].Trim('"').Split(",");

                    if (possibleValues.Any<string>(x=>x.Equals(value,StringComparison.InvariantCultureIgnoreCase)))
                    {
                        result.Pass = true;
                        result.ReasonDidntPass = String.Empty;
                    }
                    else
                    {
                        result.Pass = false;
                        result.ReasonDidntPass = RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"{value} is not one of {string.Join(",", rule.Value)}");
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
