namespace EDDY.IS.AdMatching.Entities
{
    public partial class StateTimeZone
    {
        public StateTimeZone()
        {
            StateCode = string.Empty;
            TimeZoneCode = string.Empty;
        }
        public int Id { get; set; }
        public string StateCode { get; set; }
        public string TimeZoneCode { get; set; }
    }
}
