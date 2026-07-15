namespace EDDY.IS.AdMatching.Entities
{
    public partial class Ad
    {
        public int AdId { get; set; }
        public int ClientAdAccountId { get; set; }
        public string Name { get; set; } = null!;
        public string BackupUrl { get; set; }
        public Guid AdGuid { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
    }
}
