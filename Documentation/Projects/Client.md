# Client (EDDY.IS.Examples.ClientNet)

## Purpose

**Sample gRPC-Web client** — ASP.NET MVC 5 (.NET Framework 4.6.1) demonstrating how to call the Ad Matching Service.

## Responsibilities

- MVC scaffold (Home, About, Contact views)
- gRPC-Web call to `AdMatchingServiceInvoke` on Home/Index
- Proto compilation for client stubs

## Dependencies

| Type | References |
|------|------------|
| NuGet | Grpc.Net.Client, Grpc.Net.Client.Web, Grpc.Tools, Google.Protobuf |
| Framework | .NET Framework 4.6.1, ASP.NET MVC 5 |

## Important Classes

| Class | Purpose |
|-------|---------|
| HomeController | Makes gRPC-Web call on Index action |
| RouteConfig, BundleConfig | Standard MVC setup |

## How It Calls the Service

```csharp
// HomeController.cs:16-43
var handler = new SubdirectoryHandler(new HttpClientHandler(), "/EDDY.IS.AdMatching.Service");
var webHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, handler);
var channel = GrpcChannel.ForAddress("https://gp15-issvc1.eddycorp.local:4443", ...);
var client = new Ads.AdsClient(channel);
var response = client.AdMatchingServiceInvoke(request);
```

## Configuration

- `Web.config` — standard MVC, no service URLs
- Hardcoded service URL in HomeController
- `Cert/EDCertificate.pfx` present but not referenced in code

## Proto Gap

Client proto is **older subset** — missing `EddyAdsListingServiceInvoke` and several request fields.

## Potential Improvements

- Move service URL to Web.config
- Update proto to match server
- Display response in view (currently discarded)
- Upgrade to .NET 6+ for consistency
- Add EddyAdsListing example call

## Folder Structure

| Folder | Purpose |
|--------|---------|
| Controllers/ | HomeController |
| Views/ | MVC views |
| Protos/ | gRPC client stubs |
| App_Start/ | MVC configuration |
| Cert/ | Client certificate (unused) |

## Note on Client.csproj

A second `Client/Client.csproj` exists — appears to be legacy/alternate project file.
