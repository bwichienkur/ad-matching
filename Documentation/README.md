# EDDY.IS.AdMatching — Enterprise Documentation

Reverse-engineered documentation for the **Ad Matching Service (AMS)** solution. This documentation set is intended for engineering teams maintaining, extending, or rewriting the system.

## Documentation Index

| Section | Path | Description |
|---------|------|-------------|
| Executive Summary | [ExecutiveSummary.md](./ExecutiveSummary.md) | Business purpose, architecture overview, strengths/weaknesses |
| Solution & Architecture | [Architecture.md](./Architecture.md) | Solution structure, dependency graph, DI, config, auth, logging |
| Business Processes | [BusinessProcesses.md](./BusinessProcesses.md) | Major workflows with sequence diagrams |
| Data Flow | [DataFlow.md](./DataFlow.md) | Request lifecycle and integration flows |
| Design Patterns | [DesignPatterns.md](./DesignPatterns.md) | Patterns used across the solution |
| Code Quality | [CodeQuality.md](./CodeQuality.md) | Smells, SOLID violations, dead code |
| Testing | [Testing.md](./Testing.md) | Test projects, coverage gaps, recommendations |
| AI Knowledge Base | [AI_CONTEXT.md](./AI_CONTEXT.md) | Condensed context for AI agents |

### Projects (14 analyzed)

| Project | Path |
|---------|------|
| EDDY.IS.AdMatching.Service | [Projects/EDDY.IS.AdMatching.Service.md](./Projects/EDDY.IS.AdMatching.Service.md) |
| EDDY.IS.AdMatching.Core | [Projects/EDDY.IS.AdMatching.Core.md](./Projects/EDDY.IS.AdMatching.Core.md) |
| EDDY.IS.AdMatching.Domain | [Projects/EDDY.IS.AdMatching.Domain.md](./Projects/EDDY.IS.AdMatching.Domain.md) |
| EDDY.IS.AdMatching.Data | [Projects/EDDY.IS.AdMatching.Data.md](./Projects/EDDY.IS.AdMatching.Data.md) |
| EDDY.IS.AdMatching.Entities | [Projects/EDDY.IS.AdMatching.Entities.md](./Projects/EDDY.IS.AdMatching.Entities.md) |
| EDDY.IS.AdMatching.Repositories | [Projects/EDDY.IS.AdMatching.Repositories.md](./Projects/EDDY.IS.AdMatching.Repositories.md) |
| EDDY.IS.AdMatching.EAV | [Projects/EDDY.IS.AdMatching.EAV.md](./Projects/EDDY.IS.AdMatching.EAV.md) |
| EDDY.IS.RuleEngine | [Projects/EDDY.IS.RuleEngine.md](./Projects/EDDY.IS.RuleEngine.md) |
| EDDY.IS.Common | [Projects/EDDY.IS.Common.md](./Projects/EDDY.IS.Common.md) |
| EDDY.IS.AdMatching.Caching | [Projects/EDDY.IS.AdMatching.Caching.md](./Projects/EDDY.IS.AdMatching.Caching.md) |
| EDDY.IS.AdMatching.Core.Tests | [Projects/EDDY.IS.AdMatching.Core.Tests.md](./Projects/EDDY.IS.AdMatching.Core.Tests.md) |
| EDDY.IS.Test.AdMatchingServiceTest | [Projects/EDDY.IS.Test.AdMatchingServiceTest.md](./Projects/EDDY.IS.Test.AdMatchingServiceTest.md) |
| Client (EDDY.IS.Examples.ClientNet) | [Projects/Client.md](./Projects/Client.md) |

### APIs

| Document | Path |
|----------|------|
| gRPC API Reference | [APIs/gRPC-API.md](./APIs/gRPC-API.md) |

### Database

| Document | Path |
|----------|------|
| Database Overview & ER Diagram | [Database/Overview.md](./Database/Overview.md) |
| DbContext & Repositories | [Database/DbContext.md](./Database/DbContext.md) |

### Entities

| Document | Path |
|----------|------|
| Entity Catalog | [Entities/EntityCatalog.md](./Entities/EntityCatalog.md) |

### Services

| Document | Path |
|----------|------|
| Service Catalog | [Services/ServiceCatalog.md](./Services/ServiceCatalog.md) |
| Handler Chain | [Services/HandlerChain.md](./Services/HandlerChain.md) |
| Important Classes | [Classes/ImportantClasses.md](./Classes/ImportantClasses.md) |

### Cross-Cutting Concerns

| Document | Path |
|----------|------|
| Security Review | [Security/SecurityReview.md](./Security/SecurityReview.md) |
| Performance Review | [Performance/PerformanceReview.md](./Performance/PerformanceReview.md) |
| Deployment | [Deployment/Deployment.md](./Deployment/Deployment.md) |
| Refactoring Recommendations | [Refactoring/Recommendations.md](./Refactoring/Recommendations.md) |

### Diagrams

| Document | Path |
|----------|------|
| Dependency Graphs | [Diagrams/DependencyGraphs.md](./Diagrams/DependencyGraphs.md) |
| Sequence Diagrams | [Diagrams/SequenceDiagrams.md](./Diagrams/SequenceDiagrams.md) |
| Class Diagrams | [Diagrams/ClassDiagrams.md](./Diagrams/ClassDiagrams.md) |
| Flowcharts | [Diagrams/Flowcharts.md](./Diagrams/Flowcharts.md) |

## Solution File

`EDDY.IS.AdMatching.sln` — 12 buildable projects + Solution Items folder.

## Confidence Notes

Documentation conclusions are derived from source code analysis. Items marked **(inferred)** lack explicit code confirmation. Database schema details beyond EF mappings are **(inferred from entity/view names)** unless stated otherwise.
