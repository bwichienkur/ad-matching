using EDDY.IS.AdMatching.EAV.Interface;
using EDDY.IS.AdMatching.EAV.MatchingEngineService;
using EDDY.IS.AdMatching.EAV.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EDDY.IS.AdMatching.EAV
{
    public class Config
    {
        public static void RegisterDependencies(IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IEddyAdsListingService, EddyAdsListingService>();
            services.AddScoped<IMatchingEngineService, MatchingServiceClient>();
        }
    }
}
