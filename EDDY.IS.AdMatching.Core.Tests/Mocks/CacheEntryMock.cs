using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace EDDY.IS.AdMatching.Core.Tests.Mocks;

public class CacheEntryMock : ICacheEntry
{
    public void Dispose()
    { 
    }

    public DateTimeOffset? AbsoluteExpiration { get; set; }
    public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
    public IList<IChangeToken> ExpirationTokens { get; }
    public object Key { get; }
    public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; }
    public CacheItemPriority Priority { get; set; }
    public long? Size { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }
    public object Value { get; set; }
}