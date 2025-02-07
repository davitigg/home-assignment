using Domain.Enums;

namespace Domain.Entities
{
    public class OtpEntry
    {
        public string Id => $"{UserId}:{Type}";
        public int UserId { get; private set; }
        public string Contact { get; private set; } // Email or PhoneNumber
        public OtpType Type { get; private set; }
        public string Code { get; private set; } // The generated OTP
        public bool ResendPermitted => DateTime.Now.Subtract(CreatedAt).TotalSeconds > 120;

        public TimeSpan ExpirationDuration { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime ExpiresAt => CreatedAt.Add(ExpirationDuration);

        public OtpEntry(int userId, string contact, OtpType type, TimeSpan expirationDuration)
        {
            UserId = userId;
            Contact = contact;
            Type = type;
            Code = GenerateOtp();
            ExpirationDuration = expirationDuration;
        }

        private static string GenerateOtp()
        {
            var random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
}
