using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.RuleEngine.CustomRuleEngine.Operators
{
    /*
     *  Operator			DataType	HardCodedRange/Value	Range/Values				Filters using this
     *  IsNot       		String		Yes						string values, Multiple		CollegeCredits, EducationLevel, Gender, HighSchoolGPA, Income, MilitaryAffiliation, MilitaryBranch, PlannedStart, RegisteredNurse, TeachingCert, USCitizen, MarketingUnits, SubChannels, Country, State, AreaOfStudy, Subject, DegreeLevel, LearningPreference, LeadDeliveredTo, TrackingTags, Browser, BrowserPlatform, ConnectionType, DeviceType, ConnectionSource
     */
    public static class IsNotOneOfStringOperator
    {
        internal static RuleEngineResult Evaluate(Dictionary<string, string> sourceValues, QueryBuilderFilterRule rule)
        {
            var result = new RuleEngineResult(rule);

            if (sourceValues.TryGetValue(rule.Id.ToString(), out string value))
            {
                if (rule.Value != null && rule.Value.Length > 0)
                {

                    if (!rule.Value.Any<string>(x => x.Equals(value, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        result.Pass = true;
                        result.ReasonDidntPass = String.Empty;
                    }
                    else
                    {
                        result.Pass = false;
                        result.ReasonDidntPass = RuleEngineResult.GetReasonDidntPassFromSourceValuesRule(sourceValues, rule, $"{value} is one of {string.Join(",", rule.Value)}");
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
