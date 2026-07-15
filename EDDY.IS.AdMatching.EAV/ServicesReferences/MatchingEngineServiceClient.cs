using EDDY.IS.AdMatching.Domain.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EDDY.IS.AdMatching.EAV.MatchingEngineService
{
    public interface IMatchingEngineService : IMatchingService { }
    public partial class MatchingServiceClient : IMatchingEngineService
    {
        private readonly MatchingEngineServiceSettings _settings;

        [ActivatorUtilitiesConstructor]
        public MatchingServiceClient(IOptions<MatchingEngineServiceSettings> options)
            : base(MatchingServiceClient.GetBindingForEndpoint(EndpointConfiguration.CustomBinding_IMatchingService)
                    , MatchingServiceClient.GetEndpointAddress(EndpointConfiguration.CustomBinding_IMatchingService))
        {
            this._settings = options.Value;
            this.Endpoint.Name = EndpointConfiguration.CustomBinding_IMatchingService.ToString();
            this.Endpoint.Address = new System.ServiceModel.EndpointAddress(new Uri(_settings.Endpoint));
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
    }
}
