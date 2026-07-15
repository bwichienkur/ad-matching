# 1. Executive Summary

## What the Application Does

**EDDY.IS.AdMatching** (Ad Matching Service, AMS) is a real-time **advertising selection and ranking engine** for Education Dynamics' lead-generation platform. Given a visitor's context (source, demographics, geo, referrer, etc.), it returns the best-matching paid education ads from the **GlassPanel** ad server database.

A second capability, **Eddy Ads Listing**, delegates institution/program matching to a legacy **Matching Engine** WCF service and transforms results into vendor ad listings for directory-style placements.

**Evidence:** Orchestration in `AdMatchingService.GetAdsMatched` (`EDDY.IS.AdMatching.Domain/Services/AdMatchingService.cs:25-55`); gRPC surface in `AdsGreet.proto` (`EDDY.IS.AdMatching.Service/Protos/AdsGreet.proto:8-13`); EAV integration in `EddyAdsListingService` (`EDDY.IS.AdMatching.EAV/Services/EddyAdsListingService.cs`).

## Business Problem It Solves

Higher-education lead buyers run campaigns with complex targeting rules (state, degree level, area of study, schedule windows, budget caps, etc.). Publisher sites need sub-second ad selection that:

1. Respects campaign budgets, stops, and schedules
2. Evaluates JSON-based targeting rules against visitor parameters
3. Ranks ads by CPC with bid multipliers and dynamic bid boosts
4. Substitutes tracking macros into click/display URLs
5. Supports static (single-ad) placements and institution pre-exclusion

Without AMS, each publisher would need to replicate this logic or call GlassPanel directly with unacceptable latency.

## Primary Users

| User Type | Interaction | Evidence |
|-----------|-------------|----------|
| Publisher widgets / lead forms | gRPC `AdMatchingServiceInvoke` | `AdsService.cs:44-68` |
| Directory / EAV widgets | gRPC `EddyAdsListingServiceInvoke` | `AdsService.cs:71-97` |
| Internal ops / QA | Sample MVC client, health greeter | `Client/Controllers/HomeController.cs` |
| Background cache warmer | `CacheBackgroundService` | `CacheBackgroundService.cs:31-59` |

## Major Workflows

1. **Standard ad matching** — Load cached campaign/ad dictionary → chain-of-responsibility filtering → ranked ad list
2. **Static ad matching** — Resolve single ad by GUID, bypass most filtering
3. **Eddy directory listing** — WCF call to Matching Engine → map institutions to vendor ads
4. **Cache pre-warming** — Periodic full DB reload and per-source pre-filter into Redis + memory

See [BusinessProcesses.md](./BusinessProcesses.md) for detailed sequence diagrams.

## Major Integrations

| System | Protocol | Purpose |
|--------|----------|---------|
| SQL Server `GlassPanel` | EF Core (read-only) | Campaigns, ads, rules, schedules |
| SQL Server `EddyLogging` | NLog Database target | Exception + performance logging |
| Azure Redis | StackExchange.Redis | Distributed cache for pre-filtered dictionaries |
| Matching Engine WCF | SOAP/HTTP | Institution/program matching (EAV path) |
| New Relic | Agent API `[Trace]`/`[Transaction]` | APM instrumentation |
| Logo CDN | HTTP URL templates | EAV institution logos |

## High-Level Architecture

```
┌─────────────────┐     gRPC/gRPC-Web      ┌──────────────────────────┐
│ Publisher Sites │ ─────────────────────► │ EDDY.IS.AdMatching.Service│
│ Sample Client   │                        │  (ASP.NET Core host)      │
└─────────────────┘                        └────────────┬─────────────┘
                                                      │
                    ┌─────────────────────────────────┼─────────────────────────┐
                    ▼                                 ▼                         ▼
           AdMatchingService                  EddyAdsListingService        CacheBackgroundService
           (Domain orchestrator)              (EAV → WCF adapter)          (periodic refresh)
                    │                                 │
                    ▼                                 ▼
           CommonEngine + Handler Chain         Matching Engine (WCF)
                    │
         ┌──────────┴──────────┐
         ▼                       ▼
    Redis + Memory           GlassPanel SQL
```

**Style:** Layered architecture with **Chain of Responsibility** for request processing, **Repository + Unit of Work** for data access, and **two-tier caching**. Not Clean Architecture — Domain references Entities directly; Core references Data.

## Technology Stack

| Layer | Technology |
|-------|------------|
| Runtime | .NET 6.0 |
| Host | ASP.NET Core, Kestrel (HTTP/1 + HTTP/2) |
| API | gRPC + gRPC-Web |
| ORM | Entity Framework Core 6.0 (SQL Server, NoTracking) |
| Cache | IMemoryCache + StackExchange.Redis |
| Logging | NLog (console + SQL), Microsoft.Extensions.Logging bridged |
| APM | New Relic Agent API |
| Legacy integration | WCF System.ServiceModel 4.8 |
| Scripting | Roslyn CSharp.Scripting (dynamic URL parameters) |
| Timezone | GeoTimeZone |
| Tests | MSTest, NUnit (mixed) |
| Container | Docker (net5.0 images — version mismatch) |

## Deployment Model

- **Windows/IIS or Kestrel** behind reverse proxy (inferred from launch profiles and subdirectory handler in client)
- **Docker** support via `Dockerfile` (targets .NET 5.0 runtime — mismatch with net6.0 project)
- **Environments:** Development, Staging, Dryrun, Production (from `launchSettings.json`)
- **No CI/CD pipeline** found in repository (no `.github/`, Azure Pipelines, etc.) — **(high confidence)**

Production URL pattern: `ams.educationdynamics.com` (`launchSettings.json:38`).

## Strengths

1. **Pre-computed cache architecture** — Heavy filtering moved to background service; request path reads cached dictionaries (`CommonEngine.GetCacheDictionaryContainer`, `CacheBackgroundService`)
2. **Extensible handler chain** — New filtering steps can be inserted via DI chain configurator (`Startup.cs:73-84`)
3. **Pluggable rule engine** — `IRuleEngine` singleton with operator strategy pattern (`EDDY.IS.RuleEngine`)
4. **Shallow copy isolation** — Prevents request mutations from corrupting shared cache (`DictionaryContainer.CreateShallowCopy`)
5. **Comprehensive test coverage for handlers and rule engine** — `EDDY.IS.AdMatching.Core.Tests`

## Weaknesses

1. **No authentication/authorization** on gRPC endpoints (`Startup.cs` — no `AddAuthentication`)
2. **Secrets in appsettings.json** — Redis password, connection strings in plain text
3. **Docker/runtime version mismatch** — Dockerfile uses .NET 5.0, project targets 6.0
4. **Incomplete proto implementation** — `AdStaticMatchingServiceInvoke`, `CatalogGreet` RPCs defined but not implemented
5. **Dead/stub project** — `EDDY.IS.AdMatching.Caching` is empty; actual caching in Core
6. **Legacy code excluded but present** — `RulesEvaluator.cs`, `DataManager.cs` compiled out but not removed
7. **Mixed test frameworks** — MSTest in Core.Tests, NUnit referenced in Service csproj
8. **Database-first with no migrations** — Schema changes require external SQL scripts

## Technical Debt

| Item | Impact | Reference |
|------|--------|-----------|
| Empty Caching project | Confusion about cache ownership | `EDDY.IS.AdMatching.Caching/Class1.cs` |
| Unused certificate auth package | Misleading security posture | `Service.csproj:26` |
| gRPC interceptor returns null on error | Clients may get empty responses | `GrpcExceptionInterceptor.cs:18-32` |
| Repository interfaces without implementations | Incomplete abstraction | `ICampaignSpendRepository`, etc. |
| README is Azure DevOps template placeholder | No onboarding docs | `README.md` |
| `Equal` operator enum unused in RuleEngine | Potential rule evaluation gap | `Operator.cs`, `RuleEngine.cs` |
| Commented warm-up and CatalogService | Incomplete features | `Program.cs`, `Startup.cs:110` |

See [Refactoring/Recommendations.md](./Refactoring/Recommendations.md) for prioritized remediation.
