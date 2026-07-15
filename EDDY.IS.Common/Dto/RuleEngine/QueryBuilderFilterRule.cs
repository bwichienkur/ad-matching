using EDDY.IS.Common.ConstantsAndEnums;
using Type = EDDY.IS.Common.ConstantsAndEnums.Type;

namespace EDDY.IS.Common.Dto.RuleEngine
{
    public class QueryBuilderFilterRule : IFilterRule
    {
        public override string ToString()
        {
            if (this.Condition is not null && this.Operator is null)
            {
                return $"Condition = {this.Condition}, Id={this.Id}, Value={this.GetValuesAsString()}, Operator={this.Operator}";
            }
            else if (this.Operator is not null && this.Condition is null)
            {
                if (this.Id == IdOrField.AcceptMissingKey)
                {
                    return $"{this.Id} {this.Field}";
                }
                else
                {
                    return $"{this.Id} {this.Operator} {this.GetValuesAsString()}";
                }
            }
            else
            {
                return $"Condition = {this.Condition}, Id={this.Id}, Value={this.GetValuesAsString()}, Operator={this.Operator}";
            }
        }

        public string GetValuesAsString()
        {
            if (this.Value is not null)
            {
                return $"({string.Join(", ", this.Value)})";
            }
            else return string.Empty;
        }

        //
        // Summary:
        //     Condition - acceptable values are "and" and "or".
        //
        // Value:
        //     The condition.
        public Condition? Condition
        {
            get;
            set;
        } = null;

        //
        // Summary:
        //     The name of the field that the filter applies to.
        //
        // Value:
        //     The field.
        public IdOrField? Field
        {
            get;
            set;
        } = null;

        //
        // Summary:
        //     Gets or sets the identifier.
        //
        // Value:
        //     The identifier.
        public IdOrField? Id
        {
            get;
            set;
        } = null;

        //
        // Summary:
        //     Gets or sets the input.
        //
        // Value:
        //     The input.
        public Input? Input
        {
            get;
            set;
        } = null;

        //
        // Summary:
        //     Gets or sets the operator.
        //
        // Value:
        //     The operator.
        public Operator? Operator
        {
            get;
            set;
        } = null;

        //
        // Summary:
        //     Gets or sets nested filter rules.
        //
        // Value:
        //     The rules.
        public List<QueryBuilderFilterRule> Rules
        {
            get;
            set;
        } = new List<QueryBuilderFilterRule>();

        //
        // Summary:
        //     Gets or sets the type. Supported values are "integer", "double", "string", "date",
        //     "datetime", and "boolean".
        //
        // Value:
        //     The type.
        public Type? Type
        {
            get;
            set;
        } = null;

        //
        // Summary:
        //     Gets or sets the value of the filter.
        //
        // Value:
        //     The value.
        public string[] Value
        {
            get;
            set;
        }

        public Data Data { get; set; }

        IEnumerable<IFilterRule> IFilterRule.Rules => Rules;

        object IFilterRule.Value => Value;
    }

    //
    // Summary:
    //     This interface is used to define a hierarchical filter for a given collection.
    public interface IFilterRule
    {
        //
        // Summary:
        //     Condition - acceptable values are "and" and "or".
        //
        // Value:
        //     The condition.
        Condition? Condition
        {
            get;
        }

        //
        // Summary:
        //     The name of the field that the filter applies to.
        //
        // Value:
        //     The field.
        IdOrField? Field
        {
            get;
        }

        //
        // Summary:
        //     Gets or sets the identifier.
        //
        // Value:
        //     The identifier.
        IdOrField? Id
        {
            get;
        }

        //
        // Summary:
        //     Gets or sets the input.
        //
        // Value:
        //     The input.
        Input? Input
        {
            get;
        }

        //
        // Summary:
        //     Gets or sets the operator.
        //
        // Value:
        //     The operator.
        Operator? Operator
        {
            get;
        }

        //
        // Summary:
        //     Gets or sets nested filter rules.
        //
        // Value:
        //     The rules.
        IEnumerable<IFilterRule> Rules
        {
            get;
        }

        //
        // Summary:
        //     Gets or sets the type. Supported values are "integer", "double", "string", "date",
        //     "datetime", and "boolean".
        //
        // Value:
        //     The type.
        Type? Type
        {
            get;
        }

        //
        // Summary:
        //     Gets or sets the value of the filter.
        //
        // Value:
        //     The value.
        object Value
        {
            get;
        }

        Data Data { get; set; }

    }

    public class Data
    {
        public bool ForceEnd { get; set; }
    }

    public class DataString
    {
        public string ForceEnd { get; set; }
    }
}
