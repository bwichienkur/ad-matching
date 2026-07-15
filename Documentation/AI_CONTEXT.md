# AI Knowledge Base — EDDY.IS.AdMatching

> Condensed context for AI agents working on this codebase. Read this first, then drill into linked docs.

## One-Line Summary

Real-time gRPC ad matching service for education lead-gen: loads GlassPanel SQL data into Redis/memory cache, filters/ranks ads via a 10-step handler chain and JSON rule engine, with optional WCF Matching Engine path for directory listings.

## Solution Layout

```
EDDY.IS.AdMatching.Service     ← gRPC host, Startup/DI, CacheBackgroundService
EDDY.IS.AdMatching.Domain      ← AdMatchingService, DTOs, IEngine/IDataManager interfaces
EDDY.IS.AdMatching.Core        ← Handlers, CommonEngine, CacheService, evaluators
EDDY.IS.AdMatching.Data        ← GlassPanelContext, CommonDataManager, repositories
EDDY.IS.AdMatching.Entities    ← EF entities + VW_* views
EDDY.IS.AdMatching.Repositories← Repository interfaces only
EDDY.IS.AdMatching.EAV         ← WCF Matching Engine client, EddyAdsListingService
EDDY.IS.RuleEngine             ← IRuleEngine + operators
EDDY.IS.Common                 ← QueryBuilderFilterRule, enums, RedisSettings
EDDY.IS.AdMatching.Caching     ← DEAD STUB — ignore, caching is in Core
Client/                        ← MVC 5 gRPC-Web sample (.NET 4.6.1)
```

## Entry Points

| Entry | File | Method |
|-------|------|--------|
| Main ad matching gRPC | `Service/Services/AdsService.cs` | `AdMatchingServiceInvoke` |
| EAV listing gRPC | `Service/Services/AdsService.cs` | `EddyAdsListingServiceInvoke` |
| Orchestrator | `Domain/Services/AdMatchingService.cs` | `GetAdsMatched` |
| DI registration | `Service/Startup.cs` | `ConfigureServices` |
| Background refresh | `Service/CacheBackgroundService.cs` | `ExecuteAsync` |

## Handler Chain Order (DO NOT REORDER without understanding dependencies)

```
StaticAdHandler → PreExcludeHandler → StopsHandler → CapsHandler →
RuleEngineHandler → SchedulingHandler → ResponseBuilderHandler →
DynamicBidVariablesHandler → AdSortingHandler → ParametersHandler
```

Registered in `Startup.cs:73-84`. Chain wired by `ChainConfigurator` in Core.

## Key Business Rules

1. **Cache-first**: Requests read pre-filtered dictionaries from Redis/memory; DB hit only on cache miss or background refresh.
2. **Shallow copy**: `CreateShallowCopy` prevents request mutations from corrupting shared cache.
3. **Static ads**: Valid `StaticAdGuid` → single-ad path via `StaticAdHandler`.
4. **Rule engine**: Targeting rules stored as JSON in `TargetingRule.RuleJson`, parsed to `QueryBuilderFilterRule` at load time.
5. **Dynamic bid rules**: Evaluated AFTER response building, only add CPC boost percentages.
6. **Ad sorting**: Dedup by client ad account, random among ties, cap at MaxAds.
7. **Parameters**: Final step substitutes URL macros; may execute C# scripts for dynamic params.
8. **No auth**: gRPC endpoints have no authentication middleware.

## Naming Conventions

- `Vw*` / `VW_*` — SQL Server views (read-only EF entities)
- `*Handler` — Chain of responsibility steps in `Core/RequestHandler/`
- `*Evaluator` / `*Engine` — Business logic helpers in `Core/Engines/`
- `I*Repository` — Interfaces in Repositories project; impl in Data project
- `*AMS` suffix — Ad Matching Service specific views (e.g., `VwAdsAm`, `SlimAd` → `VW_SlimAdsAMS`)
- `EAV` — Entity-Attribute-Value / directory listing integration path
- `GlassPanel` — Primary SQL Server database name

## Coding Standards (Observed)

- .NET 6.0, nullable enabled in newer projects
- EF Core NoTracking on GlassPanelContext
- New Relic `[Trace]`/`[Transaction]` on hot paths
- NLog for logging; ILogger bridged via UseNLog
- Options pattern for settings (`IOptions<T>`)
- Async/await on cache and service methods
- Legacy excluded files left in repo (`Compile Remove` in csproj)

## Patterns

| Pattern | Where |
|---------|-------|
| Chain of Responsibility | RequestHandler + ChainConfigurator |
| Repository | Data/Repositories |
| Unit of Work Factory | CommonUnitOfWorkRepositoryFactory |
| Strategy | RuleEngine operators |
| Options | Common/Settings, Domain/Settings |
| Adapter | EAV → WCF Matching Engine |
| Singleton cache | CacheService, IRuleEngine |

## Important Workflows

1. **Request**: gRPC → AdsService → AdMatchingService → CommonEngine (cache) → handler chain → response
2. **Cache warm**: CacheBackgroundService → CommonDataManager (full DB) → CommonEngine (filter per source) → Redis
3. **EAV**: gRPC → EddyAdsListingService → WCF GetInstitutions → map to vendor ads

## Database Relationships (Simplified)

```
ClientAdAccount 1──* Campaign 1──* AdGroup 1──* AdGroupAd *──1 Ad
Campaign *──* Source (via CampaignSource)
Campaign 1──* CampaignSchedule → ScheduleOption
Campaign 1──* CampaignStop
Campaign 1──* TargetingRule (campaign or ad-group scoped)
Campaign *──* Campaign (via CampaignRelationship parent/child)
VW_SlimAdsAMS — denormalized ad rows for matching (SourceId, CampaignId, AdGroupId, AdId, SourceBid)
VW_AdsAMS — full ad creative/metadata for response building
```

DbContext: `GlassPanelContext` — 93 DbSets, schema `AdServer`, no migrations.

## External Systems

| System | Config Key | Protocol |
|--------|------------|----------|
| GlassPanel SQL | `ConnectionStrings:GlassPanelConnection` | EF Core |
| EddyLogging SQL | `ConnectionStrings:EddyLogging` | NLog |
| Redis | `Redis:Server` | StackExchange.Redis |
| Matching Engine | `MatchingEngineService:Endpoint` | WCF/SOAP |
| Logo CDN | `EAVSetting:EddyLogoImagePathDomain` | HTTP URLs |

## Configuration Binding

```csharp
// Startup.cs pattern
services.Configure<RedisSettings>(Configuration.GetSection(RedisSettings.SectionName));
services.Configure<EAVSettings>(Configuration.GetSection(EAVSettings.SectionName));
// Section names: "Redis", "EAVSetting", "MatchingEngineService", "LoggingDebugInformation", "BaseUrlSubstitutions"
```

## DI Lifetimes (Quick Reference)

- **Singleton**: ICacheService, IRuleEngine, PerformanceLogger, IConnectionMultiplexer
- **Scoped**: IEngine, IAdMatchingService, IDataManager, all handlers, DbContext, WCF client
- **Hosted**: CacheBackgroundService

## Testing

- `EDDY.IS.AdMatching.Core.Tests` — MSTest, tests handlers + rule engine with mocks
- `EDDY.IS.Test.AdMatchingServiceTest` — NUnit, references Client project
- Mock helpers: `MockDataManager`, `MemoryCacheMock`, `RuleEngineMock`

## Known Issues / Landmines

1. `GrpcExceptionInterceptor` returns null on exception
2. Secrets in committed appsettings.json
3. Dockerfile uses .NET 5.0, project is net6.0
4. `AdStaticMatchingServiceInvoke` proto RPC not implemented
5. `EDDY.IS.AdMatching.Caching` is empty/unused
6. C# scripting in ParametersEvaluator — security review needed
7. No CI/CD in repo

## Proto Files

- `Service/Protos/AdsGreet.proto` — main Ads service
- `Service/Protos/greet.proto` — Greeter demo
- `Service/Protos/CatalogGreet.proto` — NOT implemented

## File Paths for Common Tasks

| Task | Start Here |
|------|------------|
| Add handler step | Create class in Core/RequestHandler, register in Startup.cs chain |
| Add rule operator | Core/RuleEngine/CustomRuleEngine/Operators, wire in RuleEngine.EvaluateOperator |
| Add DB entity | Entities project + GlassPanelContext DbSet + OnModelCreating |
| Change cache TTL | appsettings Redis:LifeSpanSeconds |
| Change refresh interval | appsettings Redis:ComputeIntervalSeconds |
| Add gRPC method | Proto file + AdsService override + domain service |

## Documentation Map

Full docs under `/Documentation/` — start with `ExecutiveSummary.md` and `Architecture.md`.
