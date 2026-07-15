# Design Patterns

## 1. Chain of Responsibility

**Where:** `Core/RequestHandler/`, `Core/ChainResponsability/`

**Implementation:** Each handler implements `IChainHandler<AdMatchingModel>` with `Handle()` and `Next`. `ChainConfigurator` wires handlers via DI expression trees at startup.

**Why:** Decomposes monolithic ad filtering into independent, testable, reorderable steps.

```csharp
// Startup.cs:73-84 — chain registration
services.Chain<IChainHandler<AdMatchingModel>>()
    .Add<StaticAdHandler>()
    .Add<PreExcludeHandler>()
    // ... 8 more handlers
    .Configure();
```

---

## 2. Repository

**Where:** `Data/Repositories/`, `Repositories/Interfaces/`

**Implementation:** `GenericReadOnlyRepository<T>` provides `GetAll`, `Get`, `GetByID`. Concrete repos are marker interfaces extending generic repo.

**Why:** Abstracts EF Core data access; enables unit testing with mock repositories.

---

## 3. Unit of Work (Factory)

**Where:** `CommonUnitOfWorkRepositoryFactory`

**Implementation:** Lazy-creates repositories sharing one `GlassPanelContext` per DI scope. Not classic UoW with transaction — read-only aggregation.

**Why:** Ensures single DbContext per operation; centralizes repository access.

---

## 4. Strategy

**Where:** `RuleEngine/CustomRuleEngine/Operators/`

**Implementation:** Each operator class (e.g., `IsOneOfStringOperator`, `RangeNumberOperator`) implements evaluation for a specific `(Operator, IdOrField)` combination. `RuleEngine.EvaluateOperator` dispatches based on field and operator type.

**Why:** Extensible rule evaluation without modifying core engine logic.

---

## 5. Options Pattern

**Where:** `Common/Settings/`, `Domain/Settings/`, `Startup.cs`

**Implementation:** `services.Configure<T>(Configuration.GetSection(...))` → `IOptions<T>` injection.

**Why:** Strongly-typed, testable configuration binding.

---

## 6. Adapter

**Where:** `EAV/ServicesReferences/MatchingEngineServiceClient`

**Implementation:** Wraps WCF `IMatchingService` proxy behind `IMatchingEngineService` interface with configurable endpoint.

**Why:** Isolates legacy WCF integration from domain logic.

---

## 7. Facade

**Where:** `AdMatchingService`, `AdsService`

**Implementation:** Single entry point hiding handler chain, cache, and engine complexity.

**Why:** Simplifies gRPC layer and consumer integration.

---

## 8. Singleton + Scoped Composition

**Where:** DI registration in Startup

**Implementation:** Stateless services (RuleEngine, CacheService) as singleton; request-scoped handlers and engines.

**Why:** Performance (avoid re-instantiation) while maintaining request isolation.

---

## 9. Template Method (Partial)

**Where:** Handler chain — each handler follows: process → call Next

**Implementation:** Implicit via `Next.Handle(model)` at end of each handler.

---

## 10. Factory Method

**Where:** `ChainConfigurator.ConfigureType`

**Implementation:** Expression tree compilation creates handler instances with injected `Next` reference.

**Why:** Automates chain wiring without manual constructor chaining.

---

## 11. Decorator (Partial)

**Where:** `GrpcExceptionInterceptor`

**Implementation:** Wraps gRPC call pipeline with try/catch logging.

**Note:** Incomplete — should throw RpcException rather than return null.

---

## 12. Builder

**Where:** `ResponseBuilder`, `EddyAdsListingService` (vendor ad mapping)

**Implementation:** Constructs complex `AdsMatched` / `EddyVendorAd` objects from multiple source entities.

---

## Patterns NOT Used (but could apply)

| Pattern | Opportunity |
|---------|-------------|
| Mediator | Could replace chain with MediatR pipeline behaviors |
| Specification | Could formalize targeting rule predicates |
| CQRS | Read-only data layer already separates reads; no writes |
| Event Driven | Cache invalidation could use pub/sub instead of timer |
| Circuit Breaker | WCF calls lack resilience pattern |
| Observer | No event notification on cache refresh |

---

## Anti-Patterns Observed

| Anti-Pattern | Location |
|--------------|----------|
| God class tendencies | CommonDataManager (loads everything) |
| Anemic domain | Entities are POCOs; logic in handlers/engines |
| Magic strings | Cache keys, parameter names |
| Shotgun surgery | Adding handler requires Startup + new class + tests |
