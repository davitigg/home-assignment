namespace Application.DTOs.Requests
{
    public class SendOtpRequest
    {
        public string ICNumber { get; set; }
        public string DeviceId { get; set; }
    }
}