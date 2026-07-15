namespace EDDY.IS.Common.Settings;

public class RedisSettings
{
    public static string SectionName = "Redis";

    public string Server { get; set; }
    public int LifeSpanSeconds { get; set; }
    public int ComputeIntervalSeconds { get; set; }
    public bool ShouldLoadCache { get; set; }
    public string CachePrefix { get; set; }
}