using System;
using EDDY.IS.Common.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;

namespace EDDY.IS.AdMatching.Core.Logging;

public class DebugLogger
{
    private readonly LoggingDebugInformation _loggingDebugInformation;
    private Logger _logger = null;

    public DebugLogger(IOptions<LoggingDebugInformation> loggingDebugInformation)
    {
        if (loggingDebugInformation != null)
        _loggingDebugInformation = loggingDebugInformation.Value;

        if (_loggingDebugInformation is {EnabledTrueFalse: true})
        {
            _logger = NLog.Web.NLogBuilder.ConfigureNLog("./NLog.config").GetLogger("debugLogger");
        }
    }


    public void Log(string text, object toSerialize)
    {
        if (_loggingDebugInformation is {EnabledTrueFalse: true})
        {
            _logger.Debug($"{text}, {JsonConvert.SerializeObject(toSerialize)}");
        }
        
    }

    public void LogNewProcessStarts()
    {
        Log($"{Environment.NewLine}{Environment.NewLine}*******************************************************************","");
    }
}