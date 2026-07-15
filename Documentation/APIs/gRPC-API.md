# gRPC API Documentation

All endpoints are exposed via **gRPC** with **gRPC-Web** enabled. No REST/HTTP controllers exist.

**Host configuration:** Kestrel HTTP/1+HTTP2 (`appsettings.json:11-13`), gRPC-Web middleware (`Startup.cs:104-109`).

**Authentication:** None (high confidence).

---

## Service: Greeter

**Proto:** `Protos/greet.proto`  
**Implementation:** `Services/GreeterService.cs`  
**Purpose:** Connectivity/demo health check.

### RPC: SayHello

| Property | Value |
|----------|-------|
| Method | Unary |
| URL | `/Greeter/SayHello` (gRPC path) |
| Authentication | None |

**Request:** `HelloRequest`
- `name` (string)

**Response:** `HelloReply`
- `message` (string) — `"Hello {name}"`

**Validation:** None  
**Error handling:** Standard gRPC exceptions; intercepted by `GrpcExceptionInterceptor`  
**Business logic:** String concatenation only (`GreeterService.cs:15-21`)

---

## Service: Ads

**Proto:** `Protos/AdsGreet.proto`  
**Implementation:** `Services/AdsService.cs`  
**Package:** `Ads`

---

### RPC: AdMatchingServiceInvoke

| Property | Value |
|----------|-------|
| Method | Unary |
| Authentication | None |
| APM | New Relic `[Transaction]` |

#### Request: `getAdMatchingRequest`

| Field | Type | Required | Maps To |
|-------|------|----------|---------|
| Parameters | map<string,string> | Yes | `AdMatchingRequest.Parameters` |
| PlacementGuid | string | No | **Not mapped** to domain DTO |
| SearchGuid | string | Yes (logging) | `AdMatchingRequest.SearchGuid` |
| SessionGuid | string | No | **Not mapped** |
| MaxAds | int32 | Yes | `AdMatchingRequest.MaxAds` |
| SourceId | int32 | Yes | `AdMatchingRequest.SourceId` |
| MarketingUnit | int32 | No | **Not mapped** (may be in Parameters) |
| Channel | int32 | No | **Not mapped** |
| SubChannel | int32 | No | **Not mapped** |
| StaticAdGuid | string | No | `AdMatchingRequest.StaticAdGuid` |
| PreExcludeInstitutions | repeated string | No | `AdMatchingRequest.PreExcludeInstitutions` |

**Mapping evidence:** `AdsService.GetAdMatchingRequestFrom` (`AdsService.cs:199-210`) — only subset of proto fields mapped.

#### Response: `getAdMatchingResponse`

| Field | Type | Description |
|-------|------|-------------|
| message | string | Status/error message |
| adsReturned | int32 | Count of ads |
| adsMatched | repeated AdsMatchedResponse | Ad list |

**AdsMatchedResponse fields:** adId, adName, adDescription, adHeader, adClickUrl, adDisplayUrl, campaignId, institutionName, image URLs (original/small/medium/large), realCPC, boostedCPC, clientAdAccountId, adGroupId, popularPrograms, productTypeId, clientToken, CRId

**Mapping evidence:** `AdsService.GetgetAdMatchingResponse` (`AdsService.cs:98-132`)

#### Validation

- No explicit server-side validation beyond GUID parse for static ads
- Invalid/missing SourceId may yield empty results from cache

#### Error Handling

```csharp
// AdsService.cs:53-59
catch (Exception ex) {
    response.Message = "There is an error: " + ex.Message;
}
```

Performance logged in `finally` via `PerformanceLogger`.

#### Business Logic Dependencies

`IAdMatchingService` → `IEngine` → `IChainHandler<AdMatchingModel>` → `IRuleEngine`

#### Example Flow

See [BusinessProcesses.md](../BusinessProcesses.md) Process 1.

---

### RPC: AdStaticMatchingServiceInvoke

| Property | Value |
|----------|-------|
| Status | **NOT IMPLEMENTED** |
| Proto | Defined in `AdsGreet.proto:11` |
| Implementation | Missing from `AdsService.cs` |

Static matching is handled via `StaticAdGuid` field on `AdMatchingServiceInvoke` instead.

**Confidence:** High — no override in AdsService.

---

### RPC: EddyAdsListingServiceInvoke

| Property | Value |
|----------|-------|
| Method | Unary |
| Authentication | None |

#### Request: `getEddyAdsListingRequest`

| Field | Type | Maps To |
|-------|------|---------|
| MaxAds | int32 | EddyAdsListingRequest |
| TrackId | string | ✓ |
| PlacementViewGuid | string | ✓ |
| MaxPrograms | int32 | ✓ |
| ConversionRate | double | ✓ |
| WidgetName | string | ✓ |
| WidgetRequestGuid | string | ✓ |
| Parameters | map<string,string> | ✓ |
| ApplicationId | int32 | ✓ |
| DuplicateForInstitutionList | repeated string | ✓ |
| LeadInitiatingUrl | string | ✓ |

**Mapping:** `GetEddyAdsListingRequestFrom` (`AdsService.cs:212-221`)

#### Response: `getEddyAdsListingResponse`

| Field | Type |
|-------|------|
| Success | bool |
| AdsCount | int32 |
| Ads | repeated EddyVendorAd |
| MatchingEngineGuid | string |
| MatchingEngineCount | int32 |
| PreMatchCount | int32 |
| Message | string |

**EddyVendorAd** includes: institutionName, ClickURL, Header, Description, image URLs, CPC, EstimatedRevShare, Pixel, AdLinks, Programs, Address, CampusName, CampusType, CRId

#### Error Handling

```csharp
// AdsService.cs:82-89
Success = false, Message = "There is an error: " + ex.Message
```

#### Business Logic Dependencies

`IEddyAdsListingService` → `IMatchingEngineService` (WCF) → External Matching Engine

---

## Service: Campaings (Catalog)

**Proto:** `Protos/CatalogGreet.proto`  
**Status:** **NOT IMPLEMENTED** — endpoint mapping commented out (`Startup.cs:110`)

Defined RPCs (unimplemented):
- `Campaings.getCampaings`
- `adGroup.getAdGroup`
- `Account.getAccount`

---

## Client Integration Notes

Sample client (`Client/Controllers/HomeController.cs`):
- Uses **gRPC-Web Text** mode
- `SubdirectoryHandler` for path prefix `/EDDY.IS.AdMatching.Service`
- Target: `https://gp15-issvc1.eddycorp.local:4443`

Proto version in Client is **older subset** — missing EddyAdsListing RPC and some request fields.

---

## gRPC Interceptor

**Class:** `Logging/GrpcExceptionInterceptor.cs`

| Behavior | Issue |
|----------|-------|
| Logs exception | OK |
| Returns `null` on failure | Clients may receive empty/null response instead of gRPC error status |

**Recommendation:** Use `RpcException` with `StatusCode.Internal` instead of returning null.

---

## Environment URLs

| Environment | URL (from launchSettings) |
|-------------|---------------------------|
| Development | http://localhost:6080, https://localhost:60443 |
| Staging | http://ams.gp15.educationdynamics.local |
| Dryrun | http://ams.dryrun.educationdynamics.local |
| Production | http://ams.educationdynamics.com |
