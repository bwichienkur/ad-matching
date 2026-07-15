# Deployment Documentation

## Build Process

### Prerequisites

- .NET 6.0 SDK
- SQL Server access (GlassPanel, EddyLogging)
- Redis instance (Azure Redis or compatible)
- Matching Engine WCF service (for EAV path)

### Build Command

```bash
dotnet build EDDY.IS.AdMatching.sln -c Release
```

### Publish

```bash
dotnet publish EDDY.IS.AdMatching.Service/EDDY.IS.AdMatching.Service.csproj -c Release -o ./publish
```

---

## Docker

**File:** `EDDY.IS.AdMatching.Service/Dockerfile`

| Stage | Image | Note |
|-------|-------|------|
| base | mcr.microsoft.com/dotnet/aspnet:5.0 | **Mismatch** — project targets net6.0 |
| build | mcr.microsoft.com/dotnet/sdk:5.0 | **Mismatch** |
| Exposed ports | 80, 443 | |
| Entrypoint | `dotnet EDDY.IS.AdMatching.Service.dll` | |

**Action required:** Update Dockerfile to `aspnet:6.0` and `sdk:6.0`.

Docker profile in `launchSettings.json:41-46` with `publishAllPorts: true`.

---

## CI/CD

**No CI/CD pipeline found in repository.**

No `.github/workflows/`, Azure Pipelines YAML, or similar configuration detected.

**(Inferred):** Deployment may be managed via Azure DevOps outside this repo (SCC markers in csproj suggest TFS/Azure DevOps source control).

---

## Infrastructure

| Component | Technology | Config |
|-----------|------------|--------|
| App host | Kestrel / IIS / Docker | launchSettings |
| Database | SQL Server (gp15-sql1) | ConnectionStrings |
| Cache | Azure Redis | Redis:Server |
| Logging DB | SQL Server (EddyLogging) | ConnectionStrings:EddyLogging |
| APM | New Relic Agent | Requires agent install on host |
| External ME | WCF on Windows service | MatchingEngineService:Endpoint |

---

## Environment Promotion

| Environment | ASPNETCORE_ENVIRONMENT | URL |
|-------------|------------------------|-----|
| Development | Development | localhost:6080/60443 |
| Staging | Staging | ams.gp15.educationdynamics.local |
| Dryrun | Dryrun | ams.dryrun.educationdynamics.local |
| Production | Production | ams.educationdynamics.com |

**Source:** `Properties/launchSettings.json`

### Configuration Differences

| Setting | Dev | Prod (inferred) |
|---------|-----|-----------------|
| Logging level | Debug/Trace | Warning+ |
| Redis server | Dev QA instance | Production Redis (not in repo) |
| SQL server | gp15-sql1 | Production SQL (not in repo) |
| ME endpoint | gp15-issvc1 | Production ME (not in repo) |
| Performance logging | Disabled | Environment-specific |

Only `appsettings.json` and `appsettings.Development.json` exist — no Staging/Production-specific JSON files. **(Inferred):** Production config injected via environment variables or deployment pipeline.

---

## IIS Deployment (Inferred)

Evidence suggesting IIS deployment:
- Client uses `SubdirectoryHandler` with path `/EDDY.IS.AdMatching.Service`
- `AllowedHosts: *`
- Windows Trusted Connection in connection strings
- Certificate auth package reference (unused)

**(Medium confidence)** — typical Education Dynamics deployment pattern.

---

## Kubernetes

No Kubernetes manifests found in repository.

Docker support exists but no k8s YAML, Helm charts, or health probes defined.

---

## Azure

| Service | Usage |
|---------|-------|
| Azure Redis | `eddydevqa.redis.cache.windows.net` in dev config |
| Azure Container Tools | Package reference in Service csproj |

No Azure App Service, Functions, or ARM/Bicep templates in repo.

---

## Health Checks

**Not implemented.** No `AddHealthChecks()` or health endpoint.

Greeter `SayHello` serves as informal connectivity test only.

---

## Startup Dependencies

| Dependency | Required at Start | Failure Mode |
|------------|-------------------|--------------|
| Redis | Yes | CacheService errors, NeedsReCompute returns true |
| GlassPanel SQL | Yes (background) | Cache refresh fails, logged |
| Matching Engine | No (EAV path only) | EAV requests fail individually |

---

## Deployment Checklist

1. Update Dockerfile to .NET 6.0 images
2. Configure connection strings via secure vault
3. Configure Redis with SSL
4. Set `ASPNETCORE_ENVIRONMENT`
5. Install New Relic agent (if using APM)
6. Verify Matching Engine endpoint reachable (for EAV)
7. Ensure GlassPanel views exist (VW_SlimAdsAMS, VW_AdsAMS, etc.)
8. Configure reverse proxy for gRPC-Web if browser clients
9. Set `Redis:ShouldLoadCache=true` for production
10. Tune `ComputeIntervalSeconds` and `LifeSpanSeconds` for load
