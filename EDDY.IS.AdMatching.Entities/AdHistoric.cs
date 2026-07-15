namespace EDDY.IS.AdMatching.Entities
{
    public partial class AdHistoric
    {
        public int HistoricAdId { get; set; }
        public int AdId { get; set; }
        public int ClientAdAccountId { get; set; }
        public int AdCreativeTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? ClickUrl { get; set; }
        public string Headline { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? DisplayUrl { get; set; }
        public bool? IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? PopularPrograms { get; set; }
    }
}
