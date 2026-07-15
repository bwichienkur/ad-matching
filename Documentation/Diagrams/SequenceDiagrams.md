# Sequence Diagrams

## 1. Standard Ad Matching (Full)

```mermaid
sequenceDiagram
    autonumber
    participant Pub as Publisher
    participant GRPC as AdsService
    participant AMS as AdMatchingService
    participant Engine as CommonEngine
    participant Cache as CacheService
    participant Redis as Redis
    participant Chain as Handler Chain
    participant RE as RuleEngine

    Pub->>GRPC: AdMatchingServiceInvoke(request)
    GRPC->>AMS: GetAdsMatched(dto)
    AMS->>AMS: CreateModel(request)
    AMS->>Engine: GetCacheDictionaryContainer(sourceId)
    Engine->>Cache: GetFromCache(sourceKey)
    Cache->>Cache: Check IMemoryCache
    Cache->>Redis: GetFromRedis if miss
    Redis-->>Cache: FilteredContainer JSON
    Cache-->>Engine: Source-filtered data
    Engine->>Cache: GetFromCache(sharedKey)
    Cache-->>Engine: Shared data
    Engine->>Engine: CreateShallowCopy
    Engine-->>AMS: FilteredContainerDictionary

    alt No ads and not static
        AMS-->>GRPC: Empty response
    end

    AMS->>Chain: Handle(model)
    Note over Chain: StaticAd → PreExclude → Stops → Caps
    Chain->>RE: EvaluateRules (targeting)
    RE-->>Chain: Pass/Fail per rule
    Note over Chain: Scheduling → ResponseBuilder
    Chain->>RE: EvaluateRules (dynamic bid)
    Note over Chain: AdSorting → Parameters
    Chain-->>AMS: FinalAdsList

    AMS-->>GRPC: AdMatchingResponse
    GRPC->>GRPC: Map to proto
    GRPC->>GRPC: PerformanceLogger.Log
    GRPC-->>Pub: getAdMatchingResponse
```

## 2. Cache Miss Recovery

```mermaid
sequenceDiagram
    participant Engine as CommonEngine
    participant Cache as CacheService
    participant DM as CommonDataManager
    participant SQL as GlassPanel
    participant Redis as Redis

    Engine->>Cache: GetFromCache(key)
    Cache-->>Engine: null (miss)
    Engine->>DM: GetDictionaryContainer()
    DM->>SQL: Multiple GetAll queries
    SQL-->>DM: Entity data
    DM-->>Engine: DictionaryContainer
    Engine->>Engine: LoadSharedContainer
    Engine->>Cache: SetValueToCache(sharedKey)
    Cache->>Redis: SET with TTL
    Engine->>Engine: FilterDictionaryContainer(sourceId)
    Engine->>Cache: SetValueToCache(sourceKey)
    Engine-->>Engine: Return filtered copy
```

## 3. Background Cache Refresh

```mermaid
sequenceDiagram
    participant BG as CacheBackgroundService
    participant Scope as DI Scope
    participant DM as CommonDataManager
    participant Engine as CommonEngine
    participant SQL as GlassPanel

    loop Every ComputeIntervalSeconds
        BG->>Scope: CreateScope()
        Scope->>DM: GetDictionaryContainer()
        DM->>SQL: Full table loads
        SQL-->>DM: All entities
        Scope->>Engine: LoadSharedContainer(data)
        par For each sourceId
            Scope->>Engine: FilterDictionaryContainer(sourceId, data)
        end
        BG->>BG: Task.Delay(interval)
    end
```

## 4. Eddy Ads Listing (EAV)

```mermaid
sequenceDiagram
    participant Pub as Publisher
    participant GRPC as AdsService
    participant EAV as EddyAdsListingService
    participant WCF as MatchingServiceClient
    participant ME as Matching Engine

    Pub->>GRPC: EddyAdsListingServiceInvoke
    GRPC->>EAV: GetEddyAdsListingMatched
    EAV->>EAV: BuildMatchRequest(params)
    EAV->>WCF: GetInstitutionsAsync
    WCF->>ME: SOAP GetInstitutions
    ME-->>WCF: InstitutionResponse
    WCF-->>EAV: Institutions list
    EAV->>EAV: Filter duplicates/exclusions
    EAV->>EAV: Map to EddyVendorAd (URLs, logos)
    EAV->>EAV: Trim to MaxAds
    EAV-->>GRPC: EddyAdsListingResponse
    GRPC-->>Pub: getEddyAdsListingResponse
```

## 5. Static Ad Path

```mermaid
sequenceDiagram
    participant AMS as AdMatchingService
    participant Chain as StaticAdHandler
    participant DM as IDataManager
    participant Next as PreExcludeHandler

    AMS->>Chain: Handle(model) [IsStatic=true]
    Chain->>Chain: Lookup StaticAdGuid in cache
    alt Not in cache
        Chain->>DM: GetStaticAd(guid)
        DM-->>Chain: VwAdsAm
    end
    Chain->>Chain: Set SlimAdsDictionary to single ad
    Chain->>Next: Handle(model)
```

## 6. Rule Engine Evaluation

```mermaid
sequenceDiagram
    participant Handler as RuleEngineHandler
    participant RE as RuleEngine
    participant Op as Operator Strategy

    Handler->>RE: EvaluateRules(params, rule)
    RE->>RE: Check Condition AND/OR
    loop Each sub-rule
        alt Nested group
            RE->>RE: Recursive evaluate
        else Leaf rule
            RE->>Op: EvaluateOperator(params, rule)
            Op->>Op: Compare field value
            Op-->>RE: RuleEngineResult(Pass/Fail)
        end
        RE->>RE: Short-circuit AND/OR
    end
    RE-->>Handler: Final Pass/Fail
    alt Fail
        Handler->>Handler: Remove ads/campaigns
    end
```

## 7. Error Handling Flow

```mermaid
sequenceDiagram
    participant Pub as Client
    participant GRPC as AdsService
    participant Svc as AdMatchingService
    participant Log as ILogger/NLog

    Pub->>GRPC: AdMatchingServiceInvoke
    GRPC->>Svc: GetAdsMatched
    Svc--xSvc: Exception thrown
    GRPC->>Log: LogError
    GRPC->>GRPC: response.Message = ex.Message
    GRPC->>GRPC: PerformanceLogger (finally)
    GRPC-->>Pub: Error response (not gRPC error status)
```

## 8. Application Startup

```mermaid
sequenceDiagram
    participant Main as Program.Main
    participant NLog as NLog
    participant Host as WebHost
    participant Startup as Startup
    participant BG as CacheBackgroundService

    Main->>NLog: LoadConfigurationFromAppSettings
    Main->>Host: Build
    Host->>Startup: ConfigureServices
    Note over Startup: Redis, EF, DI, Chain, gRPC
    Host->>Startup: Configure middleware
    Main->>Host: Run
    Host->>BG: Start background loop
```
