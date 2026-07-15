# EDDY.IS.AdMatching.Domain

## Purpose

**Application/domain layer** — orchestration service, DTOs, domain interfaces, business entity models, and chain handler interfaces.

## Responsibilities

- `AdMatchingService` orchestrator
- Request/response DTOs
- Domain interfaces: `IAdMatchingService`, `IEngine`, `IDataManager`, `ICacheService`
- Business entities: `DictionaryContainer`, `FilteredContainerDictionary`, `AdsMatched`
- Chain interfaces: `IChainHandler<T>`, `IChainConfigurator<T>`
- Settings: `EAVSettings`, `MatchingEngineServiceSettings`

## Dependencies

| Type | References |
|------|------------|
| Projects | Entities |
| NuGet | NewRelic.Agent.Api 10.0 |

## Important Classes

| Class | Purpose |
|-------|---------|
| AdMatchingService | Main orchestrator |
| AdMatchingModel | Pipeline state object |
| AdMatchingRequest/Response | Public API DTOs |
| DictionaryContainer | In-memory DB snapshot |
| FilteredContainerDictionary | Cached/filtered state |
| AdsMatched | Output ad representation |

## Configuration

Settings classes with static `SectionName` properties for Options binding.

## Potential Improvements

- Remove direct dependency on Entities (use domain models)
- Add validation attributes or FluentValidation on DTOs
- Fix typo "campaings" in response message

## Folder Structure

| Folder | Purpose |
|--------|---------|
| Services/ | AdMatchingService + interfaces |
| Dto/ | Request/response DTOs |
| Models/ | AdMatchingModel pipeline state |
| BusinessEntities/ | Dictionary containers, AdsMatched |
| ChainResponsability/ | Handler interfaces |
| Settings/ | EAV and ME settings classes |
