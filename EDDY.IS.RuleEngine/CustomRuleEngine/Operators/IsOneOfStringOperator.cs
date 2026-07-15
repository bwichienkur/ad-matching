using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.RuleEngine.CustomRuleEngine.Operators
{
    /*
     *  Operator			DataType	HardCodedRange/Value	Range/Values				Filters using this
     *  Is					String		Yes						string values, Multiple		CollegeCredits, EducationLevel, Gender, HighSchoolGPA, Income, MilitaryAffiliation, MilitaryBranch, PlannedStart, RegisteredNurse, TeachingCert, USCitizen, MarketingUnits, SubChannels, Country, State, AreaOfStudy, Subject, DegreeLevel, LearningPreference, LeadDeliveredTo, TrackingTags, Browser, BrowserPlatform, ConnectionType, DeviceType, ConnectionSource
     */
    public static class IsOneOfStringOperator
    {
        internal static RuleEngineResult Evaluate(Dictionary<string, string> sourceValues, QueryBuilderFilterRule rule)
        {
            var result = new RuleEngineResult(rule);
            
            if (sourceValues.TryGetValue(rule.Id.ToString(), out string value))
            {
                if (rule.Value != null && rule.Value.Length > 0 && !string.IsNullOrEmpty(value))
                {
                    foreach (var splitedValue in value.Split('|'))
                    {
                        if (rule.Value.Any<string>(x => x.Equals(splitedValue, StringComparison.InvariantCultureIgnoreCase)))
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
                                $"{splitedValue} is not one of {string.Join(",", rule.Value)}{Environment.NewLine}");
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
