# EDDY.IS.AdMatching.EAV

## Purpose

**External integration adapter** — connects to legacy Matching Engine WCF service and transforms institution matches into Eddy vendor ad listings.

## Responsibilities

- `EddyAdsListingService` — main listing orchestrator
- `MatchingServiceClient` — WCF proxy with configurable endpoint
- Build DirectoryMatchRequest from visitor parameters
- Map institutions to EddyVendorAd with logos, URLs, programs
- DI registration via `Config.RegisterDependencies`

## Dependencies

| Type | References |
|------|------------|
| Projects | Domain |
| NuGet | System.ServiceModel.* 4.8.1 (Http, Primitives, Security) |

## Important Classes

| Class | Purpose |
|-------|---------|
| EddyAdsListingService | Listing orchestrator |
| MatchingServiceClient | WCF adapter (partial class) |
| Config | DI registration |
| EddyAdsListingRequest/Response | EAV DTOs |
| IEddyAdsListingService | Interface |

## Configuration

- `MatchingEngineService:Endpoint` — WCF service URL
- `EAVSetting` — logo sizes, URL templates

## External Services

| Service | Protocol | Endpoint |
|---------|----------|----------|
| Matching Engine | WCF/SOAP | `http://gp15-issvc1/.../MatchingService.svc` |

## Connected Services

Auto-generated WCF proxy in `Connected Services/EDDY.IS.AdMatching.EAV.MatchingEngineService/Reference.cs` (~10,800 lines).

## Potential Improvements

- Add retry/circuit breaker (Polly)
- Use HTTPS for WCF endpoint
- Add timeout configuration
- Mock-friendly integration tests

## Folder Structure

| Folder | Purpose |
|--------|---------|
| Services/ | EddyAdsListingService |
| ServicesReferences/ | WCF client partial |
| Interface/ | IEddyAdsListingService |
| Models/ | Request/response DTOs |
| Connected Services/ | WCF generated code |
