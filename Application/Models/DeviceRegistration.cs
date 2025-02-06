namespace Application.Models
{
    public class DeviceRegistration(int userId, string deviceId)
    {
        public int UserId { get; set; } = userId;
        public string DeviceId { get; set; } = deviceId;
        public bool MobileVerified { get; set; } = false;
        public bool EmailVerified { get; set; } = false;
    }
}
