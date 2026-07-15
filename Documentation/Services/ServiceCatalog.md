# Service Catalog

## Application Services

### AdMatchingService

| Aspect | Detail |
|--------|--------|
| Interface | `IAdMatchingService` |
| Implementation | `Domain/Services/AdMatchingService.cs` |
| Lifetime | Scoped |
| Responsibilities | Orchestrate ad matching: create model, load cache, run handler chain, build response |
| Dependencies | `IEngine`, `IChainHandler<AdMatchingModel>` |
| Consumers | `AdsService.AdMatchingServiceInvoke` |
| External APIs | None |
| Caching | Via IEngine → CacheService |
| Logging | New Relic `[Trace]` |
| Error handling | Propagates to AdsService |

### EddyAdsListingService

| Aspect | Detail |
|--------|--------|
| Interface | `IEddyAdsListingService` |
| Implementation | `EAV/Services/EddyAdsListingService.cs` |
| Lifetime | Scoped |
| Responsibilities | Build ME request, call WCF, map institutions to vendor ads |
| Dependencies | `IMatchingEngineService`, `IOptions<EAVSettings>`, `ILogger` |
| Consumers | `AdsService.EddyAdsListingServiceInvoke` |
| External APIs | Matching Engine WCF |
| Retry | None observed |
| Error handling | try/catch → Success=false response |

---

## Infrastructure Services

### CommonEngine (IEngine)

| Aspect | Detail |
|--------|--------|
| File | `Core/Engines/CommonEngine.cs` |
| Lifetime | Scoped |
| Responsibilities | Cache load/filter, pre-compute source-specific dictionaries, targeting rule inheritance |
| Dependencies | `ICacheService`, `IDataManager`, `ILogger` |
| Consumers | `AdMatchingService`, `CacheBackgroundService` |
| Cache keys | `sharedDictionaryCacheKey`, `filteredDictionaryCacheKey:ForSourceId{N}` |

### CacheService (ICacheService)

| Aspect | Detail |
|--------|--------|
| File | `Core/Services/CacheService.cs` |
| Lifetime | Singleton |
| Responsibilities | Two-tier cache (memory + Redis), TTL management, key prefixing |
| Dependencies | `IMemoryCache`, `IDistributedCache`, `IConnectionMultiplexer`, `RedisSettings` |
| Key format | `{CachePrefix}:AMS:{key}` |
| Thread safety | SemaphoreSlim (3 for read/write, 2 for TTL) |
| Retry | None; errors logged, NeedsReCompute returns true |

### CommonDataManager (IDataManager)

| Aspect | Detail |
|--------|--------|
| File | `Data/CommonDataManager.cs` |
| Lifetime | Scoped |
| Responsibilities | Load full DictionaryContainer from GlassPanel via repositories |
| Dependencies | `ICommonUnitOfWorkRepositoryFactory`, `ILogger` |
| Consumers | `CommonEngine`, `CacheBackgroundService`, `StaticAdHandler` |
| Caching | None (raw DB load) |

### MatchingServiceClient (IMatchingEngineService)

| Aspect | Detail |
|--------|--------|
| File | `EAV/ServicesReferences/MatchingEngineServiceClient.cs` |
| Lifetime | Scoped |
| Responsibilities | WCF proxy to Matching Engine |
| Endpoint | Configured via `MatchingEngineServiceSettings.Endpoint` |
| Binding | CustomBinding (binary encoding + HTTP) |
| Retry | None observed |

---

## Evaluator/Builder Services (Core/Engines)

| Service | Used By | Purpose |
|---------|---------|---------|
| StopsEvaluator | StopsHandler | Stop window logic |
| CapsEvaluator | CapsHandler | Cap enforcement |
| ScheduleEvaluator | SchedulingHandler | Schedule matching |
| ResponseBuilder | ResponseBuilderHandler | SlimAd → AdsMatched mapping |
| AdSortingEngine | AdSortingHandler | CPC ranking, dedup, selection |
| ParametersEvaluator | ParametersHandler | URL macro substitution, C# scripts |

---

## Logging Services

| Service | Lifetime | Purpose |
|---------|----------|---------|
| DebugLogger | Singleton | Conditional debug logging via NLog |
| PerformanceLogger | Singleton | Request performance to SQL (conditional) |
| GrpcExceptionInterceptor | Per-call | gRPC exception logging |

---

## Background Services

### CacheBackgroundService

| Aspect | Detail |
|--------|--------|
| File | `Service/CacheBackgroundService.cs` |
| Type | `BackgroundService` (Hosted) |
| Schedule | Every `ComputeIntervalSeconds` (default 60s) |
| Action | Full DB reload + per-source cache pre-filter |
| Error handling | Log and continue loop |
| Scoped resolution | Creates DI scope per iteration |

---

## Rule Engine Service

### RuleEngine (IRuleEngine)

| Aspect | Detail |
|--------|--------|
| File | `RuleEngine/CustomRuleEngine/RuleEngine.cs` |
| Lifetime | Singleton |
| Responsibilities | Recursive AND/OR tree evaluation, operator dispatch |
| Thread safety | Stateless (operators created per call) |
| Consumers | RuleEngineHandler, DynamicBidVariablesHandler |

---

## gRPC Services

| Service | RPCs | Implementation |
|---------|------|----------------|
| GreeterService | SayHello | Demo |
| AdsService | AdMatchingServiceInvoke, EddyAdsListingServiceInvoke | Production |

---

## Potential Improvements

1. Add retry/circuit breaker for WCF Matching Engine calls
2. Extract cache interface from Core to eliminate dead Caching project
3. Add health check endpoint for cache freshness
4. Implement missing `AdStaticMatchingServiceInvoke` or remove from proto
5. Replace C# scripting in ParametersEvaluator with safer template engine
