namespace EDDY.IS.Common.Settings;

public class BaseUrlSubstitutions
{
    public static string SectionName = "BaseUrlSubstitutions";

    public List<Substitution> Substitutions { get; set; } = new ();

}

public class Substitution
{
    public bool EnabledTrueFalse { get; set; }
    public string BaseUrlSubstitutionFrom { get; set; }
    public string BaseUrlSubstitutionTo { get; set; }

}