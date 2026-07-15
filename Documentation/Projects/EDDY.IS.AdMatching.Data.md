# EDDY.IS.AdMatching.Data

## Purpose

**Data access infrastructure** — EF Core DbContext, generic repositories, unit-of-work factory, and `CommonDataManager` data loader.

## Responsibilities

- `GlassPanelContext` — 93 DbSets, fluent configuration
- Generic read-only repositories
- 20 concrete repository implementations
- `CommonUnitOfWorkRepositoryFactory` — lazy repo creation
- `CommonDataManager` — loads full DictionaryContainer from DB
- TargetingRule JSON parsing extensions

## Dependencies

| Type | References |
|------|------------|
| Projects | Domain, Entities, Repositories, Common |
| NuGet | EF Core 6.0 SqlServer, Configuration 6.0 |

## Important Classes

| Class | Purpose |
|-------|---------|
| GlassPanelContext | EF Core DbContext (NoTracking) |
| CommonDataManager | Full data load for cache |
| CommonUnitOfWorkRepositoryFactory | Repository factory |
| GenericReadOnlyRepository<T> | Base read repo |
| TargetingRuleExtensions | RuleJson → QueryBuilderFilterRule |

## Configuration

Connection string `GlassPanelConnection` injected via Startup.

## External Services

SQL Server `GlassPanel` database only.

## Potential Improvements

- Parallelize repository loads in CommonDataManager
- Implement missing repository interfaces or remove them
- Add incremental refresh instead of full table scans
- Extract repetitive `.GetAll(x => x.IsEnabled && !x.IsDeleted)` pattern

## Folder Structure

| Folder | Purpose |
|--------|---------|
| Context/ | GlassPanelContext |
| Repositories/ | Concrete repository implementations |
| Infrastructure/ | UnitOfWork factory |
| Extensions/ | TargetingRule parsing |
