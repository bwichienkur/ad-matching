# Testing Documentation

## Test Projects

### EDDY.IS.AdMatching.Core.Tests

| Property | Value |
|----------|-------|
| Framework | MSTest 2.2.7 |
| Coverage tool | coverlet.collector 3.1.0 |
| References | Core, Service, RuleEngine |

#### Test Categories

| Folder | Tests |
|--------|-------|
| RequestHandlerTests/ | SchedulingHandler, ResponseBuilderHandler, RuleEngineHandler, StopsHandler, ParametersHandler, DynamicBidVariablesHandler, AdSortingHandler, CapsHandler |
| Engines/ | RuleEngine binary/unary/multiple input operators, CommonEngine |
| CommonEngineTests/ | Engine integration with mocks |
| LogAndAnalyzeHandlers/ | Performance analysis wrappers for handlers |
| Utilities/ | QueryBuilderGenerator, SourceValuesGenerator, TestingUtilities |
| Mocks/ | MemoryCacheMock, CacheEntryMock, MockDataManager |

**Strengths:** Good handler-level unit test coverage; rule engine operator tests.

**Gaps:** No CommonDataManager tests; no CacheService integration tests; no end-to-end gRPC tests.

---

### EDDY.IS.Test.AdMatchingServiceTest

| Property | Value |
|----------|-------|
| Framework | NUnit 3.13.2 |
| References | Client project only |

| Test | File | Coverage |
|------|------|----------|
| AdsServiceTests | AdsServiceTests.cs | Minimal — references client |

**Note:** NUnit also referenced in Service csproj (unusual for web project).

---

## Mocking Strategy

| Mock | Purpose |
|------|---------|
| MockDataManager | Returns canned DictionaryContainer |
| RuleEngineMock | Controlled Pass/Fail for handler tests |
| MemoryCacheMock | In-memory cache without Redis |
| LogAndAnalyze handlers | Wrap real handlers with stopwatch logging |

No Moq/NSubstitute/FakeItEasy detected — custom mocks and manual stubs.

---

## Coverage Assessment

| Area | Coverage | Confidence |
|------|----------|------------|
| Handler chain | Good | High |
| Rule engine operators | Good | High |
| CommonEngine caching | Partial | Medium |
| AdMatchingService orchestration | Minimal | Medium |
| gRPC AdsService | Minimal | High |
| EAV/WCF integration | None | High |
| CacheBackgroundService | None | High |
| CommonDataManager | None | High |

**Overall estimated coverage: 30-40%** (inferred from file counts — no coverage report in repo).

---

## Missing Tests (Recommended)

| Priority | Test Type | Target |
|----------|-----------|--------|
| High | Integration | AdMatchingService end-to-end with mock IDataManager |
| High | Unit | CacheService Redis fallback behavior |
| High | Unit | CommonDataManager rule JSON parsing |
| Medium | Integration | gRPC AdsService with TestServer |
| Medium | Unit | ParametersEvaluator macro substitution |
| Medium | Unit | EddyAdsListingService with mock WCF |
| Low | Load | Cache refresh under concurrent requests |

---

## Running Tests

```bash
# Core tests (MSTest)
dotnet test EDDY.IS.AdMatching.Core.Tests/EDDY.IS.AdMatching.Core.Tests.csproj

# Service tests (NUnit)
dotnet test EDDY.IS.Test.AdMatchingServiceTest/EDDY.IS.Test.AdMatchingServiceTest.csproj
```

---

## Recommendations

1. Standardize on single test framework (xUnit recommended for .NET 6)
2. Remove NUnit/Test SDK from Service csproj — test packages belong in test projects only
3. Add WebApplicationFactory integration tests for gRPC
4. Add coverlet coverage gate in CI (target 60%+ on Core)
5. Mock WCF client for EAV unit tests
6. Add snapshot tests for rule engine complex trees
