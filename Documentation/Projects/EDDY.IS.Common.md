# EDDY.IS.Common

## Purpose

**Shared cross-cutting types** — rule engine DTOs, enums, constants, and settings classes used across multiple projects.

## Responsibilities

- QueryBuilder rule DTOs
- Targeting field enums (IdOrField, Operator, Condition, Input, Type)
- Business lookup constants (Income, AreaOfStudy, State, etc.)
- Settings classes (RedisSettings, BaseUrlSubstitutions, LoggingDebugInformation)

## Dependencies

None — leaf project.

## Important Classes

| Class/Enum | Purpose |
|------------|---------|
| QueryBuilderFilterRule | Rule tree node |
| RuleEngineResult | Evaluation result |
| IdOrField | 40+ targeting dimensions |
| Operator | Comparison operators |
| Condition | AND/OR |
| Constants | Lookup value enums |
| RedisSettings | Cache configuration |

## Configuration Sections

| Class | Section Name |
|-------|-------------|
| RedisSettings | "Redis" |
| BaseUrlSubstitutions | "BaseUrlSubstitutions" |
| LoggingDebugInformation | "LoggingDebugInformation" |

## Potential Improvements

- Split into Common.Dto and Common.Settings if project grows
- Add XML docs on IdOrField values
- Document mapping between IdOrField and VwRules* views

## Folder Structure

| Folder | Purpose |
|--------|---------|
| Dto/RuleEngine/ | QueryBuilderFilterRule, RuleEngineResult |
| ConstantsAndEnums/ | IdOrField, Operator, Constants |
| Settings/ | RedisSettings, etc. |
