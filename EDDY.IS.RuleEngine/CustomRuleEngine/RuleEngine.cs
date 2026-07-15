using EDDY.IS.Common.ConstantsAndEnums;
using EDDY.IS.Common.Dto.RuleEngine;
using EDDY.IS.RuleEngine.CustomRuleEngine.Operators;

namespace EDDY.IS.RuleEngine.CustomRuleEngine
{
    public class RuleEngine : IRuleEngine
    {
        public RuleEngineResult EvaluateRulesForDictionaryAndQueryBuilderFilterRule(Dictionary<string, string> sourceValues, QueryBuilderFilterRule queryBuilderFilterRule)
        {
            var result = new RuleEngineResult(queryBuilderFilterRule);
            RuleEngineResult innerRuleResult = null;
            if (queryBuilderFilterRule.Condition == Condition.AND)
            { result.Pass = true; }

            foreach (var rule in queryBuilderFilterRule.Rules)
            {
                //This is an AND/OR rule operation and will have rules inside of it, we need to iterate
                //through them
                if (rule.Condition != null && rule.Operator == null)
                {
                    SetRuleToOrOperatorIfAndBetweenOptionAndAcceptMissingKey(rule);
                    innerRuleResult = EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, rule);
                }

                //This is a comparation, we need to operate it on the sourceValues input parameter
                if (rule.Condition == null && rule.Operator != null)
                {
                    SetIdFromOtherRuleIfRuleIsAcceptMissingKey(rule,queryBuilderFilterRule.Rules);
                    innerRuleResult = EvaluateOperator(sourceValues, rule);
                }

                if (innerRuleResult != null)
                {
                    switch (queryBuilderFilterRule.Condition)
                    {
                        case Condition.AND:
                            result.Pass = innerRuleResult.Pass && result.Pass;
                            result.ReasonDidntPass = innerRuleResult.ReasonDidntPass;
                            if (!result.Pass)
                            {
                                return result;
                            }
                            break;
                        case Condition.OR:
                            result.Pass = innerRuleResult.Pass || result.Pass;
                            if (result.Pass)
                            {
                                result.ReasonDidntPass = String.Empty;
                            }
                            else
                            {
                                result.ReasonDidntPass = $"{rule.ToString()} ({innerRuleResult.ReasonDidntPass}) --AND-- ({result.ReasonDidntPass})";
                            }
                            break;
                    }
                }
            }

            return result;
        }

        private void SetRuleToOrOperatorIfAndBetweenOptionAndAcceptMissingKey(QueryBuilderFilterRule rule)
        {
            if (
                rule.Condition == Condition.AND
                && rule.Rules.Any(r=>r.Id == IdOrField.AcceptMissingKey)
            )
            {
                rule.Condition = Condition.OR;
            }
        }


        private RuleEngineResult EvaluateOperator(Dictionary<string, string> sourceValues, QueryBuilderFilterRule rule)
        {
            var result = new RuleEngineResult(rule);

            switch ((rule.Operator, rule.Id))
            {
                case (Operator.Is, IdOrField.AreaOfStudy):
                case (Operator.Is, IdOrField.EducationLevel):
                case (Operator.Is, IdOrField.CollegeCredits):
                case (Operator.Is, IdOrField.Gender):
                case (Operator.Is, IdOrField.HighSchoolGPA):
                case (Operator.Is, IdOrField.Income):
                case (Operator.Is, IdOrField.MilitaryAffiliation):
                case (Operator.Is, IdOrField.MilitaryBranch):
                case (Operator.Is, IdOrField.PlannedStart):
                case (Operator.Is, IdOrField.RegisteredNurse):
                case (Operator.Is, IdOrField.TeachingCert):
                case (Operator.Is, IdOrField.USCitizen):
                case (Operator.Is, IdOrField.MarketingUnits):
                case (Operator.Is, IdOrField.SubChannels):
                case (Operator.Is, IdOrField.Country):
                case (Operator.Is, IdOrField.State):
                case (Operator.Is, IdOrField.Subject):
                case (Operator.Is, IdOrField.LearningPreference):
                case (Operator.Is, IdOrField.DegreeLevel):
                case (Operator.Is, IdOrField.Browser):
                case (Operator.Is, IdOrField.BrowserPlatform):
                case (Operator.Is, IdOrField.ConnectionType):
                case (Operator.Is, IdOrField.ConnectionSource):
                case (Operator.Is, IdOrField.SiteUrl):
                case (Operator.Is, IdOrField.DeviceType):
                // → SD-9340
                case (Operator.Is, IdOrField.Referrer):
                case (Operator.Is, IdOrField.Step):
                case (Operator.Is, IdOrField.WorkflowStep):
                case (Operator.Is, IdOrField.Specialties):
                case (Operator.Is, IdOrField.PaidStatusType):
                case (Operator.Is, IdOrField.Institution):
                case (Operator.Is, IdOrField.Placement):
                case (Operator.Is, IdOrField.Channel):
                    result = IsOneOfStringOperator.Evaluate(sourceValues, rule);
                    break;
                case (Operator.Is_Not, IdOrField.EducationLevel):
                case (Operator.Is_Not, IdOrField.AreaOfStudy):
                case (Operator.Is_Not, IdOrField.CollegeCredits):
                case (Operator.Is_Not, IdOrField.Income):
                case (Operator.Is_Not, IdOrField.Gender):
                case (Operator.Is_Not, IdOrField.HighSchoolGPA):
                case (Operator.Is_Not, IdOrField.MilitaryAffiliation):
                case (Operator.Is_Not, IdOrField.MilitaryBranch):
                case (Operator.Is_Not, IdOrField.PlannedStart):
                case (Operator.Is_Not, IdOrField.RegisteredNurse):
                case (Operator.Is_Not, IdOrField.TeachingCert):
                case (Operator.Is_Not, IdOrField.USCitizen):
                case (Operator.Is_Not, IdOrField.MarketingUnits):
                case (Operator.Is_Not, IdOrField.SubChannels):
                case (Operator.Is_Not, IdOrField.State):
                case (Operator.Is_Not, IdOrField.Country):
                case (Operator.Is_Not, IdOrField.Subject):
                case (Operator.Is_Not, IdOrField.LearningPreference):
                case (Operator.Is_Not, IdOrField.DegreeLevel):
                case (Operator.Is_Not, IdOrField.Browser):
                case (Operator.Is_Not, IdOrField.BrowserPlatform):
                case (Operator.Is_Not, IdOrField.ConnectionSource):
                case (Operator.Is_Not, IdOrField.ConnectionType):
                case (Operator.Is_Not, IdOrField.SiteUrl):
                case (Operator.Is_Not, IdOrField.DeviceType):
                // → SD-9340
                case (Operator.Is_Not, IdOrField.Referrer):
                case (Operator.Is_Not, IdOrField.Step):
                case (Operator.Is_Not, IdOrField.WorkflowStep):
                case (Operator.Is_Not, IdOrField.Specialties):
                case (Operator.Is_Not, IdOrField.PaidStatusType):
                case (Operator.Is_Not, IdOrField.Institution):
                case (Operator.Is_Not, IdOrField.Placement):
                case (Operator.Is_Not, IdOrField.Channel):
                    result = IsNotOneOfStringOperator.Evaluate(sourceValues, rule);
                    break;
                case (Operator.Is, IdOrField.Zip):
                case (Operator.Is, IdOrField.LeadDeliveredTo):
                case (Operator.Is, IdOrField.TrackingTags):
                case (Operator.Is, IdOrField.ThankYouExperience):
                    result = IsStringOperator.Evaluate(sourceValues, rule);
                    break;
                case (Operator.Is_Not, IdOrField.Zip):
                case (Operator.Is_Not, IdOrField.LeadDeliveredTo):
                case (Operator.Is_Not, IdOrField.TrackingTags):
                case (Operator.Is_Not, IdOrField.ThankYouExperience):
                    result = IsNotStringOperator.Evaluate(sourceValues, rule);
                    break;
                case (Operator.Not_In_Range, IdOrField.Age):
                case (Operator.Not_In_Range, IdOrField.HighSchoolGradYear):
                    result = NotInRangeNumberOperator.Evaluate(sourceValues, rule);
                    break;
                case (Operator.Range, IdOrField.Age):
                case (Operator.Range, IdOrField.HighSchoolGradYear):
                    result = RangeNumberOperator.Evaluate(sourceValues, rule);
                    break;
                case (Operator.Is_Greater_Than, IdOrField.Age):
                    result = IsGreaterThanNumberOperator.Evaluate(sourceValues, rule);
                    break;
                case (Operator.Is_Less_Than, IdOrField.Age):
                    result = IsLessThanNumberOperator.Evaluate(sourceValues, rule);
                    break;
                case (Operator.Contains_One_Of, IdOrField.SiteUrl):
                // → SD-9340
                case (Operator.Contains_One_Of, IdOrField.Referrer):
                    result = ContainsOneOfStringOperator.Evaluate(sourceValues, rule);
                    break;
                case (Operator.Not_Contains_One_Of, IdOrField.SiteUrl):
                // → SD-9340
                case (Operator.Not_Contains_One_Of, IdOrField.Referrer):
                    result = NotContainsOneOfStringOperator.Evaluate(sourceValues, rule);
                    break;
                // → SD-9340
                case (Operator.Is, IdOrField.LeadCreatedProduct):
                    result = IsMultiStringOperator.Evaluate(sourceValues, rule);
                    break;
                // → SD-9340
                case (Operator.Is_Not, IdOrField.LeadCreatedProduct):
                    result = IsNotMultiStringOperator.Evaluate(sourceValues, rule);
                    break;
                case (_,IdOrField.AcceptMissingKey):
                    result = AcceptMissingKeyOperator.Evaluate(sourceValues, rule);
                    break;
            }

            return result;
        }

        private void SetIdFromOtherRuleIfRuleIsAcceptMissingKey(QueryBuilderFilterRule rule, List<QueryBuilderFilterRule> allRules)
        {
            if (rule.Id == IdOrField.AcceptMissingKey)
            {
                var allRulesButAcceptMissingKey = allRules.FindAll(r => r.Id != IdOrField.AcceptMissingKey);
                if (allRulesButAcceptMissingKey.Any())
                {
                    rule.Field = allRulesButAcceptMissingKey.First().Field;
                }
            }
        }
    }
}
