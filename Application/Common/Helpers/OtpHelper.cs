namespace Application.Common.Helpers
{
    public static class OtpHelper
    {
        public static string GenerateOtpPin()
        {
            var random = new Random();
            return random.Next(1000, 9999).ToString();
        }

        public static string BuildMobileOtpKey(string userId)
        {
            return $"{userId}:{OtpChannel.Mobile}";
        }

          public static string BuildEmailOtpKey(string userId)
        {
            return $"{userId}:{OtpChannel.Email}";
        }

        private enum OtpChannel
        {
            Mobile,
            Email
        }
    }
}
