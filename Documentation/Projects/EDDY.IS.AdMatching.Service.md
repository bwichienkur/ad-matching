# EDDY.IS.AdMatching.Service

## Purpose

ASP.NET Core **gRPC host** and **composition root** for the Ad Matching Service. Exposes ad matching and Eddy listing endpoints, configures DI, runs background cache refresh.

## Responsibilities

- gRPC service endpoints (Ads, Greeter)
- DI container configuration
- Middleware pipeline (routing, gRPC-Web)
- Background cache warming
- NLog initialization

## Dependencies

| Type | References |
|------|------------|
| Projects | Core, Data, EAV, Common |
| Key NuGet | Grpc.AspNetCore 2.40, StackExchangeRedis 6.0, NLog.Web 5.1, NUnit (should not be here) |

## Important Classes

| Class | File | Purpose |
|-------|------|---------|
| Program | Program.cs | Entry point, NLog bootstrap |
| Startup | Startup.cs | DI + middleware |
| AdsService | Services/AdsService.cs | Main gRPC service |
| GreeterService | Services/GreeterService.cs | Demo/health |
| CacheBackgroundService | CacheBackgroundService.cs | Periodic cache refresh |
| GrpcExceptionInterceptor | Logging/GrpcExceptionInterceptor.cs | gRPC error logging |

## Configuration

- `appsettings.json`, `appsettings.Development.json`
- `nlog.config`, `nlogPerformance.config`
- `Properties/launchSettings.json` — environment profiles
- User Secrets ID: `14bd8161-32ed-476c-9941-17d54c0b90a2`

## External Services

Redis, GlassPanel SQL, EddyLogging SQL, Matching Engine (via EAV)

## Potential Improvements

- Fix Dockerfile to net6.0
- Remove NUnit from web csproj
- Add authentication middleware
- Implement health checks
- Remove unused certificate auth package

## Folder Structure

| Folder | Purpose |
|--------|---------|
| Services/ | gRPC service implementations |
| Protos/ | gRPC contract definitions |
| Logging/ | gRPC interceptor |
| Properties/ | Launch profiles, service dependencies |
| BackgroundServices/ | Empty placeholder folder |
