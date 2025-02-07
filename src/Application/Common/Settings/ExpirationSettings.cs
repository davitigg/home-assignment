namespace Application.Common.Settings
{
    public class ExpirationSettings
    {
        public const string SectionName = "ExpirationSettings";
        public int OtpLifetimeMinutes { get; set; }
        public int PendingDeviceLifetimeMinutes { get; set; }
    }
}
