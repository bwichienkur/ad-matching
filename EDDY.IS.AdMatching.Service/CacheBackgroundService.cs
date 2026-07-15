using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.Common.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Service;

public class CacheBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly RedisSettings _cacheSettings;
    private readonly ILogger _logger;

    public CacheBackgroundService(
        IServiceProvider serviceProvider,
        IOptions<RedisSettings> cacheSettings,
        ILogger<CacheBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _cacheSettings = cacheSettings.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var tasks = new List<Task>();
                    var commonEngine = scope.ServiceProvider.GetService<IEngine>();
                    var dataManager = scope.ServiceProvider.GetService<IDataManager>();

                    var freshData = dataManager.GetDictionaryContainer();
                    await commonEngine.LoadSharedContainer(freshData);

                    foreach (var key in freshData.CampaignsBySource.Keys)
                        tasks.Add(commonEngine.FilterDictionaryContainer(key, freshData));

                    await Task.WhenAll(tasks);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Cache service fails");
                }
            }

            await Task.Delay((int)_cacheSettings.ComputeIntervalSeconds * 1000, stoppingToken);
        }
    }

}