# EDDY.IS.AdMatching.Repositories

## Purpose

**Repository interface definitions** — contracts for data access, implemented in the Data project.

## Responsibilities

- Generic repository interfaces (`IGenericReadOnlyRepository`, `IGenericAsyncReadOnlyRepository`, `IGenericRepository`)
- 25+ entity-specific repository interfaces
- `ICommonUnitOfWorkRepositoryFactory` — aggregates all repo interfaces

## Dependencies

| Type | References |
|------|------------|
| Projects | Entities only |

## Important Interfaces

| Interface | Implemented | Entity |
|-----------|-------------|--------|
| ICommonUnitOfWorkRepositoryFactory | Yes | All repos |
| ICampaignRepository | Yes | Campaign |
| ISlimAdsAMSRepository | Yes | SlimAd |
| IAdsAMSRepository | Yes | VwAdsAm |
| ITargetingRuleRepository | Yes | TargetingRule |
| ICampaignSpendRepository | **No** | CampaignSpend |
| IClientAdAccountSpendRepository | **No** | ClientAdAccountSpend |
| ICampaignCapsRepository | **No** | — |
| IUsZipCodeRepository | **No** | — |
| IAdMatchingServiceParametersRepository | **No** | — |

## Potential Improvements

- Remove unimplemented interfaces or implement them
- Split factory into smaller interfaces (ISP)
- Move interfaces closer to Data project (single project)

## Folder Structure

| Folder | Purpose |
|--------|---------|
| Interfaces/ | All repository interfaces |
