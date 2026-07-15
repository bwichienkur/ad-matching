using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;

namespace EDDY.IS.AdMatching.Core.Tests
{
    public class MockDataManager : IDataManager
    {
        public DictionaryContainer DictionaryContainer { get; set; } = new DictionaryContainer();

        public void Dispose()
        {
        }

        public DictionaryContainer GetDictionaryContainer()
        {
            return DictionaryContainer;
        }
    }
}
