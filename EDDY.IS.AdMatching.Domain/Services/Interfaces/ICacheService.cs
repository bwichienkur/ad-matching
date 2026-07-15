namespace EDDY.IS.AdMatching.Domain.Services.Interfaces
{
    public interface ICacheService
    {
        Task<TEntity> GetFromCache<TEntity>(string key);
        Task<bool> SetValueToCache<TEntity>(string key, TEntity value);
        Task<bool> NeedsReCompute(string key);
        Task<TEntity> RefreshFromDistributedCache<TEntity>(string key);
    }
}
