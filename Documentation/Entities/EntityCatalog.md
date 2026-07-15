# Entity Catalog

Entities live in `EDDY.IS.AdMatching.Entities`. All mapped through `GlassPanelContext`.

## Runtime-Critical Entities

### SlimAd → VW_SlimAdsAMS

| Property | Type | Business Meaning |
|----------|------|------------------|
| Id | int | Surrogate key |
| ClientAdAccountId | int | Advertiser account |
| CampaignId | int | Campaign |
| AdGroupId | int | Ad group |
| AdId | int | Ad |
| SourceId | int | Publisher source |
| SourceBid | decimal | Bid for this source |
| AdKey | string [NotMapped] | Computed composite key |

**Purpose:** Denormalized matching row — primary input to handler chain filtering.

### VwAdsAm → VW_AdsAMS

Full ad creative and metadata for response building: names, descriptions, URLs, image URLs, CPC, institution name, client token, popular programs, etc.

**Evidence:** `VwAdsAm.cs` (properties used in ResponseBuilder).

### Campaign → VW_CampaignAMS

| Property | Business Meaning |
|----------|------------------|
| CampaignId | Primary key |
| ClientAdAccountId | Owner account |
| ProductTypeId | Product classification |
| TimeZoneId | Schedule timezone |
| StopsTimeZoneId | Stop window timezone |
| IsCapped | Budget cap reached |
| IsEnabled / IsDeleted | Active flag / soft delete |

**Note:** Mapped to **view** not base table (`GlassPanelContext.cs:224-232`).

### TargetingRule

| Property | Business Meaning |
|----------|------------------|
| TargetingRuleId | PK |
| RuleJson | Raw JSON rule (QueryBuilder format) [JsonIgnore] |
| RuleAsQueryBuilderFilterRule | Parsed rule [NotMapped] |
| CampaignId / AdGroupId | Rule scope |
| IsDynamicBid | Bid boost vs filter rule |
| IsEnabled / IsDeleted | Active flags |

**Lifecycle:** Loaded at cache refresh; JSON parsed via `CreateAndAssignRuleAsQueryBuilderFilterRule` extension.

### ClientAdAccount

| Property | Business Meaning |
|----------|------------------|
| ClientAdAccountId | PK |
| InstitutionAlias | Institution identifier for pre-exclusion |
| Status | Account status |
| IsEnabled / IsDeleted | Active flags |

### CampaignSource

Links campaigns to publisher sources with optional `BidMultiplier`.

### CampaignSchedule + ScheduleOption

Schedule windows with day-of-week, start/end time, optional bid boost, `DisableAds` flag.

### CampaignStop / ClientAdAccountStop

Temporary pause windows with timezone-aware begin/end.

### CampaignRelationship

Parent/child campaign linking with flags: LinkParentSchedule, LinkParentSources, LinkParentCaps (inferred from property names).

## Lookup View Entities (VwRules*)

18 keyless views providing valid values for rule engine fields:

| Entity | View | Rule Field |
|--------|------|------------|
| VwRulesState | VW_Rules_State | State |
| VwRulesAreaOfStudy | VW_Rules_AreaOfStudy | AreaOfStudy |
| VwRulesSubject | VW_Rules_Subject | Subject |
| VwRulesDegreeLevel | VW_Rules_DegreeLevel | DegreeLevel |
| VwRulesDeviceType | VW_Rules_DeviceType | DeviceType |
| VwRulesBrowserPlatform | VW_Rules_BrowserPlatform | BrowserPlatform |
| ... | ... | ... |

**Pattern:** Key, Value, Text columns. Used **(inferred)** by admin UI for rule building, not directly queried at runtime by AMS handlers.

## Domain Business Entities (Not EF)

| Class | Project | Purpose |
|-------|---------|---------|
| DictionaryContainer | Domain | Raw DB snapshot in memory |
| FilteredContainerDictionary | Domain | Source-filtered + shared cache state |
| FilteredDictionary | Domain | Mutable evaluation state |
| AdsMatched | Domain | Response DTO for matched ad |
| CampaignEvaluated | Domain | Stub/sparse usage |

## Validation

- EF entities have **no data annotations** for validation
- Business validation occurs in handlers and rule engine
- `IsEnabled && !IsDeleted` filtering at load time

## Navigation Properties

Sparse — most relationships are FK scalar columns only. Navigation exists on:
- CampaignSchedule → ScheduleOption
- CampaignSpend → Campaign
- SubSource → Source, AdProvider
- LineItem → Placement
- ConversionPixel → ClientAdAccount, ConversionPixelType

See [Database/Overview.md](../Database/Overview.md) ER diagram.

## Entities Excluded from DbContext

AdHistoric, AdImage, AdImageHistoric, AdImageSizeType, CampaignDestinationUrl, VwStandardReport — present as classes but not registered. **(Inferred:** legacy scaffold artifacts.)
