namespace EDDY.IS.AdMatching.EAV.Extensions
{
    public static class StringExtension
    {
        public static KeyValuePair<string, int> StartDateToKeyValue(this string source)
        {
            return source switch
            {
                "Immediately" => new KeyValuePair<string, int>("Desired_Start_Date", 1),
                "1-3 Months" => new KeyValuePair<string, int>("Desired_Start_Date", 2),
                "4-6 Months" => new KeyValuePair<string, int>("Desired_Start_Date", 3),
                "7-12 Months" => new KeyValuePair<string, int>("Desired_Start_Date", 4),
                "More than 1 Year" => new KeyValuePair<string, int>("Desired_Start_Date", 5),
                "Not Sure" => new KeyValuePair<string, int>("Desired_Start_Date", 6),
                _ => new KeyValuePair<string, int>("Desired_Start_Date", 6),
            };
        }
    }
}
