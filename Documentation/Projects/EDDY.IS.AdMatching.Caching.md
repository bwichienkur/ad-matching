# EDDY.IS.AdMatching.Caching

## Status: **DEAD / STUB PROJECT**

## Purpose

**None** — placeholder project with empty `Class1` class. Not referenced by any other project or the solution file.

## Contents

```csharp
// Class1.cs
namespace EDDY.IS.AdMatching.Caching
{
    public class Class1 { }
}
```

## Actual Caching Location

All caching logic lives in **`EDDY.IS.AdMatching.Core/Services/CacheService.cs`**.

## Recommendation

**Delete this project** or repurpose it by moving CacheService here (would require refactoring Core dependencies).

## Dependencies

None — empty net6.0 class library.

## Potential Improvements

- Delete project to eliminate confusion
- If kept, move ICacheService interface and CacheService implementation here
