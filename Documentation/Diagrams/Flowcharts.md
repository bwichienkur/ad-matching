# Flowcharts

## 1. Ad Matching Decision Flow

```mermaid
flowchart TD
    Start([gRPC Request Received]) --> Parse[Parse Request Parameters]
    Parse --> Static{StaticAdGuid valid?}
    Static -->|Yes| StaticPath[StaticAdHandler: single ad]
    Static -->|No| LoadCache[Load Cached Dictionary for SourceId]
    StaticPath --> LoadCache

    LoadCache --> HasAds{SlimAds exist?}
    HasAds -->|No| Empty1[Return: No ads matched with parameters]
    HasAds -->|Yes| PreExclude[PreExclude Institutions]

    PreExclude --> Stops[Evaluate Stop Windows]
    Stops --> Caps[Remove Capped Campaigns]
    Caps --> Rules[Evaluate Targeting Rules]
    Rules --> Schedule[Evaluate Schedules]
    Schedule --> Build[Build AdsMatched List]
    Build --> DynBid[Apply Dynamic Bid Rules]
    DynBid --> Sort[Sort, Dedup, Select Top N]
    Sort --> Params[Substitute URL Parameters]
    Params --> HasFinal{FinalAdsList empty?}

    HasFinal -->|Yes| Empty2[Return: No ads after filtering]
    HasFinal -->|No| Success[Return: Success with ads]
```

## 2. Targeting Rule Evaluation

```mermaid
flowchart TD
    Start([For each TargetingRule]) --> Scope{Rule scope?}
    Scope -->|AdGroup| EvalAdGroup[Evaluate rule against Parameters]
    Scope -->|Campaign| DynBid{IsDynamicBid?}
    DynBid -->|Yes| Skip[Skip - handled later]
    DynBid -->|No| EvalCamp[Evaluate rule against Parameters]

    EvalAdGroup --> Pass1{Pass?}
    Pass1 -->|No| RemoveAds[Remove ads in ad group]
    Pass1 -->|Yes| Next1[Next rule]

    EvalCamp --> Pass2{Pass?}
    Pass2 -->|No| RemoveCamp[Remove ads + campaign]
    Pass2 -->|Yes| Next2[Next rule]

    RemoveAds --> Next1
    RemoveCamp --> Next2
    Skip --> Next2
    Next1 --> Done([Continue chain])
    Next2 --> Done
```

## 3. Rule Engine Tree Evaluation

```mermaid
flowchart TD
    Start([Evaluate Rule Node]) --> HasCond{Has Condition AND/OR?}
    HasCond -->|Yes| Loop[For each child rule]
    HasCond -->|No| Leaf[Evaluate leaf operator]

    Loop --> Child{Child type?}
    Child -->|Group| Recurse[Recursive evaluate]
    Child -->|Leaf| Leaf

    Recurse --> Combine
    Leaf --> Combine{Combine results}

    Combine -->|AND| AndCheck{Any fail?}
    AndCheck -->|Yes| Fail([Return Fail - short circuit])
    AndCheck -->|No| Continue[Continue loop]

    Combine -->|OR| OrCheck{Any pass?}
    OrCheck -->|Yes| Pass([Return Pass - short circuit])
    OrCheck -->|No| Continue

    Continue --> Loop
    Loop -->|Done| Result([Return aggregated result])
```

## 4. Cache TTL Recompute Decision

```mermaid
flowchart TD
    Start([NeedsReCompute called]) --> GetTTL[Get Redis key TTL]
    GetTTL --> Null{TTL null?}
    Null -->|Yes| Recompute([Return true - recompute])
    Null -->|No| Check{TTL <= 20% of LifeSpanSeconds?}
    Check -->|Yes| Recompute
    Check -->|No| Fresh([Return false - cache fresh])
```

## 5. Ad Sorting & Selection

```mermaid
flowchart TD
    Start([AdSortingHandler]) --> Boost[Calculate CPC boosts]
    Boost --> Rank[Apply rank/school/schedule multipliers]
    Rank --> Sort[Sort by BoostedCPC descending]
    Sort --> Dedup[Remove duplicate client ad accounts]
    Dedup --> Random[Random selection among remaining]
    Random --> Cap[Take top MaxAds]
    Cap --> Done([Update FinalAdsList])
```

## 6. EAV Listing Flow

```mermaid
flowchart TD
    Start([EddyAdsListingRequest]) --> Build[Build DirectoryMatchRequest]
    Build --> WCF[Call Matching Engine GetInstitutions]
    WCF --> Success{ME success?}
    Success -->|No| Error[Return Success=false]
    Success -->|Yes| Filter[Filter excluded/duplicate institutions]
    Filter --> Map[Map to EddyVendorAd]
    Map --> Logo[Apply logo URL templates]
    Logo --> URL[Build click URLs from EAVSettings]
    URL --> Trim[Trim to MaxAds]
    Trim --> Return([Return EddyAdsListingResponse])
```

## 7. Stop Window Evaluation

```mermaid
flowchart TD
    Start([StopsHandler]) --> AcctStops[Check account-level stops]
    AcctStops --> InStop{Account in stop window?}
    InStop -->|Yes| RemoveAcct[Remove all account ads]
    InStop -->|No| CampStops[Check campaign stops]
    CampStops --> InCampStop{Campaign in stop window?}
    InCampStop -->|Yes| RemoveCamp[Remove campaign from list]
    InCampStop -->|No| Keep[Keep campaign]
    RemoveAcct --> Next
    RemoveCamp --> Next
    Keep --> Next([Next handler])
```

## 8. Startup & Configuration Flow

```mermaid
flowchart TD
    Start([Program.Main]) --> NLog[Initialize NLog from config]
    NLog --> Host[CreateHostBuilder]
    Host --> Startup[Startup.ConfigureServices]
    Startup --> Redis[Connect Redis + register cache]
    Redis --> EF[Register GlassPanelContext]
    EF --> DI[Register services + handler chain]
    DI --> GRPC[Register gRPC + interceptor]
    GRPC --> BG[Register CacheBackgroundService]
    BG --> Run[host.Run]
    Run --> Listen[Listen for gRPC requests]
    Run --> BGStart[Background cache loop starts]
```
