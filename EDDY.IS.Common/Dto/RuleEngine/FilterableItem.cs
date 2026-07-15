
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

namespace EDDY.IS.Common.Dto.RuleEngine
{
    //    public class RuleEngine : IRuleEngine
    //    {
    //        public RuleEngineResult EvaluateRulesForDictionaryAndQueryBuilderFilterRule(Dictionary<string, string> sourceValues, QueryBuilderFilterRule queryBuilderFilterRule)
    //        {

    //            var valuesToFilter = sourceValues.ToArray()
    //                .Select(s => new FilterableItem
    //                { 
    //                    Key = s.Key,
    //                    Value = s.Value
    //                });


    //            var filteredDictionary = valuesToFilter.BuildQuery<FilterableItem>(queryBuilderFilterRule);

    //            return new RuleEngineResult
    //            {
    //                FilteredSet = filteredDictionary,
    //                Pass = filteredDictionary != null && filteredDictionary.Any(),
    //            };
    //        }

    //        public RuleEngineResult EvaluateRulesForDictionaryAndQueryBuilderFilterRule(Dictionary<string, string> sourceValues, string queryBuilderFilterRuleAsJson)
    //        {
    //            var queryFilter = JsonSerializer.Deserialize<QueryBuilderFilterRule>(queryBuilderFilterRuleAsJson);
    //            return EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryFilter);
    //        }
    //    }

    public class FilterableItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
