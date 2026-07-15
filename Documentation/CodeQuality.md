# Code Quality Review

## Code Smells

| Smell | Location | Detail |
|-------|----------|--------|
| Dead code | Core: RulesEvaluator, DataManager | Excluded from compile |
| Dead project | AdMatching.Caching | Empty Class1 |
| Speculative generality | IGenericRepository | Interface with no implementation |
| Incomplete feature | CatalogGreet proto, AdStatic RPC | Defined but not implemented |
| Commented code | Program.cs warm-up, Startup CatalogService | Should remove or implement |
| Typo in messages | AdMatchingService:48 | "campaings" |
| Duplicate client csproj | Client/Client.csproj + EDDY.IS.Examples.ClientNet.csproj | Confusing |

## God Classes

| Class | Lines (approx) | Concern |
|-------|----------------|---------|
| GlassPanelContext | ~1930 | Auto-generated fluent config — acceptable |
| CommonDataManager | ~170 | Loads all data — candidate for split by domain |
| CommonEngine | Large | Multiple filter methods — could split by concern |
| RuleEngine.EvaluateOperator | ~120 switch | Could use registry pattern |

## Long Methods

| Method | File | Concern |
|--------|------|---------|
| GetDictionaryContainer | CommonDataManager | Repetitive repository load pattern |
| FilterDictionaryContainer | CommonEngine | Multiple filter steps inline |
| EvaluateOperator | RuleEngine | Large switch statement |

## Duplicated Logic

| Pattern | Occurrences |
|---------|-------------|
| `.GetAll(x => x.IsEnabled && !x.IsDeleted)` | CommonDataManager (10+ times) |
| Rule evaluation loop | RuleEngineHandler + DynamicBidVariablesHandler |
| Error response pattern | Both AdsService RPC methods |

**Recommendation:** Extract `GetEnabled<T>()` extension; shared rule evaluation helper.

## Dead Code

| Item | Status |
|------|--------|
| EDDY.IS.AdMatching.Caching | Unused |
| RulesEvaluator.cs | Compile removed |
| Core/Auxiliary/DataManager.cs | Compile removed |
| Core/Logging/CoreException.cs | Compile removed |
| ICampaignSpendRepository + 4 others | No implementation |
| Microsoft.AspNetCore.Authentication.Certificate | Package unused |

## Circular Dependencies

**None detected.** Dependency graph is acyclic (Domain → Entities → Common; Core → Data → Domain).

## Naming Issues

| Issue | Example |
|-------|---------|
| Misspelling | ChainResponsability (should be Responsibility) |
| Inconsistent casing | getAdMatchingRequest (proto lowercase) |
| Ambiguous name | CommonEngine (not obviously IEngine impl) |
| Typo in constraint | FK_ReportSchedule_ReportFrecuency |

## SOLID Violations

| Principle | Violation |
|-----------|-----------|
| Single Responsibility | CommonDataManager loads all entity types |
| Open/Closed | RuleEngine switch statement for operators |
| Liskov Substitution | N/A — minimal inheritance |
| Interface Segregation | ICommonUnitOfWorkRepositoryFactory exposes all repos |
| Dependency Inversion | Core depends on concrete Data project |

## DRY Violations

- Repository load boilerplate in CommonDataManager
- Proto-to-DTO mapping could use AutoMapper (not used)
- Performance logging try/finally duplicated in AdsService methods

## Large Interfaces

`ICommonUnitOfWorkRepositoryFactory` — 20+ repository properties. Could group by aggregate.

## Overengineering

- Expression tree chain configurator — elegant but hard to debug vs simple manual chain
- Separate Repositories project with mostly marker interfaces

## Underengineering

- No input validation layer
- No health checks
- No retry/circuit breaker for WCF
- No structured correlation IDs

## Recommendations

1. Extract repository loading into typed loaders (CampaignLoader, AdLoader, etc.)
2. Replace RuleEngine switch with operator registry dictionary
3. Delete dead code and empty project
4. Add FluentValidation for gRPC requests
5. Introduce AutoMapper or Mapster for proto mapping
