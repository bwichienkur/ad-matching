# EDDY.IS.AdMatching.Core

## Purpose

**Business logic layer** — handler chain, evaluation engines, caching service, and chain-of-responsibility DI wiring.

## Responsibilities

- 10 request handlers (chain of responsibility)
- CommonEngine (cache load/filter/pre-compute)
- Evaluators: Stops, Caps, Schedule, Parameters, AdSorting, ResponseBuilder
- CacheService (Redis + memory two-tier)
- ChainConfigurator DI extension
- Debug/performance logging helpers

## Dependencies

| Type | References |
|------|------------|
| Projects | Data, Domain, Repositories, RuleEngine |
| Key NuGet | StackExchange.Redis, GeoTimeZone, CSharp.Scripting, NLog |

## Important Classes

See [Services/HandlerChain.md](../Services/HandlerChain.md) and [Classes/ImportantClasses.md](../Classes/ImportantClasses.md).

## Excluded Files (Compile Remove)

| File | Reason |
|------|--------|
| Auxiliary/DataManager.cs | Legacy data loader |
| Engines/RulesEvaluator.cs | Legacy rule evaluator |
| Logging/CoreException.cs | Unused |

## Configuration

Consumes `RedisSettings`, `LoggingDebugInformation`, `BaseUrlSubstitutions` via IOptions.

## Potential Improvements

- Delete excluded legacy files
- Split CommonEngine into smaller filter services
- Replace operator switch with registry in RuleEngine (separate project)
- Move CacheService here is correct — delete empty Caching project

## Folder Structure

| Folder | Pattern | Key Classes |
|--------|---------|-------------|
| RequestHandler/ | Chain of Responsibility | 10 handlers |
| Engines/ | Evaluator/Builder | CommonEngine, *Evaluator, *Engine |
| ChainResponsability/ | DI wiring | ChainConfiguratorExtension |
| Services/ | Infrastructure | CacheService |
| Auxiliary/ | Helpers | TargetingRuleExtensions, CommonTimeZoneManager |
| Logging/ | Cross-cutting | DebugLogger, PerformanceLogger |
