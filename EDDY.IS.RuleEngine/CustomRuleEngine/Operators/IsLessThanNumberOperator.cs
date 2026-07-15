using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.RuleEngine.CustomRuleEngine.Operators
{
    /*
     *  Operator			DataType	HardCodedRange/Value	Range/Values				Filters using this
     *  IsLessThan			Int			No						int user selectable			Age
     */
    public static class IsLessThanNumberOperator
    {
        internal static RuleEngineResult Evaluate(Dictionary<string, string> sourceValues, QueryBuilderFilterRule rule)
        {
            var result = new RuleEngineResult(rule);

            if (sourceValues.TryGetValue(rule.Id.ToString(), out string value))
            {
                //we have a reference value in the rule AND we have a value supplied in the sourceValues dictionary
                if (decimal.TryParse(value, out decimal decimalValue) && decimal.TryParse(rule.Value.First(), out decimal compareValue))
                {
                    if (decimal.Compare(decimalValue, compareValue) < 0)
                    {
                        result.Pass = true;
                        result.ReasonDidntPass = String.Empty;
                    }
                    else
                    {
                        result.Pass = false;
                        result.ReasonDidntPass = RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"{decimalValue} !> {compareValue}");
                    }
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
