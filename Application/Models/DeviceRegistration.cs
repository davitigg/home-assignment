namespace Application.Models
{
    public class DeviceRegistration(string deviceId)
    {
        public string DeviceId { get; set; } = deviceId;
        public bool MobileVerified { get; set; } = false;
        public bool EmailVerified { get; set; } = false;
        public bool PrivacyPolicyAccepted { get; set; } = false;
    }
}
