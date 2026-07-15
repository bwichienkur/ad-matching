# EDDY.IS.Test.AdMatchingServiceTest

## Purpose

**Integration test project** for the Ad Matching Service — minimal coverage.

## Responsibilities

- `AdsServiceTests` — basic service test referencing Client project

## Dependencies

| Type | References |
|------|------------|
| Projects | Client (EDDY.IS.Examples.ClientNet) only |
| NuGet | NUnit 3.13.2, NUnit3TestAdapter, Microsoft.NET.Test.Sdk |

## Assessment

**Minimal value** — references Client project rather than Service directly. Does not appear to use WebApplicationFactory or gRPC TestServer.

## Potential Improvements

- Reference Service project directly
- Add gRPC integration tests with TestServer
- Add WebApplicationFactory-based tests
- Consolidate with Core.Tests under single framework

## Coverage

Near zero meaningful integration coverage.

See [Testing.md](../Testing.md).
