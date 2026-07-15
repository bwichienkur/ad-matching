using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.RuleEngine.CustomRuleEngine.Operators
{
    /*
     *  Operator			DataType	HardCodedRange/Value	Range/Values				Filters using this
     *  IsNotBetween(range)	Int 		No						int user selectable			Age, High School Grad Year,
     */
    public static class NotInRangeNumberOperator
    {
        internal static RuleEngineResult Evaluate(Dictionary<string, string> sourceValues, QueryBuilderFilterRule rule)
        {
            var result = new RuleEngineResult(rule);

            if (sourceValues.TryGetValue(rule.Id.ToString(), out string value))
            {
                //we have a reference value in the rule AND we have a value supplied in the sourceValues dictionary
                if (decimal.TryParse(value, out decimal decimalValue) 
                    //Do we have two values for the lower and upper range?
                    && rule.Value.Any() && rule.Value.Count() > 1
                    //Try to get the values for the upper and lower range
                    && decimal.TryParse(rule.Value.First(), out decimal lowerCompareValue) && decimal.TryParse(rule.Value.ToArray<object>()[1].ToString(), out decimal upperCompareValue))
                {
                    if (!(
                        decimal.Compare(decimalValue, lowerCompareValue) >= 0
                        && decimal.Compare(decimalValue, upperCompareValue) <= 0
                        ))
                    {
                        result.Pass = true;
                        result.ReasonDidntPass = String.Empty;
                    }
                    else
                    {
                        result.Pass = false;
                        result.ReasonDidntPass = RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"{rule.Id.ToString()}:{decimalValue} is between {lowerCompareValue} and {upperCompareValue}");
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
