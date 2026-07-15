namespace EDDY.IS.AdMatching.Entities
{
    public partial class AdImageHistoric
    {
        public int HistoricAdImageId { get; set; }
        public int AdImageId { get; set; }
        public int AdId { get; set; }
        public int AdImageSizeTypeId { get; set; }
        public string Url { get; set; } = null!;
        public bool? IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
