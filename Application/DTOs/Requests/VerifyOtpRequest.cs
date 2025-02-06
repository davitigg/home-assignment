namespace Application.DTOs.Requests
{
    public class VerifyOtpRequest
    {
        public string OtpToken { get; set; }
        public string Otp { get; set; }
    }
}