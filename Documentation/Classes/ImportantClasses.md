# Important Class Documentation

## AdMatchingService

| Aspect | Detail |
|--------|--------|
| **Purpose** | Application orchestrator for ad matching |
| **File** | `Domain/Services/AdMatchingService.cs` |
| **Responsibilities** | Create model, load cache, invoke chain, build response |
| **Collaborators** | IEngine, IChainHandler<AdMatchingModel> |
| **Lifecycle** | Scoped per request |
| **Dependencies** | IEngine, IChainHandler |
| **Patterns** | Facade |
| **Thread safety** | Not thread-safe (scoped instance) |
| **Issues** | Typo in error message "campaings" |
| **Methods** | `GetAdsMatched` (async), `CreateModel` (private static) |
| **Complexity** | Low — delegates to engine and chain |

---

## CommonEngine

| Aspect | Detail |
|--------|--------|
| **Purpose** | Cache management and pre-filtering engine |
| **File** | `Core/Engines/CommonEngine.cs` |
| **Responsibilities** | Load shared/source caches, filter campaigns/ads/rules per source, targeting rule inheritance |
| **Collaborators** | ICacheService, IDataManager |
| **Lifecycle** | Scoped |
| **Cache keys** | `sharedDictionaryCacheKey`, `filteredDictionaryCacheKey:ForSourceId{N}` |
| **Patterns** | Facade over cache + data |
| **Thread safety** | Relies on CacheService semaphores |
| **Methods** | `GetCacheDictionaryContainer`, `LoadSharedContainer`, `FilterDictionaryContainer`, multiple Filter* methods |
| **Complexity** | High — central pre-computation logic |

---

## CacheService

| Aspect | Detail |
|--------|--------|
| **Purpose** | Two-tier distributed cache |
| **File** | `Core/Services/CacheService.cs` |
| **Responsibilities** | Memory + Redis read/write, TTL check, key prefixing |
| **Collaborators** | IMemoryCache, IDistributedCache, IConnectionMultiplexer |
| **Lifecycle** | Singleton |
| **Patterns** | Cache-Aside |
| **Thread safety** | SemaphoreSlim (3 read/write, 2 TTL) |
| **Methods** | `GetFromCache<T>`, `SetValueToCache<T>`, `NeedsReCompute`, `RefreshFromDistributedCache` |
| **Complexity** | Medium |

---

## CommonDataManager

| Aspect | Detail |
|--------|--------|
| **Purpose** | Full database snapshot loader |
| **File** | `Data/CommonDataManager.cs` |
| **Responsibilities** | Load all enabled entities into DictionaryContainer, parse targeting rules |
| **Collaborators** | ICommonUnitOfWorkRepositoryFactory |
| **Lifecycle** | Scoped |
| **Patterns** | Repository consumer, Data Mapper |
| **Thread safety** | Scoped — not shared |
| **Methods** | `GetDictionaryContainer`, `GetStaticAd` |
| **Complexity** | Medium — repetitive but straightforward |

---

## RuleEngine

| Aspect | Detail |
|--------|--------|
| **Purpose** | Evaluate QueryBuilder targeting rules |
| **File** | `RuleEngine/CustomRuleEngine/RuleEngine.cs` |
| **Responsibilities** | Recursive AND/OR evaluation, operator dispatch |
| **Collaborators** | Operator strategy classes |
| **Lifecycle** | Singleton (stateless) |
| **Patterns** | Composite (tree), Strategy (operators) |
| **Thread safety** | Safe — no mutable state |
| **Methods** | `EvaluateRulesForDictionaryAndQueryBuilderFilterRule`, `EvaluateOperator` |
| **Complexity** | Medium-High — large switch statement |

---

## AdsService (gRPC)

| Aspect | Detail |
|--------|--------|
| **Purpose** | gRPC endpoint adapter |
| **File** | `Service/Services/AdsService.cs` |
| **Responsibilities** | Proto ↔ DTO mapping, error handling, performance logging |
| **Collaborators** | IAdMatchingService, IEddyAdsListingService, PerformanceLogger |
| **Lifecycle** | Per gRPC call (scoped dependencies) |
| **Patterns** | Adapter, Anti-Corruption Layer |
| **Issues** | Exception messages exposed to clients; unmapped proto fields |
| **Methods** | `AdMatchingServiceInvoke`, `EddyAdsListingServiceInvoke`, mapping helpers |
| **Complexity** | Medium — mostly mapping boilerplate |

---

## CacheBackgroundService

| Aspect | Detail |
|--------|--------|
| **Purpose** | Periodic cache refresh |
| **File** | `Service/CacheBackgroundService.cs` |
| **Responsibilities** | Full DB reload + per-source pre-filter on timer |
| **Collaborators** | IServiceProvider (creates scopes), IEngine, IDataManager |
| **Lifecycle** | Hosted singleton |
| **Patterns** | Background Service |
| **Thread safety** | Creates scoped instances per iteration |
| **Methods** | `ExecuteAsync` |
| **Complexity** | Low |

---

## ChainConfigurator

| Aspect | Detail |
|--------|--------|
| **Purpose** | Wire handler chain via DI |
| **File** | `Core/ChainResponsability/ChainConfigurator.cs` |
| **Responsibilities** | Register handlers as scoped, compile expression trees linking Next |
| **Patterns** | Factory Method, Chain of Responsibility wiring |
| **Complexity** | High — expression tree compilation |

---

## Handler Classes (Summary)

| Handler | Key Collaborator | Mutates |
|---------|-----------------|---------|
| StaticAdHandler | IDataManager | SlimAdsDictionary |
| PreExcludeHandler | — | Accounts, ads |
| StopsHandler | StopsEvaluator | CampaignsList |
| CapsHandler | CapsEvaluator | CampaignsList |
| RuleEngineHandler | IRuleEngine | SlimAdsDictionary, CampaignsList |
| SchedulingHandler | ScheduleEvaluator | CampaignsList |
| ResponseBuilderHandler | ResponseBuilder | FinalAdsList |
| DynamicBidVariablesHandler | IRuleEngine | DynamicBoostPercent |
| AdSortingHandler | AdSortingEngine | FinalAdsList order/count |
| ParametersHandler | ParametersEvaluator | FinalAdsList URLs |

All implement `IChainHandler<AdMatchingModel>`, scoped, sequential execution.

---

## EddyAdsListingService

| Aspect | Detail |
|--------|--------|
| **Purpose** | EAV directory listing via Matching Engine |
| **File** | `EAV/Services/EddyAdsListingService.cs` |
| **Responsibilities** | Build ME request, call WCF, map institutions to vendor ads |
| **Collaborators** | IMatchingEngineService, IOptions<EAVSettings> |
| **Lifecycle** | Scoped |
| **Patterns** | Adapter, Anti-Corruption Layer |
| **Issues** | No retry on WCF failure |
| **Complexity** | Medium-High — mapping logic |

---

## GlassPanelContext

| Aspect | Detail |
|--------|--------|
| **Purpose** | EF Core database context |
| **File** | `Data/Context/GlassPanelContext.cs` |
| **Responsibilities** | 93 DbSets, fluent entity configuration |
| **Lifecycle** | Scoped |
| **Configuration** | NoTracking, SQL Server |
| **Complexity** | Very High (~1930 lines, auto-scaffolded) |

---

## TargetingRule

| Aspect | Detail |
|--------|--------|
| **Purpose** | Targeting/optimization rule entity |
| **File** | `Entities/TargetingRule.cs` |
| **Responsibilities** | Store RuleJson, scope to campaign/ad-group, dynamic bid flag |
| **Lifecycle** | Loaded at cache refresh; RuleJson parsed to QueryBuilderFilterRule |
| **NotMapped** | RuleAsQueryBuilderFilterRule (runtime parsed) |

---

## DictionaryContainer / FilteredContainerDictionary

| Aspect | Detail |
|--------|--------|
| **Purpose** | In-memory data structures for matching pipeline |
| **Files** | `Domain/BusinessEntities/DictionaryContainer.cs` |
| **Responsibilities** | Hold all campaigns, ads, rules, stops, schedules as dictionaries |
| **Key method** | `CreateShallowCopy` — isolation from cache mutations |
| **Complexity** | Medium — many dictionary properties |

---

## GrpcExceptionInterceptor

| Aspect | Detail |
|--------|--------|
| **Purpose** | Global gRPC exception handler |
| **File** | `Service/Logging/GrpcExceptionInterceptor.cs` |
| **Issue** | Returns null instead of RpcException — **bug** |
| **Complexity** | Low |

---

## PerformanceLogger / DebugLogger

| Aspect | Detail |
|--------|--------|
| **Purpose** | Conditional performance/debug logging via separate NLog configs |
| **Files** | `Core/Logging/PerformanceLogger.cs`, `DebugLogger.cs` |
| **Lifecycle** | Singleton |
| **Activation** | `LoggingPerformance:EnabledTrueFalse`, `LoggingDebugInformation:EnabledTrueFalse` |
