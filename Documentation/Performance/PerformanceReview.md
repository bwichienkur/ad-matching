# Performance Review

## Architecture Strengths

The **pre-computation + cache** design is the primary performance optimization:

1. Full DB load happens in background (`CacheBackgroundService`) not per request
2. Per-source filtering pre-computed and cached in Redis + memory
3. Request path reads cached dictionaries and runs in-memory handler chain
4. EF Core `NoTracking` avoids change tracking overhead

**Evidence:** `CommonEngine.GetCacheDictionaryContainer`, `CacheBackgroundService`, `GlassPanelContext` constructor.

---

## Hot Paths

| Path | Frequency | Components |
|------|-----------|------------|
| AdMatchingServiceInvoke | High | CacheService → Handler chain → RuleEngine |
| Cache refresh | Every 60s | Full DB load + N source filters |
| RuleEngine evaluation | Per rule per request | Recursive tree + operator dispatch |

---

## Memory Allocations

| Area | Concern | Evidence |
|------|---------|----------|
| Full DictionaryContainer load | Large object heap | All campaigns/ads/rules loaded into memory |
| Shallow copy per request | Dictionary allocations | `CreateShallowCopy` creates new dict shells |
| LINQ in handlers | Intermediate enumerables | Multiple `.Where().ToList()` in handlers |
| JSON serialization for perf logging | Allocations per request | `JsonSerializer.Serialize(request/response)` in finally |

**Recommendation:** Pool or reuse DictionaryContainer where safe; avoid serializing full request/response in hot path when performance logging disabled.

---

## LINQ Inefficiencies

| Pattern | Location | Impact |
|---------|----------|--------|
| Multiple `.Where()` on same collection | RuleEngineHandler | Medium — runs per rule |
| `.Count()` vs `.Count` property | AdMatchingService:33,46 | Minor — extension method on IEnumerable |
| Full table `.GetAll()` | CommonDataManager | High at refresh time, not per request |

---

## N+1 Queries

**Not observed in request path** — all data pre-loaded into dictionaries.

At refresh time, each repository call is a separate query (not N+1 per entity, but ~20 sequential full-table queries). Could be optimized with parallel loading.

---

## Async Correctness

| Item | Status |
|------|--------|
| CacheService async methods | Proper async/await with semaphores |
| AdMatchingService.GetAdsMatched | async throughout |
| CacheBackgroundService | async with Task.WhenAll for source filters |
| GreeterService.SayHello | Sync Task.FromResult (acceptable for trivial) |

**No `.Result` or `.Wait()` blocking calls found** in hot paths (high confidence from handler review).

---

## Database Performance

| Factor | Assessment |
|--------|------------|
| Read-only NoTracking | Good |
| No pagination on GetAll | Risk at scale — entire tables loaded |
| View-based queries | VW_SlimAdsAMS likely optimized in SQL |
| Connection pooling | Default EF Core pooling |

**Recommendation:** Monitor GlassPanel query times during cache refresh; consider materialized views or incremental refresh.

---

## Caching Opportunities

| Already Implemented | Additional Opportunity |
|--------------------|----------------------|
| Redis + memory two-tier | Incremental cache update vs full reload |
| Per-source pre-filter | Cache rule evaluation results for common parameter sets (low ROI) |
| TTL-based recompute | Stagger source refresh to avoid thundering herd |

---

## Parallelization

| Current | Opportunity |
|---------|-------------|
| `Task.WhenAll` for source filters in background | Parallel repository loads in CommonDataManager |
| Sequential handler chain | Cannot parallelize (state mutations) |
| Singleton semaphores in CacheService | May bottleneck under high concurrency |

---

## Large Object Allocations

`DictionaryContainer` and nested dictionaries hold the entire ad server state in memory. With thousands of campaigns/ads, this could be 10-100MB+ per instance.

**Recommendation:** Monitor memory per pod; consider Redis-only mode without memory cache duplication for large deployments.

---

## Performance Monitoring

| Tool | Usage |
|------|-------|
| PerformanceLogger | Optional SQL logging of request duration + payload |
| New Relic | `[Transaction]` on AdMatchingServiceInvoke, `[Trace]` on data load |
| NLog | Error logging only in production config |

---

## Recommendations

| Priority | Action | Impact |
|----------|--------|--------|
| High | Parallelize CommonDataManager repository loads | Faster cache refresh |
| High | Skip JSON serialization when perf logging disabled | Reduce per-request allocations |
| Medium | Add cache hit/miss metrics | Observability |
| Medium | Stagger background refresh per source | Reduce CPU spikes |
| Low | Replace IEnumerable.Count() with .Count property | Minor GC reduction |
