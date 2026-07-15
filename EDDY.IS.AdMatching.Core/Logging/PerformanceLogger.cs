using EDDY.IS.AdMatching.Core.Auxiliary;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Targets;
using System;

namespace EDDY.IS.AdMatching.Core.Logging
{
    public class PerformanceLogger
    {
        private Logger _PerformanceloggerDetail = null;
        private Logger _PerformanceloggerMaster = null;
        private readonly IConfiguration _config;
        public PerformanceLogger(IConfiguration config)
        {
            _config = config;
            var _isLoggingEnabled = _config.GetValue<bool>("LoggingPerformance:EnabledTrueFalse");
            if (_isLoggingEnabled)
            {
                _PerformanceloggerDetail = NLog.Web.NLogBuilder.ConfigureNLog("./nlogPerformance.config").GetLogger("_PerformanceLoggingDetail");
                _PerformanceloggerMaster = NLog.Web.NLogBuilder.ConfigureNLog("./nlogPerformance.config").GetLogger("_PerformanceLoggingMaster");
                
                using (var databaseTarget = (DatabaseTarget)((NLog.Targets.Wrappers.WrapperTargetBase)LogManager.Configuration.FindTargetByName("PerformanceLoggingDetail")).WrappedTarget)
                { databaseTarget.ConnectionString = _config.GetValue<string>("ConnectionStrings:EddyLogging"); }
                using (var databaseTarget = (DatabaseTarget)((NLog.Targets.Wrappers.WrapperTargetBase)LogManager.Configuration.FindTargetByName("PerformanceLoggingMaster")).WrappedTarget)
                { databaseTarget.ConnectionString = _config.GetValue<string>("ConnectionStrings:EddyLogging"); }

                LogManager.ReconfigExistingLoggers();
            }
        }


        public void LogPerformanceDetail(string searchGuid, int levelId, string stepName, DateTime startTime)
        {
            DateTime endTime = DateTime.Now;
            TimeSpan ts = endTime - startTime;
            var enabled = _config.GetValue<bool>("LoggingPerformance:EnabledTrueFalse");
            if (enabled)
            {
                _PerformanceloggerDetail.WithProperty("RequestStartTimeStamp", startTime.ToString("yyyy-MM-dd HH: mm:ss.fff"))
                      .WithProperty("RequestEndTimeStamp", endTime.ToString("yyyy-MM-dd HH: mm:ss.fff"))
                      .WithProperty("ResponseTimeInMilliseconds", Convert.ToInt32(ts.TotalMilliseconds))
                      .WithProperty("StepName", stepName)
                      .WithProperty("LevelId", levelId)
                      .WithProperty("SearchGuid", searchGuid).Fatal("This is a test.");
            }
        }

        public void LogPerformance(string searchGuid, string requestString, string responseString, DateTime startTime, DateTime endTime)
        {
            TimeSpan ts = endTime - startTime;
            var enabled = _config.GetValue<bool>("LoggingPerformance:EnabledTrueFalse");
            if (enabled)
            {
                _PerformanceloggerMaster.WithProperty("RequestStartTimeStamp", startTime.ToString("yyyy-MM-dd HH: mm:ss.fff"))
                        .WithProperty("RequestEndTimeStamp", endTime.ToString("yyyy-MM-dd HH: mm:ss.fff"))
                        .WithProperty("PerformanceLoggingName", "AdMatchingService")
                          .WithProperty("ResponseTimeInMilliseconds", Convert.ToInt32(ts.TotalMilliseconds))
                          .WithProperty("RequestMessage", requestString)
                          .WithProperty("ResponseMessage", responseString)
                          .WithProperty("ISApplicationId", (int)ISApplication.AdMatching)
                          .WithProperty("RequestISApplicationId", (int)ISApplication.AdAggregator)
                          .WithProperty("SearchGuid", searchGuid).Info("This is a test.");
            }
        }
    }


}
