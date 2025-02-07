namespace Domain.Entities
{
    public class PendingDevice
    {
        public string Id => $"{UserId}";
        public int UserId { get; private set; }
        public string DeviceId { get; private set; }
        public bool IsMobileVerified { get; private set; } = false;
        public bool IsEmailVerified { get; private set; } = false;
        public bool IsPrivacyPolicyAccepted { get; private set; } = false;

        public TimeSpan ExpirationDuration { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime ExpiresAt => CreatedAt.Add(ExpirationDuration);

        public PendingDevice(int userId, string deviceId, TimeSpan expirationDuration)
        {
            UserId = userId;
            DeviceId = deviceId;
            ExpirationDuration = expirationDuration;
        }

        public void VerifyMobile() => IsMobileVerified = true;
        public void VerifyEmail() => IsEmailVerified = true;
        public void AcceptPrivacyPolicy() => IsPrivacyPolicyAccepted = true;

        public bool CanBeRegistered() => IsMobileVerified && IsEmailVerified && IsPrivacyPolicyAccepted;
    }
}
