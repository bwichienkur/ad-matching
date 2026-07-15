using System.Collections.Generic;
using EDDY.IS.Common.ConstantsAndEnums;
using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.AdMatching.Core.Tests.Utilities
{
    public class QueryBuilderGenerator
    {
        private QueryBuilderFilterRule _queryBuilderFilterRule;

        public QueryBuilderGenerator()
        {
            _queryBuilderFilterRule = new QueryBuilderFilterRule()
            {
                Condition = Condition.AND,
                Rules = new List<QueryBuilderFilterRule>()
            };
        }

        public QueryBuilderFilterRule Build()
        {
            return _queryBuilderFilterRule;
        }

        public QueryBuilderGenerator AddIdOrFieldOperatorThan(IdOrField idOrField, Operator op, object operatorReferenceValue, bool allowNotPresent = true)
        {
            if (operatorReferenceValue is string || operatorReferenceValue is int)
                AddIntOperatorQueryBuilder(idOrField, operatorReferenceValue?.ToString(), op, allowNotPresent);
            else
                AddRangeOperatorQueryBuilder(idOrField, new List<string>((string[])operatorReferenceValue), op, allowNotPresent);
            return this;
        }

        public QueryBuilderGenerator AddIdOrBinaryFieldOperatorThan(IdOrField idOrField, Operator op, string operatorFirstReferenceValue, string operatorSecondReferenceValue, bool allowNotPresent = true)
        {
            var values = new List<string>() { operatorFirstReferenceValue, operatorSecondReferenceValue };
            AddRangeOperatorQueryBuilder(idOrField, values, op, allowNotPresent);
            return this;
        }

        public QueryBuilderGenerator AddRangeOperatorQueryBuilder(IdOrField idOrField, List<string> values, Operator op, bool allowNotPresent = true)
        {
            _queryBuilderFilterRule.Rules.Add(new QueryBuilderFilterRule()
            {
                Condition = Condition.OR,
                Rules = new List<QueryBuilderFilterRule>()
                {
                    new QueryBuilderFilterRule()
                    {
                        Id = idOrField,
                        Field = idOrField,
                        Type = Type.String,
                        Input = Input.Number,
                        Operator = op,
                        Value = values.ToArray(),
                    },
                    GetAcceptMissingKeyFilterRuleWithValue(allowNotPresent)
                }
            });
            return this;
        }


        public QueryBuilderGenerator AddIntOperatorQueryBuilder(IdOrField idOrField, string value,Operator op, bool allowNotPresent = true)
        {
            var values = new List<string>() { value };
            AddRangeOperatorQueryBuilder(idOrField, values, op, allowNotPresent);
            return this;
        }

        private QueryBuilderFilterRule GetAcceptMissingKeyFilterRuleWithValue(bool allowNotPresent = true)
        {
            var res = new QueryBuilderFilterRule()
            {
                Id = IdOrField.AcceptMissingKey,
                Field = IdOrField.AcceptMissingKey,
                Type = Type.String,
                Input = Input.Checkbox,
                Operator = Operator.Is,
                Value = new List<string>().ToArray(),
                Data = new Common.Dto.RuleEngine.Data()
                {
                    ForceEnd = true,
                }
            };

            if (allowNotPresent)
            {
                res.Value = new List<string>()
                        {
                            Constants.IfThisValueIsPresentTheClickWillBeAccepted
                        }.ToArray();
            }

            return res;
        }
    }
}
