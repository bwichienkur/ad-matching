namespace EDDY.IS.AdMatching.Domain.Settings
{
    public class EAVSettings
    {
        public static string SectionName = "EAVSetting";
        public string? EddyLogoImageSizeSmall { get; set; }
        public string? EddyLogoImageSizeMedium { get; set; }
        public string? EddyLogoImageSizeLarge { get; set; }
        public string? EddyLogoImagePathDomain { get; set; }
        public string? EddyLogoImageFileName { get; set; }
        public string? DirectoryInstitutionURL { get; set; }
        public string? DirectoryInstitutionProgramURL { get; set; }
    }
}
