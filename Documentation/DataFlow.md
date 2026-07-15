# Data Flow Documentation

## Request Lifecycle (Standard Ad Match)

```mermaid
flowchart TD
    A[gRPC Request] --> B[AdsService]
    B --> C[Map proto to AdMatchingRequest]
    C --> D[AdMatchingService.GetAdsMatched]
    D --> E[Create AdMatchingModel]
    E --> F[CommonEngine.GetCacheDictionaryContainer]
    F --> G{Cache hit?}
    G -->|Yes| H[Shallow copy FilteredContainer]
    G -->|No| I[Load from DB + filter + cache]
    I --> H
    H --> J{Any ads?}
    J -->|No| K[Return empty response]
    J -->|Yes| L[Handler Chain x10]
    L --> M[Map to AdMatchingResponse]
    M --> N[Map to proto getAdMatchingResponse]
    N --> O[Performance log]
    O --> P[gRPC Response]
```

## Database Interaction Flow

```mermaid
flowchart LR
    subgraph Background
        BG[CacheBackgroundService] --> DM[CommonDataManager]
        DM --> UoW[UnitOfWork Factory]
        UoW --> CTX[GlassPanelContext]
        CTX --> SQL[(GlassPanel SQL)]
    end

    subgraph Request
        REQ[AdMatchingService] --> ENG[CommonEngine]
        ENG --> CACHE[CacheService]
        CACHE --> REDIS[(Redis)]
        CACHE --> MEM[IMemoryCache]
    end

    BG --> ENG
    ENG -.->|cache miss| DM
```

**Key insight:** Normal requests do NOT hit SQL Server. DB accessed only on cache miss or background refresh.

## Service Interaction Flow

```mermaid
flowchart TB
    subgraph Presentation
        AdsService
    end

    subgraph Application
        AdMatchingService
        EddyAdsListingService
    end

    subgraph Domain Logic
        CommonEngine
        Handlers[10 Handlers]
        RuleEngine
        Evaluators[Stops/Caps/Schedule/Sort/Params]
    end

    subgraph Infrastructure
        CacheService
        CommonDataManager
        WCFClient[MatchingServiceClient]
    end

    AdsService --> AdMatchingService
    AdsService --> EddyAdsListingService
    AdMatchingService --> CommonEngine
    AdMatchingService --> Handlers
    Handlers --> RuleEngine
    Handlers --> Evaluators
    CommonEngine --> CacheService
    CommonEngine --> CommonDataManager
    EddyAdsListingService --> WCFClient
```

## External API Interaction Flow

```mermaid
sequenceDiagram
    participant AMS as Ad Matching Service
    participant Redis
    participant SQL as GlassPanel SQL
    participant ME as Matching Engine WCF
    participant NLog as EddyLogging SQL

    Note over AMS: Standard path
    AMS->>Redis: Get cached dictionary
    Redis-->>AMS: FilteredContainer

    Note over AMS: Cache miss path
    AMS->>SQL: EF Core GetAll queries
    SQL-->>AMS: Entity data

    Note over AMS: EAV path
    AMS->>ME: GetInstitutions SOAP
    ME-->>AMS: InstitutionResponse

    Note over AMS: Error path
    AMS->>NLog: INSERT Exception

    Note over AMS: Performance path
    AMS->>NLog: INSERT PerformanceLogging
```

## Authentication Flow

**Not implemented.** No authentication flow exists.

```mermaid
flowchart LR
    Client -->|Unauthenticated gRPC| AMS[Ad Matching Service]
    AMS --> Response
```

**(Inferred)** Authentication may occur at network/reverse-proxy level outside this application.

## Background Processing Flow

See [BusinessProcesses.md](./BusinessProcesses.md) Process 13.

```mermaid
flowchart TD
    Start[Host starts] --> Register[Register CacheBackgroundService]
    Register --> Loop{Cancellation requested?}
    Loop -->|No| Scope[Create DI scope]
    Scope --> Load[GetDictionaryContainer from SQL]
    Load --> Shared[LoadSharedContainer to cache]
    Shared --> Parallel[FilterDictionaryContainer per source]
    Parallel --> Wait[Task.WhenAll]
    Wait --> Delay[Delay ComputeIntervalSeconds]
    Delay --> Loop
    Loop -->|Yes| Stop[Stop]
```

## Cache Key Flow

| Key Pattern | Content | Scope |
|-------------|---------|-------|
| `{prefix}:AMS:sharedDictionaryCacheKey` | Ads, slim ads, relationships, timezones | Global |
| `{prefix}:AMS:filteredDictionaryCacheKey:ForSourceId{N}` | Source-filtered campaigns, rules, stops | Per source |

Prefix from `Redis:CachePrefix` (default: `ams-refactor`).

## Mutation Isolation Flow

```mermaid
flowchart LR
    A[Shared cache object] --> B[CreateShallowCopy]
    B --> C[Request-scoped copy]
    C --> D[Handler mutations]
    D --> E[Original cache unchanged]
```

Evidence: `DictionaryContainer.CreateShallowCopy` in Domain/BusinessEntities.
