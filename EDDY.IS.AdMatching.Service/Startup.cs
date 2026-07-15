using EDDY.IS.AdMatching.Core.ChainResponsability;
using EDDY.IS.AdMatching.Core.Engines;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Core.RequestHandler;
using EDDY.IS.AdMatching.Core.Services;
using EDDY.IS.AdMatching.Data;
using EDDY.IS.AdMatching.Data.Context;
using EDDY.IS.AdMatching.Data.Infrastructure;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;
using EDDY.IS.AdMatching.Domain.Services;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.AdMatching.Domain.Settings;
using EDDY.IS.AdMatching.Repositories.Interfaces;
using EDDY.IS.AdMatching.Service.Logging;
using EDDY.IS.AdMatching.Service.Services;
using EDDY.IS.Common.Settings;
using EDDY.IS.RuleEngine.CustomRuleEngine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace EDDY.IS.AdMatching.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var redisSettings = Configuration.GetSection(RedisSettings.SectionName)
                .Get<RedisSettings>();

            services.AddMemoryCache();

            services.Configure<LoggingDebugInformation>(Configuration.GetSection(LoggingDebugInformation.SectionName));
            services.Configure<RedisSettings>(Configuration.GetSection(RedisSettings.SectionName));
            services.Configure<BaseUrlSubstitutions>(Configuration.GetSection(BaseUrlSubstitutions.SectionName));
            services.Configure<MatchingEngineServiceSettings>(Configuration.GetSection(MatchingEngineServiceSettings.SectionName));
            services.Configure<EAVSettings>(Configuration.GetSection(EAVSettings.SectionName));
            services.AddStackExchangeRedisCache(option => { 
                option.Configuration = redisSettings.Server; 
            });

            var multiplexer = ConnectionMultiplexer.Connect(redisSettings.Server);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            EDDY.IS.AdMatching.EAV.Config.RegisterDependencies(services, Configuration);

            //Dependency Injection / Inversion of Control Pattern setup / Option Pettern IOptionMonitor<Options>
            //services.Configure<OptionSettings>(Configuration.GetSection(OptionSettings.ConnectionStrings));
            services.AddDbContext<GlassPanelContext>(
                    options => options.UseSqlServer(Configuration.GetConnectionString("GlassPanelConnection")));
            services.AddScoped<ICommonUnitOfWorkRepositoryFactory, CommonUnitOfWorkRepositoryFactory>();
            services.AddScoped<IDataManager, CommonDataManager>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<DebugLogger>();
            services.AddSingleton<PerformanceLogger>();
            services.AddSingleton<IRuleEngine, RuleEngine.CustomRuleEngine.RuleEngine>();
            services.AddScoped<IEngine, CommonEngine>();
            services.AddScoped<IAdMatchingService, AdMatchingService>();

            services.Chain<IChainHandler<AdMatchingModel>>()
                .Add<StaticAdHandler>()
                .Add<PreExcludeHandler>()
                .Add<StopsHandler>()
                .Add<CapsHandler>()
                .Add<RuleEngineHandler>()
                .Add<SchedulingHandler>()
                .Add<ResponseBuilderHandler>()
                .Add<DynamicBidVariablesHandler>()
                .Add<AdSortingHandler>()
                .Add<ParametersHandler>()
                .Configure();

            services.AddGrpc(
                options =>
                {
                    options.Interceptors.Add<GrpcExceptionInterceptor>();
                });

            services.AddHostedService<CacheBackgroundService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseGrpcWeb();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();
                endpoints.MapGrpcService<AdsService>().EnableGrpcWeb();
                //endpoints.MapGrpcService<CatalogService>().EnableGrpcWeb();
            });
        }
    }
}
