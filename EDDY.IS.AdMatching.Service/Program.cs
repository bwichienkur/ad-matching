using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;

namespace EDDY.IS.AdMatching.Service;

public class Program
{
    public static void Main(string[] args)
    {
        Logger logger = null;
        try
        {
            logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("Application Starting Up");
            NLog.LogManager.ThrowConfigExceptions = true;
            NLog.LogManager.ThrowExceptions = true;

            var host = CreateHostBuilder(args).Build();

            //warm up the service
            //var scope = host.Services.CreateScope();
            // var commonEngine = scope.ServiceProvider.GetService<IEngine>();
            // commonEngine.FilterDictionaryContainer(0, Guid.NewGuid().ToString());
            host.Run();
        }
        catch (Exception exception)
        {
            if (logger != null)
                logger.Error(exception, "Stopped program because of exception");
        }
        finally
        {
            NLog.LogManager.Shutdown();
        }
    }
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                });
                webBuilder.UseStartup<Startup>();
                webBuilder.UseNLog();
            });
    }
}