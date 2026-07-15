# GlassPanelContext & Repository Reference

## DbContext: GlassPanelContext

**File:** `EDDY.IS.AdMatching.Data/Context/GlassPanelContext.cs`

### Configuration

```csharp
// Constructor — line 7-13
this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
```

```csharp
// Startup registration — Startup.cs:62-63
services.AddDbContext<GlassPanelContext>(
    options => options.UseSqlServer(Configuration.GetConnectionString("GlassPanelConnection")));
```

### DbSet Inventory (93 total)

#### Table-Mapped Entities

| DbSet | Entity | Notes |
|-------|--------|-------|
| Ads | Ad | |
| AdGroups | AdGroup | |
| AdGroupAds | AdGroupAd | |
| AdMatchingServiceParameters | AdMatchingServiceParameter | |
| AdProviders | AdProvider | |
| AdTypes | AdType | |
| Campaigns | Campaign | Maps to **view** VW_CampaignAMS |
| CampaignCapReasons | CampaignCapReason | |
| CampaignCapTypes | CampaignCapType | |
| CampaignLevels | CampaignLevel | |
| CampaignRelationships | CampaignRelationship | |
| CampaignSchedules | CampaignSchedule | |
| CampaignSources | CampaignSource | |
| CampaignSpends | CampaignSpend | |
| CampaignStops | CampaignStop | |
| CampaignTypes | CampaignType | |
| ClientAdAccounts | ClientAdAccount | |
| ClientAdAccountBudgets | ClientAdAccountBudget | |
| ClientAdAccountDefaultParameters | ClientAdAccountDefaultParameter | |
| ClientAdAccountParameters | ClientAdAccountParameter | |
| ClientAdAccountSpends | ClientAdAccountSpend | |
| ClientAdAccountStops | ClientAdAccountStop | |
| ConversionPixels | ConversionPixel | |
| ConversionPixelTypes | ConversionPixelType | |
| LineItems | LineItem | |
| LineItemSubSources | LineItemSubSource | |
| Permissions | Permission | Unused by AMS runtime |
| PermissionRoles | PermissionRole | Unused by AMS runtime |
| Placements | Placement | |
| PlacementGroups | PlacementGroup | |
| ProductTypes | ProductType | |
| ReportSchedules | ReportSchedule | Unused by AMS runtime |
| ReportScheduleEmails | ReportScheduleEmail | Unused |
| ReportScheduleFrequencies | ReportScheduleFrequency | Unused |
| ReportTypes | ReportType | Unused |
| ScheduleOptions | ScheduleOption | |
| Sources | Source | |
| SourceClientAccess | SourceClientAccess | |
| SourceGroups | SourceGroup | |
| SourceProductTypes | SourceProductType | |
| SourceGroupSources | SourceGroupSource | |
| SubSources | SubSource | |
| SubSourcePixelClientAdAccounts | SubSourcePixelClientAdAccount | |
| TargetingRules | TargetingRule | RuleJson → parsed at load |
| TimeZones | TimeZone | |
| StateTimeZones | StateTimeZone | Schema: Common |
| TrackingParameters | TrackingParameter | |

#### View-Mapped Entities (Keyless unless noted)

| DbSet | SQL View | Key |
|-------|----------|-----|
| SlimAds | VW_SlimAdsAMS | Has key (Id) |
| VwAdsAms | VW_AdsAMS | Has key |
| VwSourceByCampaigns | VW_SourceByCampaignAms | Keyless |
| VwAds | VW_Ad | Keyless |
| VwAdGroups | VW_AdGroup | Keyless |
| VwAdGroupAds | VW_AdGroupAds | Keyless |
| VwCampaigns | VW_Campaign | Keyless |
| VwClientAdAccounts | VW_ClientAdAccount | Keyless |
| VwSources | VW_Source | Keyless |
| VwSubSources | VW_SubSource | Keyless |
| VwRules* (18) | VW_Rules_* | Keyless lookup views |
| VwUsers, VwRoles, etc. | Various | Keyless, unused by AMS |

### Fluent FK Relationships (Explicit)

| From | To | Constraint Name |
|------|----|-----------------|
| CampaignSchedule | ScheduleOption | FK_CampaignSchedule_ScheduleOption |
| ConversionPixel | ConversionPixelType | FK_ConversionPixelType_ConversionPixel |
| LineItem | Placement | FK_LineItem_Placement |
| LineItemSubSource | LineItem, SubSource | FK_LineItemSubSource_* |
| PermissionRole | Permission | FK_PermissionRole_Permission |
| ReportSchedule | ReportScheduleFrequency | FK_ReportSchedule_ReportFrecuency |
| ReportScheduleEmail | ReportSchedule | FK_ReportScheduleEmail_ReportSchedule |
| SourceGroupSource | SourceGroup, Source | FK_SourceGroupSource_* |
| SubSource | AdProvider, Source | FK_SubSource_* |
| SubSourcePixelClientAdAccount | SubSource | FK__SubSource__SubSo__* |

## Repository Pattern

### Generic Base

```csharp
// GenericReadOnlyRepository<TEntity> — Data/Repositories/
GetAll(), Get(filter, orderBy, includeProperties), GetByID(id)
// Async variant: GenericAsyncReadOnlyRepository<TEntity>
```

**No SaveChanges** — read-only by design. `IGenericRepository<T>` interface exists but has **no implementation**.

### Unit of Work Factory

`CommonUnitOfWorkRepositoryFactory` — lazy-instantiates repositories sharing one `GlassPanelContext` per scope.

### Implemented Repositories

| Interface | Implementation | Entity |
|-----------|---------------|--------|
| ICampaignRepository | CampaignRepository | Campaign |
| IAdGroupRepository | AdGroupRepository | AdGroup |
| IAdGroupAdRepository | AdGroupAdRepository | AdGroupAd |
| IClientAdAccountRepository | ClientAdAccountRepository | ClientAdAccount |
| ICampaignSourceRepository | CampaignSourceRepository | CampaignSource |
| ICampaignScheduleRepository | CampaignScheduleRepository | CampaignSchedule |
| IScheduleOptionRepository | ScheduleOptionRepository | ScheduleOption |
| ICampaignStopRepository | CampaignStopRepository | CampaignStop |
| IClientAccountStopRepository | AccountStopRepository | ClientAdAccountStop |
| IAdsAMSRepository | AdsAMSRepository | VwAdsAm |
| ISlimAdsAMSRepository | SlimAdsAMSRepository | SlimAd |
| IClientAdAccountDefaultParameterRepository | ClientAdAccountDefaultParameterRepository | ClientAdAccountDefaultParameter |
| IClientAdAccountParameterRepository | ClientAdAccountParameterRepository | ClientAdAccountParameter |
| ICampaignRelationshipRepository | CampaignRelationshipRepository | CampaignRelationship |
| ITargetingRuleRepository | TargetingRuleRepository | TargetingRule |
| ITimeZoneRepository | TimeZoneRepository | TimeZone |
| IStateTimeZoneRepository | StateTimeZoneRepository | StateTimeZone |
| ISourceProductTypeRepository | SourceProductTypeRepository | SourceProductType |
| IClientAdAccountBudgetRepository | ClientAdAccountBudgetRepository | ClientAdAccountBudget |
| ISourceByCampaignRepository | SourceByCampaignRepository | VwSourceByCampaignAms |
| IAdRepository | AdRepository | Ad |

### Unimplemented Repository Interfaces

- ICampaignSpendRepository
- IClientAdAccountSpendRepository
- IAdMatchingServiceParametersRepository
- ICampaignCapsRepository
- IUsZipCodeRepository

## CommonDataManager Load Order

1. VwSourceByCampaignAms → CampaignsBySource
2. CampaignSource, AdGroup, AdGroupAd, Campaign, ClientAdAccount
3. ClientAdAccountParameter, ClientAdAccountDefaultParameter
4. TargetingRule (+ JSON parse)
5. CampaignSchedule, ScheduleOption
6. ClientAdAccountStop, CampaignStop
7. CampaignRelationship, TimeZone, StateTimeZone
8. VwAdsAm, SlimAd, SourceProductType, ClientAdAccountBudget

**Evidence:** `CommonDataManager.cs:31-170`
