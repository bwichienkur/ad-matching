namespace EDDY.IS.AdMatching.Entities
{
    public partial class AdImage
    {
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

        public virtual Ad Ad { get; set; } = null!;
        public virtual AdImageSizeType AdImageSizeType { get; set; } = null!;
    }
}
