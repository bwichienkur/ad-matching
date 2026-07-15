namespace EDDY.IS.Common.Dto.RuleEngine
{
    public class RuleEngineResult
    {
        public RuleEngineResult(QueryBuilderFilterRule rule)
        {
            this.ReasonDidntPass = GetReasonDidntPassFromSourceValuesRule(null, rule, "Not Evaluated Yet");
        }

        public bool Pass { get; set; }
        public string ReasonDidntPass { get; set; }

        public static string GetReasonDidntPassFromSourceValuesRule(Dictionary<string, string>? sourceValues, QueryBuilderFilterRule rule, string reason)
        {
            return $"Rule: {rule}, failed because: {reason}";
        }
    }
}