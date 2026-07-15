# Dependency Graphs

## Project Dependency Graph

```mermaid
graph TD
    subgraph Host
        SVC[AdMatching.Service]
    end

    subgraph Application
        DOM[AdMatching.Domain]
        EAV[AdMatching.EAV]
    end

    subgraph Business Logic
        CORE[AdMatching.Core]
        RE[RuleEngine]
    end

    subgraph Infrastructure
        DATA[AdMatching.Data]
        ENT[AdMatching.Entities]
        REPO[AdMatching.Repositories]
    end

    subgraph Shared
        COM[Common]
    end

    subgraph Tests
        CT[Core.Tests]
        ST[ServiceTest]
        CLI[Client]
    end

    SVC --> CORE
    SVC --> DATA
    SVC --> EAV
    SVC --> COM

    CORE --> DATA
    CORE --> DOM
    CORE --> REPO
    CORE --> RE

    DATA --> DOM
    DATA --> ENT
    DATA --> REPO
    DATA --> COM

    DOM --> ENT
    EAV --> DOM
    ENT --> COM
    REPO --> ENT
    RE --> COM

    CT --> CORE
    CT --> SVC
    CT --> RE
    ST --> CLI
```

## DI Dependency Graph (Runtime)

```mermaid
graph TD
    AdsService --> IAdMatchingService
    AdsService --> IEddyAdsListingService
    AdsService --> PerformanceLogger

    IAdMatchingService --> IEngine
    IAdMatchingService --> IChainHandler

    IEngine --> ICacheService
    IEngine --> IDataManager

    IDataManager --> ICommonUnitOfWorkRepositoryFactory
    ICommonUnitOfWorkRepositoryFactory --> GlassPanelContext

    IChainHandler --> StaticAdHandler
    StaticAdHandler --> PreExcludeHandler
    PreExcludeHandler --> StopsHandler
    StopsHandler --> CapsHandler
    CapsHandler --> RuleEngineHandler
    RuleEngineHandler --> IRuleEngine
    RuleEngineHandler --> SchedulingHandler
    SchedulingHandler --> ResponseBuilderHandler
    ResponseBuilderHandler --> DynamicBidVariablesHandler
    DynamicBidVariablesHandler --> AdSortingHandler
    AdSortingHandler --> ParametersHandler

    IEddyAdsListingService --> IMatchingEngineService

    CacheBackgroundService --> IEngine
    CacheBackgroundService --> IDataManager

    ICacheService --> IMemoryCache
    ICacheService --> IDistributedCache
    ICacheService --> IConnectionMultiplexer
```

## NuGet Package Dependency Graph (Key Packages)

```mermaid
graph LR
    SVC[Service] --> Grpc[Grpc.AspNetCore 2.40]
    SVC --> Redis[StackExchangeRedis Cache 6.0]
    SVC --> NLog[NLog.Web.AspNetCore 5.1]

    CORE[Core] --> SERedis[StackExchange.Redis 2.66]
    CORE --> Roslyn[CSharp.Scripting 4.0]
    CORE --> GeoTZ[GeoTimeZone 4.1]

    DATA[Data] --> EF[EF Core.SqlServer 6.0]

    EAV[EAV] --> WCF[System.ServiceModel.* 4.8]

    DOM[Domain] --> NR[NewRelic.Agent.Api 10.0]
```

## Data Dependency Graph

```mermaid
graph TD
    DM[CommonDataManager] --> R1[CampaignRepository]
    DM --> R2[SlimAdsAMSRepository]
    DM --> R3[TargetingRuleRepository]
    DM --> R4[AdsAMSRepository]
    DM --> R5[SourceByCampaignRepository]
    DM --> RN[... 15 more repos]

    R1 --> CTX[GlassPanelContext]
    R2 --> CTX
    R3 --> CTX

    CTX --> V1[VW_SlimAdsAMS]
    CTX --> V2[VW_AdsAMS]
    CTX --> V3[VW_CampaignAMS]
    CTX --> T1[TargetingRule table]
```

## External System Dependency Graph

```mermaid
graph TD
    AMS[Ad Matching Service]
    AMS --> GP[GlassPanel SQL Server]
    AMS --> EL[EddyLogging SQL Server]
    AMS --> RD[Azure Redis]
    AMS --> ME[Matching Engine WCF]
    AMS --> CDN[Logo CDN HTTP]
    AMS --> NR[New Relic APM]

    PUB[Publisher Sites] --> AMS
    CLI[Sample Client] --> AMS
```
