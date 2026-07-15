# EDDY.IS.AdMatching.Entities

## Purpose

**EF Core entity POCOs** — table and view mappings for the GlassPanel database.

## Responsibilities

- 40+ table entity classes
- 46+ view entity classes (Vw*)
- 18 rule lookup view entities (VwRules*)
- Navigation properties (sparse)
- SlimAd entity for VW_SlimAdsAMS

## Dependencies

| Type | References |
|------|------------|
| Projects | Common |
| NuGet | None beyond SDK |

## Important Classes

Runtime-critical: `SlimAd`, `VwAdsAm`, `Campaign`, `TargetingRule`, `ClientAdAccount`, `CampaignSource`, `CampaignSchedule`, `CampaignStop`, `ClientAdAccountStop`, `CampaignRelationship`.

See [Entities/EntityCatalog.md](../Entities/EntityCatalog.md).

## Configuration

None — pure POCOs configured in GlassPanelContext.

## Potential Improvements

- Remove unused entity classes not in DbContext
- Add XML documentation on key entities
- Consider splitting view entities from table entities into subfolders

## Folder Structure

Flat — all entities in project root directory.

## Entities Not in DbContext

AdHistoric, AdImage, AdImageHistoric, AdImageSizeType, CampaignDestinationUrl, VwStandardReport — likely legacy scaffold artifacts.
