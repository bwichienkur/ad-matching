# EDDY.IS.RuleEngine

## Purpose

**JSON rule evaluation engine** — evaluates QueryBuilder-format targeting rules against visitor parameter dictionaries.

## Responsibilities

- `IRuleEngine` interface
- Recursive AND/OR tree evaluation
- 14 operator strategy classes
- AcceptMissingKey special handling

## Dependencies

| Type | References |
|------|------------|
| Projects | Common |
| NuGet | None beyond SDK |

## Important Classes

| Class | Purpose |
|-------|---------|
| RuleEngine | Main evaluator |
| IRuleEngine | Interface |
| QueryBuilderFilterRule | Rule tree DTO (in Common) |
| RuleEngineResult | Pass/Fail result |
| *Operator classes | Strategy implementations |

## Operators

| Operator Class | Handles |
|----------------|---------|
| IsOneOfStringOperator | Enum field Is |
| IsNotOneOfStringOperator | Enum field Is_Not |
| IsStringOperator | Zip, LeadDeliveredTo Is |
| IsNotStringOperator | Same fields Is_Not |
| RangeNumberOperator | Age, HighSchoolGradYear Range |
| NotInRangeNumberOperator | Not_In_Range |
| IsGreaterThanNumberOperator | Age > |
| IsLessThanNumberOperator | Age < |
| ContainsOneOfStringOperator | SiteUrl, Referrer |
| NotContainsOneOfStringOperator | Inverse |
| IsMultiStringOperator | LeadCreatedProduct Is |
| IsNotMultiStringOperator | LeadCreatedProduct Is_Not |
| AcceptMissingKeyOperator | Missing field acceptance |

**Gap:** `Operator.Equal` enum value not dispatched.

## Configuration

None — stateless singleton.

## Potential Improvements

- Replace switch with operator registry
- Implement Equal operator
- Add rule schema validation
- Unit test all IdOrField + Operator combinations

## Folder Structure

| Folder | Purpose |
|--------|---------|
| CustomRuleEngine/ | RuleEngine, IRuleEngine |
| CustomRuleEngine/Operators/ | Strategy classes |
