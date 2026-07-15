using System;
using Microsoft.Extensions.Caching.Memory;

namespace EDDY.IS.AdMatching.Core.Tests.Mocks;

public class MemoryCacheMock : IMemoryCache
{
    public void Dispose() => throw new NotImplementedException();

    public ICacheEntry CreateEntry(object key)
    {
        return new CacheEntryMock();
    }

    public void Remove(object key) => throw new NotImplementedException();

    public bool TryGetValue(object key, out object value)
    {
        value = new object();
        return false;
    }
}