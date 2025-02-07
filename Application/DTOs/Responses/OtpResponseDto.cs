using Domain.Enums;

namespace Application.DTOs.Responses
{
    public class OtpResponseDto
    {
        public string MaskedContact { get; set; }

        public OtpResponseDto(string maskedContact, OtpType otpType)
        {
            MaskedContact = otpType switch
            {
                OtpType.Mobile => MaskPhone(maskedContact),
                OtpType.Email => MaskEmail(maskedContact),
                _ => maskedContact
            };
        }

        private static string MaskPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone) || phone.Length < 4)
                return "****";

            return $"{phone[..4]}****{phone[^2..]}";
        }

        private static string MaskEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                return "****@****";
            var parts = email.Split('@');
            var namePart = parts[0];
            var domainPart = parts[1];

            return $"{namePart[0]}*****@{domainPart}";
        }
    }
}