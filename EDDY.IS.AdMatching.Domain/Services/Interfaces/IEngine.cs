using EDDY.IS.AdMatching.Domain.BusinessEntities;

namespace EDDY.IS.AdMatching.Domain.Services.Interfaces
{
    /// <summary>
    /// IEngine interface is used to get Dictionary of different Data Points such as Ads, Campaign, AdGroup
    /// </summary>
    public interface IEngine : IDisposable
    {
        Task<FilteredContainerDictionary> LoadSharedContainer(DictionaryContainer container);
        Task<FilteredContainerDictionary> FilterDictionaryContainer(int sourceId, DictionaryContainer container);
        Task<FilteredContainerDictionary> GetCacheDictionaryContainer(int sourceId);

    }
}
