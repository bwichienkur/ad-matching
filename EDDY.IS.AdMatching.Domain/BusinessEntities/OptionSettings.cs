namespace EDDY.IS.AdMatching.Domain.BusinessEntities
{
    /// <summary>
    /// OptionSettings is used to be utilized by Options Pattern for Strongly Type Configuration extraction of information by IOptionsMonitor<OptionSettings> options
    /// in the future
    /// </summary>
    public class OptionSettings
    {
        public const string ConnectionStrings = "ConnectionStrings";
        public string GlassPanelConnection { get; set; } = String.Empty;
    }
}
