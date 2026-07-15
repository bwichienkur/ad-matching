# Security Review

## Summary

**Overall risk: Medium-High** — primarily due to missing authentication, secrets in config, and dynamic code execution in URL parameter handling.

---

## Secrets & Credentials

| Finding | Severity | Evidence |
|---------|----------|----------|
| Redis password in appsettings.json | **Critical** | `appsettings.json:16` |
| SQL connection strings committed | **High** | `appsettings.json:22-25` |
| User Secrets ID configured but secrets in file | Medium | `Service.csproj:12` |
| Client cert file present (Cert/EDCertificate.pfx) | Medium | `Client/Cert/` — usage not found in code |
| WCF endpoint uses HTTP not HTTPS | Medium | `appsettings.json:29` |

**Recommendation:** Move all secrets to Azure Key Vault / environment variables; rotate Redis password.

---

## Authentication & Authorization Gaps

| Finding | Severity | Evidence |
|---------|----------|----------|
| No authentication on gRPC endpoints | **High** | `Startup.cs` — no AddAuthentication |
| Certificate auth package unused | Low | `Service.csproj:26` |
| No rate limiting | Medium | No middleware found |
| No API key or mTLS enforcement | High | Open gRPC at network level |

**Recommendation:** Implement mTLS or API key validation at minimum; consider Azure API Management or reverse proxy auth.

---

## Injection Risks

| Vector | Risk | Evidence |
|--------|------|----------|
| SQL Injection | **Low** | EF Core parameterized queries only; no raw SQL |
| C# Script Injection | **High** | `ParametersEvaluator` uses `CSharpScript.EvaluateAsync` for dynamic URL params |
| Rule JSON injection | Medium | TargetingRule.RuleJson deserialized without schema validation |
| Log injection | Low | SearchGuid logged directly |

**Recommendation:** Replace Roslyn scripting with whitelisted macro substitution; validate RuleJson schema at load time.

---

## Input Validation

| Input | Validation | Gap |
|-------|------------|-----|
| SourceId | None explicit | Invalid ID → empty results (fail-open) |
| MaxAds | None | Could request unlimited (performance DoS) |
| Parameters map | None | Arbitrary keys passed to rule engine |
| StaticAdGuid | GUID parse only | Invalid GUID → non-static path |
| Proto unmapped fields | Ignored | PlacementGuid, SessionGuid dropped |

**Recommendation:** Validate SourceId against known sources; cap MaxAds server-side.

---

## CSRF / XSS

| Vector | Applicability |
|--------|---------------|
| CSRF | N/A — gRPC API, no browser form posts to AMS |
| XSS | N/A at service level; ad content URLs returned to publishers may contain XSS if GlassPanel data compromised |
| Open Redirect | **Medium** — click URLs substituted from DB without URL validation |

---

## SSRF

| Vector | Risk |
|--------|------|
| WCF Matching Engine call | Low — endpoint from config, not user input |
| Logo CDN URLs | Low — template from EAVSettings |

---

## Privilege Escalation

Not applicable — no user roles or auth model in AMS.

GlassPanel permission entities (`Permission`, `PermissionRole`, `VwUser*`) exist in DbContext but are **not used** by AMS runtime.

---

## PII Handling

| Data | Handling |
|------|----------|
| Visitor parameters (Age, State, Zip, etc.) | Passed through rule engine; logged in performance tables when enabled |
| SearchGuid | Logged with full request/response JSON in performance logging |

**Recommendation:** Review performance logging for PII compliance (GDPR/FERPA for education leads).

---

## Dependency Vulnerabilities

| Package | Version | Note |
|---------|---------|------|
| EF Core | 6.0.0 | Check for CVEs; consider 6.0 LTS latest patch |
| Grpc.AspNetCore | 2.40.0 | Update available |
| StackExchange.Redis | 2.6.66 | Check advisories |
| System.Data.SqlClient | 4.9.0 | Consider Microsoft.Data.SqlClient |
| NLog.* | 5.0.x/5.1.x | Check advisories |

**Recommendation:** Run `dotnet list package --vulnerable` in CI.

---

## Error Information Disclosure

```csharp
// AdsService.cs:58
Message = "There is an error: " + ex.Message
```

Exception messages returned to clients may expose internal details.

**Recommendation:** Return generic error messages; log details server-side only.

---

## Recommendations Priority

| Priority | Action |
|----------|--------|
| P0 | Remove secrets from committed config |
| P0 | Add authentication (mTLS or API key) |
| P1 | Remove/replace C# scripting in ParametersEvaluator |
| P1 | Cap MaxAds and validate SourceId |
| P2 | Sanitize error responses |
| P2 | Enable HTTPS for WCF endpoint |
| P3 | Add rate limiting |
| P3 | Dependency vulnerability scanning in CI |
