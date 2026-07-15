# EDDY.IS.AdMatching.Core.Tests

## Purpose

**Unit tests** for handler chain, rule engine operators, and CommonEngine caching behavior.

## Responsibilities

- Handler-level unit tests (8 handlers)
- Rule engine operator tests (binary, unary, multiple input)
- CommonEngine cache integration tests
- Mock infrastructure (MockDataManager, MemoryCacheMock)
- Performance analysis wrappers (LogAndAnalyzeHandlers)

## Dependencies

| Type | References |
|------|------------|
| Projects | Core, Service, RuleEngine |
| NuGet | MSTest 2.2.7, coverlet 3.1.0, Enums.NET 4.0.0 |

## Test Structure

| Folder | Tests |
|--------|-------|
| RequestHandlerTests/ | Per-handler test classes |
| Engines/ | RuleEngine operator tests |
| CommonEngineTests/ | Engine with mocks |
| LogAndAnalyzeHandlers/ | Performance timing wrappers |
| Mocks/ | MemoryCacheMock, CacheEntryMock |
| Utilities/ | Test data generators |

## Mocking Strategy

Custom mocks — no Moq/NSubstitute. `MockDataManager`, `RuleEngineMock`.

## Potential Improvements

- Add CommonDataManager tests
- Add CacheService integration tests with Testcontainers Redis
- Increase coverage on ParametersEvaluator

## Coverage

Estimated 30-40% of Core project. Strong on handlers and rule engine.

See [Testing.md](../Testing.md).
