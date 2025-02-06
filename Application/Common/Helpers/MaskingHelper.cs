namespace Application.Common.Helpers
{
    public static class MaskingHelper
    {
        public static string MaskPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone) || phone.Length < 4)
                return "****";

            return $"{phone[..4]}****{phone[^2..]}";
        }

        public static string MaskEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                return "****@****";
            var parts = email.Split('@');
            var namePart = parts[0];
            var domainPart = parts[1];

            return $"{namePart[0]}*****@{domainPart}";
        }
    }
}