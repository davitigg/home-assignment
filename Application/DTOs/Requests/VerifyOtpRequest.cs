namespace Application.DTOs.Requests
{
    public class VerifyOtpRequest
    {
        public string ICNumber { get; set; }
        public string OtpPin { get; set; }
    }
}