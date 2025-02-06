namespace Application.Common.Helpers
{
    public static class OtpHelper
    {
        public static string GenerateOtpPin()
        {
            var random = new Random();
            return random.Next(1000, 9999).ToString();
        }

        public static string BuildKey(string deviceId, OtpChannel channel)
        {
            return $"{deviceId}:{channel}";
        }

        public enum OtpChannel
        {
            Mobile,
            Email
        }
    }
}
